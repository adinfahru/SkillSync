# Key Features (MVP Scope)

Dokumen ini menjelaskan 8 fitur utama dalam MVP SkillSync API.

---

## 1. Authentication & Authorization

### JWT-based Authentication

**Features:**
- Secure token generation dan validation
- Token expiration & refresh mechanism
- Password hashing menggunakan industry standard (BCrypt/PBKDF2)
- Stateless authentication

**Endpoints:**
- `POST /api/auth/register` - Register new user
- `POST /api/auth/login` - Login & get JWT token
- `POST /api/auth/refresh` - Refresh JWT token

**Token Structure:**
```json
{
  "accessToken": "eyJhbGciOiJIUzI1NiIs...",
  "refreshToken": "dGhpcyBpcyByZWZyZXNo...",
  "expiresIn": 3600,
  "tokenType": "Bearer"
}
```

### Role-Based Authorization

**Implementation:**
- Endpoint protection per role menggunakan `[Authorize]` attributes
- Claim-based authorization untuk granular control
- Custom authorization policies untuk complex rules

**Example:**
```csharp
[Authorize(Roles = "HR")]
[Authorize(Policy = "ProjectOwner")]
```

---

## 2. Talent Management (HR Only)

### Update Talent Profile

**HR dapat mengupdate:**
- Full Name
- Email
- Phone
- Department
- Position

**Validations:**
- Email harus unique
- Phone format validation
- Required fields validation

**Endpoint:**
```
PUT /api/talents/{id}
```

### Manage Availability Status

**Status Options:**
| Status | Description | Set By |
|--------|-------------|--------|
| `AVAILABLE` | Talent tersedia untuk project baru | HR (manual) / System (auto) |
| `ON_PROJECT` | Sedang mengerjakan project | System (auto saat assign) |
| `ON_LEAVE` | Cuti/tidak tersedia | HR (manual) |

**Features:**
- Track availability history (timestamps)
- Otomatis update saat talent di-assign/remove dari project
- Prevent manual set to ON_PROJECT (harus via assignment)

**Endpoint:**
```
PUT /api/talents/{id}/availability
```

**Request Body:**
```json
{
  "availabilityStatus": "ON_LEAVE"
}
```

### Manage Talent Skill Mapping

**Features:**
- Add skill ke talent profile
- Remove skill dari talent profile
- Set/Update skill level
- Track years of experience
- Track last used date

**Skill Levels:**
- `Beginner` - 0-1 years
- `Intermediate` - 1-3 years
- `Advanced` - 3-5 years
- `Expert` - 5+ years

**Endpoints:**
```
GET    /api/talents/{id}/skills         # Get talent skills
POST   /api/talents/{id}/skills         # Add skill
PUT    /api/talents/{id}/skills/{skillId}  # Update level
DELETE /api/talents/{id}/skills/{skillId}  # Remove skill
```

**Add Skill Request:**
```json
{
  "skillId": "skill-guid",
  "skillLevel": "Intermediate",
  "yearsOfExperience": 2.5,
  "lastUsedDate": "2025-11-01"
}
```

---

## 3. Skill Master Data (HR Only)

### Skill Categories Management

**Purpose:** Organize skills into logical groups

**Examples:**
- Backend Development
- Frontend Development
- Mobile Development
- DevOps & Infrastructure
- Testing & QA
- Database
- Cloud Services

**Endpoints:**
```
GET    /api/skill-categories       # Get all
POST   /api/skill-categories       # Create
PUT    /api/skill-categories/{id}  # Update
DELETE /api/skill-categories/{id}  # Delete (if not used)
```

**Request Body:**
```json
{
  "categoryName": "Backend Development",
  "description": "Server-side programming skills"
}
```

### Skills Management

**Purpose:** Define specific skills within categories

**Examples:**
- C# (.NET Core, ASP.NET Core)
- JavaScript (React, Angular, Node.js)
- Python (Django, FastAPI)
- PostgreSQL, MySQL
- Docker, Kubernetes

**Endpoints:**
```
GET    /api/skills       # Get all
POST   /api/skills       # Create
PUT    /api/skills/{id}  # Update
DELETE /api/skills/{id}  # Delete (if not assigned)
```

**Request Body:**
```json
{
  "skillName": "ASP.NET Core",
  "categoryId": "category-guid",
  "description": "Modern .NET web framework"
}
```

