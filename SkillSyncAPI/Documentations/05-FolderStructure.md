# Folder Structure (Clean Architecture)

Dokumen ini menjelaskan struktur folder SkillSync API yang menggunakan Clean Architecture.

---

## 📁 Complete Folder Structure

```
SkillSyncAPI/
│
├── Controllers/                          # Presentation Layer
│   ├── AuthController.cs                # Authentication & Registration
│   ├── UsersController.cs               # User management (Admin)
│   ├── RolesController.cs               # Role management (Admin)
│   ├── TalentsController.cs             # Talent management (HR, PM, Talent)
│   ├── SkillCategoriesController.cs     # Skill category CRUD (HR)
│   ├── SkillsController.cs              # Skills CRUD (HR)
│   ├── TalentSkillsController.cs        # Talent skill mapping (HR)
│   ├── ProjectsController.cs            # Project management (PM)
│   ├── ProjectSkillsController.cs       # Project skill requirements (PM)
│   └── ProjectAssignmentsController.cs  # Talent assignment (PM)
│
├── Services/                            # Business Logic Layer
│   ├── Interfaces/                      # Service contracts
│   │   ├── IAuthService.cs
│   │   ├── IUserService.cs
│   │   ├── IRoleService.cs
│   │   ├── ITalentService.cs
│   │   ├── ISkillCategoryService.cs
│   │   ├── ISkillService.cs
│   │   ├── ITalentSkillService.cs
│   │   ├── IProjectService.cs
│   │   ├── IProjectSkillService.cs
│   │   ├── IProjectAssignmentService.cs
│   │   └── ITalentMatchingService.cs
│   │
│   ├── AuthService.cs                   # JWT generation, login, register
│   ├── UserService.cs                   # User business logic
│   ├── RoleService.cs                   # Role business logic
│   ├── TalentService.cs                 # Talent management logic
│   ├── SkillCategoryService.cs          # Category management logic
│   ├── SkillService.cs                  # Skill management logic
│   ├── TalentSkillService.cs            # Skill mapping logic
│   ├── ProjectService.cs                # Project management logic
│   ├── ProjectSkillService.cs           # Project requirements logic
│   ├── ProjectAssignmentService.cs      # Assignment logic & validation
│   └── TalentMatchingService.cs         # Search & matching algorithm
│
├── Repositories/                        # Data Access Layer
│   ├── Interfaces/                      # Repository contracts
│   │   ├── IUserRepository.cs
│   │   ├── IRoleRepository.cs
│   │   ├── ITalentProfileRepository.cs
│   │   ├── ISkillCategoryRepository.cs
│   │   ├── ISkillRepository.cs
│   │   ├── ITalentSkillRepository.cs
│   │   ├── IProjectRepository.cs
│   │   ├── IProjectSkillRepository.cs
│   │   └── IProjectAssignmentRepository.cs
│   │
│   └── Data/                            # Repository implementations
│       ├── UserRepository.cs
│       ├── RoleRepository.cs
│       ├── TalentProfileRepository.cs
│       ├── SkillCategoryRepository.cs
│       ├── SkillRepository.cs
│       ├── TalentSkillRepository.cs
│       ├── ProjectRepository.cs
│       ├── ProjectSkillRepository.cs
│       └── ProjectAssignmentRepository.cs
│
├── DTOs/                                # Data Transfer Objects
│   ├── Auth/
│   │   ├── LoginDto.cs
│   │   ├── RegisterDto.cs
│   │   └── AuthResponseDto.cs
│   │
│   ├── Users/
│   │   ├── UserDto.cs
│   │   ├── CreateUserDto.cs
│   │   └── UpdateUserDto.cs
│   │
│   ├── Roles/
│   │   ├── RoleDto.cs
│   │   └── CreateRoleDto.cs
│   │
│   ├── Talents/
│   │   ├── TalentProfileDto.cs
│   │   ├── UpdateTalentDto.cs
│   │   └── UpdateAvailabilityDto.cs
│   │
│   ├── Skills/
│   │   ├── SkillCategoryDto.cs
│   │   ├── CreateSkillCategoryDto.cs
│   │   ├── SkillDto.cs
│   │   └── CreateSkillDto.cs
│   │
│   ├── TalentSkills/
│   │   ├── TalentSkillDto.cs
│   │   ├── AddTalentSkillDto.cs
│   │   └── UpdateSkillLevelDto.cs
│   │
│   ├── Projects/
│   │   ├── ProjectDto.cs
│   │   ├── CreateProjectDto.cs
│   │   ├── UpdateProjectDto.cs
│   │   └── ProjectDetailDto.cs
│   │
│   ├── ProjectSkills/
│   │   ├── ProjectSkillDto.cs
│   │   └── AddProjectSkillDto.cs
│   │
│   ├── ProjectAssignments/
│   │   ├── ProjectAssignmentDto.cs
│   │   ├── AssignTalentDto.cs
│   │   └── UpdateAssignmentDto.cs
│   │
│   └── Search/
│       ├── TalentSearchDto.cs
│       ├── SearchResultDto.cs
│       └── TalentMatchDto.cs
│
├── Models/                              # Domain Entities
│   ├── Users.cs
│   ├── Roles.cs
│   ├── TalentProfiles.cs
│   ├── Skills.cs
│   ├── SkillCategories.cs
│   ├── TalentSkills.cs
│   ├── Projects.cs
│   ├── ProjectSkills.cs
│   └── ProjectAssignments.cs
│
├── Data/                                # Database Context & Configuration
│   ├── SkillSyncDbContext.cs
│   ├── SkillSyncDataSeeder.cs
│   │
│   └── Configurations/                  # Entity configurations
│       ├── UsersConfiguration.cs
│       ├── RolesConfiguration.cs
│       ├── TalentProfilesConfiguration.cs
│       ├── SkillsConfiguration.cs
│       ├── SkillCategoriesConfiguration.cs
│       ├── TalentSkillsConfiguration.cs
│       ├── ProjectsConfiguration.cs
│       ├── ProjectSkillsConfiguration.cs
│       └── ProjectAssignmentsConfiguration.cs
│
├── Migrations/                          # EF Core Migrations
│   ├── 20251125033515_InitialMigration.cs
│   ├── 20251125090524_ConfigureMigrations.cs
│   └── SkillSyncDbContextModelSnapshot.cs
│
├── Middleware/                          # Custom Middleware
│   ├── ExceptionHandlingMiddleware.cs
│   └── RequestLoggingMiddleware.cs
│
├── Helpers/                             # Utility classes
│   ├── JwtHelper.cs
│   ├── PasswordHelper.cs
│   └── AutoMapperProfile.cs
│
├── Documentations/                      # Project documentation
│   ├── README.md
│   ├── 01-ProjectOverview.md
│   ├── 02-RBAC.md
│   ├── 03-KeyFeatures.md
│   ├── 04-UserFlows.md
│   ├── 05-FolderStructure.md
│   ├── 06-APIEndpoints.md
│   ├── 07-DataModels.md
│   ├── 08-SecurityAndBusinessRules.md
│   ├── 09-DevelopmentGuidelines.md
│   └── 10-FutureEnhancements.md
│
├── appsettings.json                     # Configuration
├── appsettings.Development.json
├── Program.cs                           # Application entry point
└── SkillSyncAPI.csproj                  # Project file
```

