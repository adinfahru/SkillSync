# Security & Business Rules

Dokumentasi lengkap security considerations dan business rules untuk SkillSync API.

---

## 🔒 Security Considerations

### 1. Authentication

#### JWT Tokens
- **Token Type:** Bearer Token
- **Algorithm:** HS256 (HMAC-SHA256)
- **Expiration:** 1 hour (configurable)
- **Refresh Token:** 7 days (configurable)

**Token Structure:**
```json
{
  "sub": "user-guid",
  "name": "john.doe",
  "email": "john@company.com",
  "role": "Talent",
  "talentId": "talent-guid",
  "nbf": 1701187200,
  "exp": 1701190800,
  "iat": 1701187200
}
```

#### Password Security
- **Hashing Algorithm:** BCrypt atau PBKDF2
- **Salt Rounds:** 12 (BCrypt) atau 10,000 iterations (PBKDF2)
- **Minimum Requirements:**
  - Length: 8 characters
  - Uppercase: ≥ 1
  - Lowercase: ≥ 1
  - Number: ≥ 1
  - Special character: ≥ 1

**Example Implementation:**
```csharp
// Hash password
string hashedPassword = BCrypt.Net.BCrypt.HashPassword(plainPassword, 12);

// Verify password
bool isValid = BCrypt.Net.BCrypt.Verify(plainPassword, hashedPassword);
```

#### Token Refresh Mechanism
1. Client sends refresh token
2. Validate refresh token
3. Check if token is revoked
4. Generate new access token
5. Generate new refresh token (optional rotation)
6. Return both tokens

#### Token Revocation
- Store revoked tokens in cache (Redis)
- Check revocation on each request
- Auto-cleanup expired tokens

---

### 2. Authorization

#### Role-Based Access Control (RBAC)

**Implementation Pattern:**
```csharp
[Authorize(Roles = "Admin")]
[Authorize(Roles = "HR,PM")]
[Authorize(Policy = "ProjectOwner")]
```

#### Custom Authorization Policies

**1. ProjectOwner Policy**
```csharp
services.AddAuthorization(options =>
{
    options.AddPolicy("ProjectOwner", policy =>
        policy.Requirements.Add(new ProjectOwnerRequirement()));
});

// Requirement
public class ProjectOwnerRequirement : IAuthorizationRequirement { }

// Handler
public class ProjectOwnerHandler : AuthorizationHandler<ProjectOwnerRequirement>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        ProjectOwnerRequirement requirement)
    {
        var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var projectId = // Get from route/query
        
        if (IsProjectOwner(userId, projectId))
            context.Succeed(requirement);
            
        return Task.CompletedTask;
    }
}
```

**2. SelfOrAdmin Policy**
```csharp
options.AddPolicy("SelfOrAdmin", policy =>
    policy.RequireAssertion(context =>
        context.User.IsInRole("Admin") ||
        context.User.HasClaim("sub", targetUserId)));
```

**3. AvailableTalentOnly Policy**
- Validate talent availability before assignment
- Check availability status = AVAILABLE

---

### 3. Data Protection

#### Sensitive Data Handling
- ✅ **Never expose** PasswordHash di API responses
- ✅ **Never log** passwords atau tokens
- ✅ **Mask** sensitive data di logs
- ✅ **Use HTTPS** untuk semua communications

#### DTO Security
```csharp
// ❌ Bad - Exposes sensitive data
public class UserDto
{
    public string PasswordHash { get; set; } // Don't do this!
}

// ✅ Good - Hides sensitive data
public class UserDto
{
    public Guid UserId { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string RoleName { get; set; }
    // No password or sensitive fields
}
```

#### Input Validation
- **Data Annotations** di DTOs
- **FluentValidation** untuk complex rules
- **Sanitize** user input
- **Prevent SQL Injection** (EF Core handles this)
- **Prevent XSS** di text fields

**Example:**
```csharp
public class CreateUserDto
{
    [Required]
    [StringLength(50, MinimumLength = 3)]
    [RegularExpression(@"^[a-zA-Z0-9._-]+$")]
    public string Username { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$")]
    public string Password { get; set; }
}
```

---

### 4. Audit Trail

#### Tracking Changes
**Automatic Timestamps:**
- `CreatedAt` - Set saat record dibuat
- `UpdatedAt` - Update setiap kali record di-update

**Implementation:**
```csharp
public override int SaveChanges()
{
    var entries = ChangeTracker.Entries()
        .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);
        
    foreach (var entry in entries)
    {
        if (entry.State == EntityState.Added)
            entry.Property("CreatedAt").CurrentValue = DateTime.UtcNow;
            
        entry.Property("UpdatedAt").CurrentValue = DateTime.UtcNow;
    }
    
    return base.SaveChanges();
}
```