**Validations:**
- Skill name must be unique
- Category must exist
- Cannot delete skill if assigned to talents
- Cannot delete category if has skills

---

## 4. Project Management (PM Only)

### Create Project

**Features:**
- Define project name dan description
- Set initial status (default: Planning)
- PM otomatis menjadi owner project
- Set start & end dates

**Endpoint:**
```
POST /api/projects
```

**Request Body:**
```json
{
  "projectName": "E-Commerce Platform",
  "description": "Build modern e-commerce system",
  "status": "Planning",
  "startDate": "2025-12-01",
  "endDate": "2026-03-31"
}
```

### Update Project

**Features:**
- Update project name & description
- Update status: `Planning` → `Ongoing` → `Completed` / `On-Hold`
- Update timeline
- **Only PM owner can update**

**Endpoint:**
```
PUT /api/projects/{id}
```

**Status Transitions:**
```
Planning → Ongoing ✅
Ongoing → Completed ✅
Ongoing → On-Hold ✅
On-Hold → Ongoing ✅
Completed → (any) ❌ (final state)
```

### Delete Project

**Features:**
- Soft delete atau hard delete (configurable)
- **Only PM owner can delete**
- **Validation:** Tidak ada active assignment

**Endpoint:**
```
DELETE /api/projects/{id}
```

**Validation Rules:**
- Project must be owned by current PM
- No active assignments (ReleaseDate must be set for all)
- Status should be Planning or On-Hold (recommended)

---

## 5. Project Skill Requirements (PM Only)

### Set Skill Requirements

**Purpose:** Define skills needed for project

**Features:**
- Define multiple skills per project
- Set minimal skill level per skill
- Mark skills as mandatory/optional
- Used for talent matching

**Endpoint:**
```
POST /api/projects/{id}/skills
```

**Request Body:**
```json
{
  "skillId": "skill-guid",
  "minimumLevel": "Intermediate",
  "isMandatory": true
}
```

### Update Requirements

**Features:**
- Add new skill requirements
- Remove skill requirements
- Adjust minimal skill level
- Change mandatory flag

**Endpoints:**
```
PUT    /api/projects/{id}/skills/{skillId}  # Update
DELETE /api/projects/{id}/skills/{skillId}  # Remove
```

**Use Case:**
Project "E-Commerce Platform" membutuhkan:
- ASP.NET Core (Minimum: Advanced, Mandatory)
- PostgreSQL (Minimum: Intermediate, Mandatory)
- React (Minimum: Intermediate, Optional)
- Docker (Minimum: Beginner, Optional)

---

## 6. Talent Matching Engine (PM Only)

### Search Criteria

**Filter By:**
- ✅ Skills (match with project requirements)
- ✅ Minimal skill level (equal or higher)
- ✅ Availability status (AVAILABLE only)
- ✅ Department (optional)
- ✅ Years of experience (optional)

**Endpoint:**
```
POST /api/talents/search
GET  /api/talents/match/{projectId}
```

### Search Features

**Matching Algorithm:**
1. Filter talents yang AVAILABLE
2. Check skill matches dengan requirements
3. Validate skill level >= minimum required
4. Calculate match score
5. Sort by relevance (highest match first)

**Match Score Calculation:**
```
Match Score = (Matched Skills / Total Required Skills) × 100
+ Bonus for higher skill levels
+ Bonus for more years of experience
```

**Display Match Percentage:**
```
Example:
Project needs: C#, PostgreSQL, React, Docker
Talent has: C# (Advanced), PostgreSQL (Expert), React (Beginner)

Match: 3/4 skills = 75% base
+ C# level bonus = +5%
+ PostgreSQL level bonus = +10%
Total Score: 90%
```

### Search Request Example

```json
{
  "skills": ["skill-id-1", "skill-id-2"],
  "minimumLevels": {
    "skill-id-1": "Intermediate",
    "skill-id-2": "Advanced"
  },
  "availabilityStatus": "AVAILABLE",
  "department": "Engineering"
}
```

### Response Data

```json
{
  "results": [
    {
      "talentId": "talent-guid",
      "fullName": "John Doe",
      "department": "Engineering",
      "position": "Senior Developer",
      "availabilityStatus": "AVAILABLE",
      "matchScore": 90,
      "matchedSkills": [
        {
          "skillName": "C#",
          "talentLevel": "Advanced",
          "requiredLevel": "Intermediate",
          "yearsOfExperience": 5
        }
      ],
      "missingSkills": ["Docker"]
    }
  ],
  "totalResults": 5,
  "searchCriteria": { }
}
```