---

## 🏛️ Layer Responsibilities

### 1. Controllers (Presentation Layer)

**Location:** `/Controllers`

#### Tanggung Jawab
- ✅ Menerima HTTP requests dari client
- ✅ Validasi input menggunakan model binding & validation attributes
- ✅ Authorization checks menggunakan `[Authorize]` attributes
- ✅ Memanggil Services untuk business logic
- ✅ Return HTTP responses dengan proper status codes & data
- ✅ Handle routing

#### Tidak Boleh
- ❌ Mengandung business logic
- ❌ Direct database access
- ❌ Complex data transformation
- ❌ Direct repository calls

#### Example Structure
```csharp
[ApiController]
[Route("api/[controller]")]
public class TalentsController : ControllerBase
{
    private readonly ITalentService _talentService;
    
    public TalentsController(ITalentService talentService)
    {
        _talentService = talentService;
    }
    
    [HttpGet]
    [Authorize(Roles = "HR,PM")]
    public async Task<IActionResult> GetAllTalents()
    {
        var talents = await _talentService.GetAllTalentsAsync();
        return Ok(talents);
    }
}
```

#### Controllers Mapping

| Controller | Primary Role | Endpoints |
|------------|--------------|-----------|
| AuthController | All | Login, Register, Refresh Token |
| UsersController | Admin | User CRUD |
| RolesController | Admin | Role CRUD |
| TalentsController | HR, PM, Talent | Talent management & viewing |
| SkillCategoriesController | HR | Category CRUD |
| SkillsController | HR | Skill CRUD |
| TalentSkillsController | HR | Skill mapping |
| ProjectsController | PM | Project CRUD |
| ProjectSkillsController | PM | Project requirements |
| ProjectAssignmentsController | PM | Talent assignment |