#### Logging User Actions (Optional)
- Log sensitive operations (assign, remove, delete)
- Store: UserId, Action, Timestamp, Resource
- Use for audit & compliance

---

### 5. API Security Best Practices

#### Rate Limiting
```csharp
services.AddRateLimiter(options =>
{
    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
        RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: httpContext.User.Identity?.Name ?? httpContext.Request.Headers.Host.ToString(),
            factory: partition => new FixedWindowRateLimiterOptions
            {
                AutoReplenishment = true,
                PermitLimit = 100,
                Window = TimeSpan.FromMinutes(1)
            }));
});
```

#### CORS Configuration
```csharp
services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", builder =>
    {
        builder.WithOrigins("https://skillsync-frontend.com")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});
```

#### HTTPS Enforcement
```csharp
services.AddHttpsRedirection(options =>
{
    options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
    options.HttpsPort = 443;
});
```

---

## 📋 Business Rules

### 1. Availability Management

#### Rules

**1.1 Default Status**
- New talent default: `AVAILABLE`

**1.2 Automatic Updates**
```
ON ASSIGN:
  Talent.AvailabilityStatus = "ON_PROJECT"
  
ON REMOVE:
  Talent.AvailabilityStatus = "AVAILABLE"
```

**1.3 Manual Updates (HR Only)**
- ✅ `AVAILABLE` ↔ `ON_LEAVE`
- ❌ Cannot manual set to `ON_PROJECT`

**1.4 Validation Rules**
```csharp
public void UpdateAvailability(string newStatus)
{
    // Rule: Cannot manually set to ON_PROJECT
    if (newStatus == "ON_PROJECT")
        throw new BusinessException("Cannot manually set status to ON_PROJECT. Use assignment instead.");
        
    // Rule: Only HR can update
    if (!User.IsInRole("HR"))
        throw new UnauthorizedException("Only HR can update availability.");
        
    // Update
    talent.AvailabilityStatus = newStatus;
}
```

**1.5 Assignment Validation**
```csharp
public void AssignTalent(Guid talentId, Guid projectId)
{
    var talent = GetTalent(talentId);
    
    // Rule: Only AVAILABLE talents can be assigned
    if (talent.AvailabilityStatus != "AVAILABLE")
        throw new BusinessException($"Talent is {talent.AvailabilityStatus} and cannot be assigned.");
        
    // Proceed with assignment...
}
```

---

### 2. Project Management

#### Rules

**2.1 Ownership**
```csharp
// Rule: PM can only manage own projects
public void UpdateProject(Guid projectId, Guid currentUserId)
{
    var project = GetProject(projectId);
    
    if (project.ProjectManagerId != currentUserId)
        throw new ForbiddenException("You can only update your own projects.");
        
    // Proceed...
}
```

**2.2 Deletion Rules**
```csharp
public void DeleteProject(Guid projectId)
{
    var project = GetProject(projectId);
    
    // Rule: Cannot delete if has active assignments
    var hasActiveAssignments = project.Assignments
        .Any(a => a.ReleaseDate == null);
        
    if (hasActiveAssignments)
        throw new BusinessException("Cannot delete project with active talent assignments.");
        
    // Rule: Only owner can delete
    if (project.ProjectManagerId != currentUserId)
        throw new ForbiddenException("Only project owner can delete.");
        
    // Proceed with delete...
}
```

**2.3 Status Transitions**

**Allowed Transitions:**
```
Planning → Ongoing ✅
Planning → On-Hold ✅
Ongoing → Completed ✅
Ongoing → On-Hold ✅
On-Hold → Ongoing ✅
On-Hold → Planning ✅
Completed → (any) ❌
```

**Implementation:**
```csharp
public void UpdateProjectStatus(Guid projectId, string newStatus)
{
    var project = GetProject(projectId);
    
    // Rule: Cannot change status if completed
    if (project.Status == "Completed")
        throw new BusinessException("Cannot change status of completed project.");
        
    // Validation based on current status
    var validTransitions = new Dictionary<string, string[]>
    {
        ["Planning"] = new[] { "Ongoing", "On-Hold" },
        ["Ongoing"] = new[] { "Completed", "On-Hold" },
        ["On-Hold"] = new[] { "Ongoing", "Planning" }
    };
    
    if (!validTransitions[project.Status].Contains(newStatus))
        throw new BusinessException($"Invalid status transition from {project.Status} to {newStatus}.");
        
    project.Status = newStatus;
}
```