---

## 7. Project Talent Assignment (PM Only)

### Assign Talent

**Features:**
- Assign AVAILABLE talent ke project
- Set role_on_project
- **Otomatis update** availability_status → ON_PROJECT
- Track assignment date

**Validations:**
1. ✅ Talent harus AVAILABLE
2. ✅ Tidak boleh duplicate assignment (same project)
3. ✅ Project harus owned by current PM
4. ✅ Project status harus Planning/Ongoing/On-Hold (not Completed)

**Endpoint:**
```
POST /api/projects/{id}/assignments
```

**Request Body:**
```json
{
  "talentId": "talent-guid",
  "roleOnProject": "Backend Developer"
}
```

**Role on Project Examples:**
- Backend Developer
- Frontend Developer
- Full Stack Developer
- QA Engineer
- DevOps Engineer
- UI/UX Designer
- Tech Lead
- Scrum Master

### Update Assignment

**Features:**
- Update role_on_project
- Update assignment period (optional)
- Cannot change talent (must remove & create new)

**Endpoint:**
```
PUT /api/projects/{id}/assignments/{assignmentId}
```

### Remove Talent

**Features:**
- Remove talent dari project
- Set ReleaseDate
- **Otomatis update** availability_status → AVAILABLE
- Keep assignment history (soft delete)

**Endpoint:**
```
DELETE /api/projects/{id}/assignments/{assignmentId}
```

**Business Logic:**
```
When assigned:
  - Set AssignedDate = NOW
  - Set ReleaseDate = NULL
  - Update Talent.AvailabilityStatus = ON_PROJECT

When removed:
  - Set ReleaseDate = NOW
  - Update Talent.AvailabilityStatus = AVAILABLE
  - Keep record for history
```

---

## 8. Read-Only Access

### Project Manager Access

**Can View (Read-Only):**
- ✅ All talent profiles
- ✅ Talent skills & skill levels
- ✅ Talent availability status
- ✅ All skill categories & skills
- ✅ All projects (but can only modify own)

**Purpose:**
- Research talents for project staffing
- View skill landscape
- Make informed assignment decisions

**Endpoints:**
```
GET /api/talents              # All talents
GET /api/talents/{id}         # Specific talent
GET /api/skills               # All skills
GET /api/skill-categories     # All categories
```

### Talent Access

**Can View (Read-Only):**
- ✅ Own profile information
- ✅ Own skills & skill levels
- ✅ Active project assignments
- ✅ Project details (assigned projects only)
- ✅ Assignment history

**Purpose:**
- Self-service information
- Track own skills & development
- Monitor project assignments
- Career development insights

**Endpoints:**
```
GET /api/me/profile           # Own profile
GET /api/me/skills            # Own skills
GET /api/me/assignments       # Own assignments
```

**Response Example:**
```json
{
  "profile": {
    "fullName": "John Doe",
    "email": "john@company.com",
    "department": "Engineering",
    "position": "Senior Developer",
    "availabilityStatus": "ON_PROJECT"
  },
  "skills": [
    {
      "skillName": "C#",
      "categoryName": "Backend Development",
      "level": "Advanced",
      "yearsOfExperience": 5
    }
  ],
  "activeAssignments": [
    {
      "projectName": "E-Commerce Platform",
      "roleOnProject": "Backend Developer",
      "assignedDate": "2025-11-01",
      "projectManager": "Jane Smith"
    }
  ]
}
```

---

## 🎯 Feature Priority for MVP

### Must Have (P0)
1. ✅ Authentication & Authorization
2. ✅ Talent Management (basic CRUD)
3. ✅ Skills Management
4. ✅ Project Management
5. ✅ Basic Assignment

### Should Have (P1)
6. ✅ Talent Skill Mapping
7. ✅ Project Skill Requirements
8. ✅ Basic Talent Search

### Nice to Have (P2)
9. ✅ Advanced Matching Algorithm
10. ✅ Match Score Calculation
11. ✅ Read-Only Access Endpoints

### Post-MVP (P3)
- Skill Endorsements
- Performance Metrics
- Advanced Analytics
- Notification System
- Training Recommendations

---

**Next:** [User Flows](./04-UserFlows.md)