---

### 2. Services (Business Logic Layer)

**Location:** `/Services` dan `/Services/Interfaces`

#### Tanggung Jawab
- ✅ Implementasi business rules dan logic
- ✅ Data validation & business validation
- ✅ Orchestration - koordinasi multiple repositories
- ✅ Complex calculations & algorithms (matching, scoring)
- ✅ DTO mapping (entities ↔ DTOs) menggunakan AutoMapper
- ✅ Transaction management
- ✅ Error handling & custom exceptions

#### Tidak Boleh
- ❌ Direct database queries (gunakan repositories)
- ❌ HTTP-specific logic (status codes, routing)
- ❌ Direct HttpContext access

#### Example Structure
```csharp
public interface ITalentService
{
    Task<IEnumerable<TalentProfileDto>> GetAllTalentsAsync();
    Task<TalentProfileDto> GetTalentByIdAsync(Guid id);
    Task UpdateTalentAsync(Guid id, UpdateTalentDto dto);
    Task UpdateAvailabilityAsync(Guid id, UpdateAvailabilityDto dto);
}

public class TalentService : ITalentService
{
    private readonly ITalentProfileRepository _repository;
    private readonly IMapper _mapper;
    
    public TalentService(
        ITalentProfileRepository repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task UpdateAvailabilityAsync(Guid id, UpdateAvailabilityDto dto)
    {
        // Business validation
        var talent = await _repository.GetByIdAsync(id);
        if (talent == null)
            throw new NotFoundException("Talent not found");
            
        // Business rule: Cannot manually set to ON_PROJECT
        if (dto.AvailabilityStatus == "ON_PROJECT")
            throw new BusinessException("Cannot manually set to ON_PROJECT");
            
        // Update
        talent.AvailabilityStatus = dto.AvailabilityStatus;
        await _repository.UpdateAsync(talent);
    }
}
```

#### Services Mapping

| Service | Responsibilities |
|---------|------------------|
| AuthService | JWT generation, password hashing, login validation |
| UserService | User CRUD, role assignment validation |
| RoleService | Role CRUD, permission management |
| TalentService | Talent CRUD, availability management |
| SkillCategoryService | Category CRUD, validation |
| SkillService | Skill CRUD, category linking |
| TalentSkillService | Skill mapping, level validation |
| ProjectService | Project CRUD, ownership validation |
| ProjectSkillService | Requirements management |
| ProjectAssignmentService | Assignment logic, availability updates |
| TalentMatchingService | Search algorithm, scoring, filtering |

---

### 3. Repositories (Data Access Layer)

**Location:** `/Repositories/Data` dan `/Repositories/Interfaces`

#### Tanggung Jawab
- ✅ CRUD operations ke database
- ✅ Complex queries & filtering
- ✅ Data retrieval dengan EF Core
- ✅ Include/Join related entities (eager loading)
- ✅ Pagination & sorting
- ✅ Raw SQL queries (if needed)

#### Tidak Boleh
- ❌ Business logic
- ❌ DTO mapping
- ❌ Authorization logic
- ❌ Complex calculations