---

### 3. Talent Assignment

#### Rules

**3.1 Pre-Assignment Validation**
```csharp
public void AssignTalent(AssignTalentDto dto)
{
    var talent = GetTalent(dto.TalentId);
    var project = GetProject(dto.ProjectId);
    
    // Rule 1: Talent must be AVAILABLE
    if (talent.AvailabilityStatus != "AVAILABLE")
        throw new BusinessException("Talent must be AVAILABLE to be assigned.");
        
    // Rule 2: No duplicate assignment
    var existingAssignment = projectAssignments
        .FirstOrDefault(a => a.ProjectId == dto.ProjectId 
                          && a.TalentId == dto.TalentId 
                          && a.ReleaseDate == null);
                          
    if (existingAssignment != null)
        throw new BusinessException("Talent is already assigned to this project.");
        
    // Rule 3: Project must be owned by current PM
    if (project.ProjectManagerId != currentUserId)
        throw new ForbiddenException("You can only assign talents to your own projects.");
        
    // Rule 4: Project status validation
    if (project.Status == "Completed")
        throw new BusinessException("Cannot assign talents to completed project.");
        
    // Proceed...
}
```

**3.2 Assignment Actions**
```csharp
public void CreateAssignment(AssignTalentDto dto)
{
    // Validate (see above)
    
    // Create assignment
    var assignment = new ProjectAssignment
    {
        ProjectId = dto.ProjectId,
        TalentId = dto.TalentId,
        RoleOnProject = dto.RoleOnProject,
        AssignedDate = DateTime.UtcNow,
        ReleaseDate = null
    };
    
    // Auto-update availability
    talent.AvailabilityStatus = "ON_PROJECT";
    
    // Save
    Save(assignment, talent);
}
```

**3.3 Removal Actions**
```csharp
public void RemoveTalent(Guid assignmentId)
{
    var assignment = GetAssignment(assignmentId);
    var talent = GetTalent(assignment.TalentId);
    
    // Set release date (soft delete)
    assignment.ReleaseDate = DateTime.UtcNow;
    
    // Auto-update availability back to AVAILABLE
    talent.AvailabilityStatus = "AVAILABLE";
    
    // Save
    Save(assignment, talent);
}
```

**3.4 Historical Tracking**
- Keep all assignments (soft delete dengan ReleaseDate)
- ReleaseDate = NULL → active assignment
- ReleaseDate != NULL → historical assignment
- Use for reporting dan analytics

---

### 4. Skill Matching

#### Matching Algorithm

**4.1 Match Score Calculation**
```csharp
public int CalculateMatchScore(TalentProfile talent, Project project)
{
    var requiredSkills = project.ProjectSkills;
    var talentSkills = talent.TalentSkills;
    
    int matchedSkills = 0;
    int levelBonus = 0;
    
    foreach (var required in requiredSkills)
    {
        var talentSkill = talentSkills.FirstOrDefault(ts => ts.SkillId == required.SkillId);
        
        if (talentSkill == null)
            continue;
            
        // Skill level hierarchy
        var levels = new[] { "Beginner", "Intermediate", "Advanced", "Expert" };
        var requiredLevelIndex = Array.IndexOf(levels, required.MinimumLevel);
        var talentLevelIndex = Array.IndexOf(levels, talentSkill.SkillLevel);
        
        // Must meet minimum level
        if (talentLevelIndex >= requiredLevelIndex)
        {
            matchedSkills++;
            
            // Bonus for higher levels
            levelBonus += (talentLevelIndex - requiredLevelIndex) * 5;
        }
    }
    
    // Calculate percentage
    int baseScore = (matchedSkills * 100) / requiredSkills.Count;
    
    // Add level bonus (capped at 100)
    return Math.Min(baseScore + levelBonus, 100);
}
```