#### Example Structure
```csharp
public interface ITalentProfileRepository
{
    Task<IEnumerable<TalentProfile>> GetAllAsync();
    Task<TalentProfile> GetByIdAsync(Guid id);
    Task<TalentProfile> GetByIdWithSkillsAsync(Guid id);
    Task<IEnumerable<TalentProfile>> GetAvailableTalentsAsync();
    Task UpdateAsync(TalentProfile talent);
    Task<bool> ExistsAsync(Guid id);
}

public class TalentProfileRepository : ITalentProfileRepository
{
    private readonly SkillSyncDbContext _context;
    
    public TalentProfileRepository(SkillSyncDbContext context)
    {
        _context = context;
    }
    
    public async Task<TalentProfile> GetByIdWithSkillsAsync(Guid id)
    {
        return await _context.TalentProfiles
            .Include(t => t.TalentSkills)
                .ThenInclude(ts => ts.Skill)
                    .ThenInclude(s => s.Category)
            .FirstOrDefaultAsync(t => t.TalentId == id);
    }
    
    public async Task<IEnumerable<TalentProfile>> GetAvailableTalentsAsync()
    {
        return await _context.TalentProfiles
            .Where(t => t.AvailabilityStatus == "AVAILABLE")
            .ToListAsync();
    }
}
```

#### Repositories Mapping

| Repository | Entity | Complex Queries |
|------------|--------|-----------------|
| UserRepository | Users | GetByUsername, GetByEmail |
| RoleRepository | Roles | GetByName |
| TalentProfileRepository | TalentProfiles | GetAvailable, GetByDepartment |
| SkillCategoryRepository | SkillCategories | GetWithSkills |
| SkillRepository | Skills | GetByCategory, SearchByName |
| TalentSkillRepository | TalentSkills | GetByTalent, GetBySkill |
| ProjectRepository | Projects | GetByPM, GetByStatus |
| ProjectSkillRepository | ProjectSkills | GetByProject |
| ProjectAssignmentRepository | ProjectAssignments | GetActiveByTalent, GetByProject |

---

### 4. DTOs (Data Transfer Objects)

**Location:** `/DTOs` (organized by feature)

#### Tanggung Jawab
- ✅ Define data contracts untuk API
- ✅ Input validation attributes (`[Required]`, `[MaxLength]`, etc.)
- ✅ Shape response data
- ✅ Prevent over-posting attacks
- ✅ Hide sensitive data (e.g., password)
- ✅ API versioning support

#### Tidak Boleh
- ❌ Business logic
- ❌ Database annotations
- ❌ Navigation properties

#### Example Structure
```csharp
// Input DTO
public class UpdateTalentDto
{
    [Required]
    [MaxLength(100)]
    public string FullName { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Phone]
    public string Phone { get; set; }
    
    [MaxLength(50)]
    public string Department { get; set; }
    
    [MaxLength(50)]
    public string Position { get; set; }
}

// Output DTO
public class TalentProfileDto
{
    public Guid TalentId { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Department { get; set; }
    public string Position { get; set; }
    public string AvailabilityStatus { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    // No sensitive data like UserId or passwords
}
```

#### DTOs Organization

| Folder | Purpose | Examples |
|--------|---------|----------|
| Auth/ | Authentication & Authorization | LoginDto, RegisterDto, AuthResponseDto |
| Users/ | User management | UserDto, CreateUserDto, UpdateUserDto |
| Roles/ | Role management | RoleDto, CreateRoleDto |
| Talents/ | Talent management | TalentProfileDto, UpdateTalentDto |
| Skills/ | Skill & category management | SkillDto, SkillCategoryDto |
| TalentSkills/ | Skill mapping | TalentSkillDto, AddTalentSkillDto |
| Projects/ | Project management | ProjectDto, CreateProjectDto |
| ProjectSkills/ | Project requirements | ProjectSkillDto, AddProjectSkillDto |
| ProjectAssignments/ | Talent assignment | ProjectAssignmentDto, AssignTalentDto |
| Search/ | Search & matching | TalentSearchDto, SearchResultDto |

---

### 5. Models (Domain Entities)

**Location:** `/Models`

#### Tanggung Jawab
- ✅ Represent database tables
- ✅ Define relationships (navigation properties)
- ✅ Data annotations untuk EF Core mapping
- ✅ Track timestamps (CreatedAt, UpdatedAt)
- ✅ Primary keys & foreign keys