**4.2 Filtering Rules**
```csharp
public IEnumerable<TalentMatchDto> SearchTalents(TalentSearchDto criteria)
{
    var query = dbContext.TalentProfiles
        .Include(t => t.TalentSkills)
        .ThenInclude(ts => ts.Skill)
        .AsQueryable();
        
    // Rule 1: Filter by availability (AVAILABLE only)
    query = query.Where(t => t.AvailabilityStatus == "AVAILABLE");
    
    // Rule 2: Filter by department (if specified)
    if (!string.IsNullOrEmpty(criteria.Department))
        query = query.Where(t => t.Department == criteria.Department);
        
    // Rule 3: Filter by skills
    if (criteria.Skills?.Any() == true)
    {
        query = query.Where(t => t.TalentSkills
            .Any(ts => criteria.Skills.Contains(ts.SkillId)));
    }
    
    // Rule 4: Filter by minimum level
    // (Applied in memory after retrieving candidates)
    
    var talents = query.ToList();
    
    // Calculate scores and filter
    var results = talents
        .Select(t => new TalentMatchDto
        {
            TalentId = t.TalentId,
            FullName = t.FullName,
            MatchScore = CalculateMatchScore(t, criteria),
            // ... other fields
        })
        .Where(r => r.MatchScore >= criteria.MinimumMatchScore)
        .OrderByDescending(r => r.MatchScore)
        .ToList();
        
    return results;
}
```

**4.3 Mandatory vs Optional Skills**
```csharp
// Mandatory skills must ALL match
var mandatorySkills = project.ProjectSkills.Where(ps => ps.IsMandatory);
var hasAllMandatory = mandatorySkills.All(ms => 
    talent.TalentSkills.Any(ts => 
        ts.SkillId == ms.SkillId && 
        GetLevelIndex(ts.SkillLevel) >= GetLevelIndex(ms.MinimumLevel)));

if (!hasAllMandatory)
    return 0; // No match if missing mandatory skills

// Optional skills add to score
var optionalSkills = project.ProjectSkills.Where(ps => !ps.IsMandatory);
// Calculate optional match...
```

---

### 5. Skill Management

#### Rules

**5.1 Category Deletion**
```csharp
public void DeleteCategory(Guid categoryId)
{
    var category = GetCategory(categoryId);
    
    // Rule: Cannot delete if has skills
    if (category.Skills.Any())
        throw new BusinessException("Cannot delete category that has skills. Remove or reassign skills first.");
        
    Delete(category);
}
```

**5.2 Skill Deletion**
```csharp
public void DeleteSkill(Guid skillId)
{
    var skill = GetSkill(skillId);
    
    // Rule: Cannot delete if assigned to talents
    if (skill.TalentSkills.Any())
        throw new BusinessException("Cannot delete skill that is assigned to talents.");
        
    // Rule: Cannot delete if in project requirements
    if (skill.ProjectSkills.Any())
        throw new BusinessException("Cannot delete skill that is used in project requirements.");
        
    Delete(skill);
}
```

**5.3 Talent Skill Mapping**
```csharp
public void AddSkillToTalent(Guid talentId, AddTalentSkillDto dto)
{
    // Rule: No duplicate skills
    var exists = talentSkills.Any(ts => 
        ts.TalentId == talentId && ts.SkillId == dto.SkillId);
        
    if (exists)
        throw new BusinessException("Talent already has this skill.");
        
    // Add skill
    var talentSkill = new TalentSkill
    {
        TalentId = talentId,
        SkillId = dto.SkillId,
        SkillLevel = dto.SkillLevel,
        YearsOfExperience = dto.YearsOfExperience,
        LastUsedDate = dto.LastUsedDate
    };
    
    Save(talentSkill);
}
```

---

## 🛡️ Error Handling

### Standard Error Response Format

```json
{
  "error": {
    "code": "VALIDATION_ERROR",
    "message": "Validation failed",
    "details": [
      {
        "field": "email",
        "message": "Email is required"
      },
      {
        "field": "password",
        "message": "Password must be at least 8 characters"
      }
    ]
  },
  "timestamp": "2025-11-28T10:00:00Z",
  "path": "/api/auth/register"
}
```

### Error Codes

| Code | HTTP Status | Description |
|------|-------------|-------------|
| VALIDATION_ERROR | 400 | Input validation failed |
| BUSINESS_ERROR | 400 | Business rule violated |
| UNAUTHORIZED | 401 | Authentication failed |
| FORBIDDEN | 403 | Insufficient permissions |
| NOT_FOUND | 404 | Resource not found |
| CONFLICT | 409 | Duplicate resource |
| SERVER_ERROR | 500 | Internal server error |

---

## 📊 Validation Summary

### Input Validation Layers

1. **DTO Data Annotations**
   - Required fields
   - Length constraints
   - Format validation
   - Type validation

2. **FluentValidation (Optional)**
   - Complex business rules
   - Cross-field validation
   - Async validation

3. **Service Layer Validation**
   - Business logic validation
   - Database constraints
   - Authorization checks

4. **Database Constraints**
   - Unique constraints
   - Foreign key constraints
   - Check constraints

---

**Next:** [Development Guidelines](./09-DevelopmentGuidelines.md)