#### Tidak Boleh
- ❌ Business logic methods
- ❌ Validation logic
- ❌ DTO-specific properties

#### Example Structure
```csharp
public class TalentProfile
{
    public Guid TalentId { get; set; }
    public Guid UserId { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Department { get; set; }
    public string Position { get; set; }
    public string AvailabilityStatus { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    // Navigation properties
    public User User { get; set; }
    public ICollection<TalentSkill> TalentSkills { get; set; }
    public ICollection<ProjectAssignment> ProjectAssignments { get; set; }
}
```

---

### 6. Data (Database Context & Configuration)

**Location:** `/Data` dan `/Data/Configurations`

#### Tanggung Jawab
- ✅ DbContext configuration
- ✅ DbSet definitions
- ✅ Entity configurations (Fluent API)
- ✅ Relationship configurations
- ✅ Index configurations
- ✅ Seed data

#### Example Structure
```csharp
// DbContext
public class SkillSyncDbContext : DbContext
{
    public SkillSyncDbContext(DbContextOptions<SkillSyncDbContext> options)
        : base(options) { }
    
    public DbSet<User> Users { get; set; }
    public DbSet<TalentProfile> TalentProfiles { get; set; }
    public DbSet<Skill> Skills { get; set; }
    // ... other DbSets
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SkillSyncDbContext).Assembly);
    }
}

// Entity Configuration
public class TalentProfilesConfiguration : IEntityTypeConfiguration<TalentProfile>
{
    public void Configure(EntityTypeBuilder<TalentProfile> builder)
    {
        builder.HasKey(t => t.TalentId);
        
        builder.Property(t => t.FullName)
            .IsRequired()
            .HasMaxLength(100);
            
        builder.HasIndex(t => t.Email).IsUnique();
        
        builder.HasOne(t => t.User)
            .WithOne()
            .HasForeignKey<TalentProfile>(t => t.UserId);
    }
}
```

---

### 7. Middleware

**Location:** `/Middleware`

#### Tanggung Jawab
- ✅ Global exception handling
- ✅ Request/Response logging
- ✅ Authentication validation
- ✅ Performance monitoring
- ✅ CORS handling

#### Example Files
- `ExceptionHandlingMiddleware.cs` - Catch & format exceptions
- `RequestLoggingMiddleware.cs` - Log requests & responses

---

### 8. Helpers

**Location:** `/Helpers`

#### Tanggung Jawab
- ✅ Utility functions
- ✅ JWT token generation & validation
- ✅ Password hashing & verification
- ✅ AutoMapper profiles
- ✅ Common extensions

#### Example Files
- `JwtHelper.cs` - JWT operations
- `PasswordHelper.cs` - Password hashing
- `AutoMapperProfile.cs` - DTO mappings

---

## 🔄 Data Flow

### Request Flow (Top to Bottom)
```
HTTP Request
    ↓
Controller (Presentation Layer)
    - Receive request
    - Validate input (Data Annotations)
    - Check authorization
    ↓
Service (Business Logic Layer)
    - Business validation
    - Business rules
    - Orchestrate repositories
    - DTO mapping
    ↓
Repository (Data Access Layer)
    - Database queries
    - EF Core operations
    ↓
Database (PostgreSQL)
    - Store/Retrieve data
```

### Response Flow (Bottom to Top)
```
Database
    ↓
Repository
    - Return entities
    ↓
Service
    - Map entities to DTOs
    - Apply business logic
    - Return DTOs
    ↓
Controller
    - Format HTTP response
    - Set status codes
    ↓
HTTP Response
```

---

## 📦 Dependency Injection Setup

```csharp
// In Program.cs
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<ITalentProfileRepository, TalentProfileRepository>();
builder.Services.AddScoped<ITalentService, TalentService>();

builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IProjectService, ProjectService>();

builder.Services.AddScoped<ITalentMatchingService, TalentMatchingService>();

builder.Services.AddAutoMapper(typeof(Program));
```

---

**Next:** [API Endpoints](./06-APIEndpoints.md)
