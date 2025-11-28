# Development Guidelines

Panduan development untuk SkillSync API project.

---

## 🏗️ Code Organization

### 1. Project Structure

Ikuti Clean Architecture pattern dengan strict layer separation:

```
Controllers → Services → Repositories → Database
```

**Rules:**
- ✅ Controllers call Services (not Repositories)
- ✅ Services call Repositories (not DbContext directly)
- ✅ Use Dependency Injection untuk semua dependencies
- ❌ Never skip layers (e.g., Controller → Repository)

---

### 2. Dependency Injection

#### Service Registration (Program.cs)

```csharp
// DbContext
builder.Services.AddDbContext<SkillSyncDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITalentProfileRepository, TalentProfileRepository>();
builder.Services.AddScoped<ISkillRepository, SkillRepository>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();

// Services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITalentService, TalentService>();
builder.Services.AddScoped<ISkillService, SkillService>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<ITalentMatchingService, TalentMatchingService>();

// AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

// Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });
```

#### Constructor Injection Pattern

```csharp
public class TalentService : ITalentService
{
    private readonly ITalentProfileRepository _talentRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<TalentService> _logger;
    
    public TalentService(
        ITalentProfileRepository talentRepository,
        IMapper mapper,
        ILogger<TalentService> logger)
    {
        _talentRepository = talentRepository;
        _mapper = mapper;
        _logger = logger;
    }
    
    // Methods...
}
```

---

## 📝 Naming Conventions

### 1. General Rules

| Element | Convention | Example |
|---------|------------|---------|
| Namespace | PascalCase | `SkillSyncAPI.Services` |
| Class | PascalCase | `TalentService` |
| Interface | PascalCase + I prefix | `ITalentService` |
| Method | PascalCase | `GetAllTalentsAsync` |
| Property | PascalCase | `FullName` |
| Private Field | camelCase + _ prefix | `_talentRepository` |
| Parameter | camelCase | `talentId` |
| Local Variable | camelCase | `matchScore` |
| Constant | PascalCase | `MaxPageSize` |

### 2. File Names

```
Controllers:    TalentsController.cs
Services:       TalentService.cs
Interfaces:     ITalentService.cs
Repositories:   TalentProfileRepository.cs
DTOs:          TalentProfileDto.cs
Models:        TalentProfile.cs
```

### 3. Async Method Naming

```csharp
// ✅ Good - Suffix with Async
public async Task<TalentProfileDto> GetTalentByIdAsync(Guid id)
public async Task UpdateTalentAsync(Guid id, UpdateTalentDto dto)
public async Task<IEnumerable<TalentProfileDto>> GetAllTalentsAsync()

// ❌ Bad - Missing Async suffix
public async Task<TalentProfileDto> GetTalentById(Guid id)
```

---

## 🎯 DTOs & Mapping

### 1. AutoMapper Configuration

#### AutoMapper Profile

```csharp
public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // TalentProfile mappings
        CreateMap<TalentProfile, TalentProfileDto>();
        CreateMap<UpdateTalentDto, TalentProfile>()
            .ForMember(dest => dest.TalentId, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());
            
        // Skill mappings
        CreateMap<Skill, SkillDto>()
            .ForMember(dest => dest.CategoryName, 
                opt => opt.MapFrom(src => src.Category.CategoryName));
        CreateMap<CreateSkillDto, Skill>();
        
        // Project mappings
        CreateMap<Project, ProjectDto>()
            .ForMember(dest => dest.ProjectManagerName,
                opt => opt.MapFrom(src => src.ProjectManager.Username));
        CreateMap<CreateProjectDto, Project>()
            .ForMember(dest => dest.ProjectManagerId, opt => opt.Ignore());
    }
}
```

#### Usage in Services

```csharp
public async Task<TalentProfileDto> GetTalentByIdAsync(Guid id)
{
    var talent = await _talentRepository.GetByIdAsync(id);
    
    if (talent == null)
        throw new NotFoundException("Talent not found");
        
    return _mapper.Map<TalentProfileDto>(talent);
}

public async Task UpdateTalentAsync(Guid id, UpdateTalentDto dto)
{
    var talent = await _talentRepository.GetByIdAsync(id);
    
    if (talent == null)
        throw new NotFoundException("Talent not found");
        
    _mapper.Map(dto, talent);
    await _talentRepository.UpdateAsync(talent);
}
```

### 2. DTO Organization

```
DTOs/
  Auth/
    LoginDto.cs              # Input
    RegisterDto.cs           # Input
    AuthResponseDto.cs       # Output
  Talents/
    TalentProfileDto.cs      # Output
    UpdateTalentDto.cs       # Input
    UpdateAvailabilityDto.cs # Input
```

### 3. DTO Design Patterns

**Input DTOs:**
```csharp
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
```

**Output DTOs:**
```csharp
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
}
```

---

## ⚠️ Error Handling

### 1. Custom Exceptions

```csharp
// Base exception
public class AppException : Exception
{
    public AppException(string message) : base(message) { }
}

// Specific exceptions
public class NotFoundException : AppException
{
    public NotFoundException(string message) : base(message) { }
}

public class BusinessException : AppException
{
    public BusinessException(string message) : base(message) { }
}

public class UnauthorizedException : AppException
{
    public UnauthorizedException(string message) : base(message) { }
}

public class ForbiddenException : AppException
{
    public ForbiddenException(string message) : base(message) { }
}
```

### 2. Global Exception Handler Middleware

```csharp
public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;
    
    public ExceptionHandlingMiddleware(
        RequestDelegate next,
        ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }
    
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }
    
    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        
        var response = new ErrorResponse
        {
            Timestamp = DateTime.UtcNow,
            Path = context.Request.Path
        };
        
        switch (exception)
        {
            case NotFoundException notFound:
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                response.Error = new ErrorDetail
                {
                    Code = "NOT_FOUND",
                    Message = notFound.Message
                };
                break;
                
            case BusinessException business:
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                response.Error = new ErrorDetail
                {
                    Code = "BUSINESS_ERROR",
                    Message = business.Message
                };
                break;
                
            case UnauthorizedException unauthorized:
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                response.Error = new ErrorDetail
                {
                    Code = "UNAUTHORIZED",
                    Message = unauthorized.Message
                };
                break;
                
            case ForbiddenException forbidden:
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                response.Error = new ErrorDetail
                {
                    Code = "FORBIDDEN",
                    Message = forbidden.Message
                };
                break;
                
            default:
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                _logger.LogError(exception, "Unhandled exception");
                response.Error = new ErrorDetail
                {
                    Code = "SERVER_ERROR",
                    Message = "An internal error occurred"
                };
                break;
        }
        
        await context.Response.WriteAsJsonAsync(response);
    }
}
```

### 3. Usage in Services

```csharp
public async Task<TalentProfileDto> GetTalentByIdAsync(Guid id)
{
    var talent = await _talentRepository.GetByIdAsync(id);
    
    if (talent == null)
        throw new NotFoundException($"Talent with ID {id} not found");
        
    return _mapper.Map<TalentProfileDto>(talent);
}

public async Task AssignTalent(AssignTalentDto dto)
{
    var talent = await _talentRepository.GetByIdAsync(dto.TalentId);
    
    if (talent.AvailabilityStatus != "AVAILABLE")
        throw new BusinessException("Talent must be AVAILABLE to be assigned");
        
    // Proceed...
}
```

---

## ✅ Validation

### 1. Data Annotations

```csharp
public class CreateSkillDto
{
    [Required(ErrorMessage = "Skill name is required")]
    [StringLength(100, MinimumLength = 2, 
        ErrorMessage = "Skill name must be between 2 and 100 characters")]
    public string SkillName { get; set; }
    
    [Required(ErrorMessage = "Category is required")]
    public Guid CategoryId { get; set; }
    
    [MaxLength(255)]
    public string Description { get; set; }
}
```

### 2. FluentValidation (Optional)

```csharp
public class UpdateTalentDtoValidator : AbstractValidator<UpdateTalentDto>
{
    public UpdateTalentDtoValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("Full name is required")
            .MaximumLength(100).WithMessage("Full name cannot exceed 100 characters");
            
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email format");
            
        RuleFor(x => x.Phone)
            .Matches(@"^\+?[1-9]\d{1,14}$")
            .When(x => !string.IsNullOrEmpty(x.Phone))
            .WithMessage("Invalid phone number format");
    }
}
```

### 3. Model State Validation in Controllers

```csharp
[HttpPost]
public async Task<IActionResult> CreateSkill([FromBody] CreateSkillDto dto)
{
    if (!ModelState.IsValid)
        return BadRequest(ModelState);
        
    var result = await _skillService.CreateSkillAsync(dto);
    return CreatedAtAction(nameof(GetSkillById), new { id = result.SkillId }, result);
}
```

---

## 📊 Logging

### 1. Configuration (Program.cs)

```csharp
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

// Optional: Serilog
builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));
```

### 2. Usage in Services

```csharp
public class TalentService : ITalentService
{
    private readonly ILogger<TalentService> _logger;
    
    public async Task<TalentProfileDto> GetTalentByIdAsync(Guid id)
    {
        _logger.LogInformation("Retrieving talent with ID: {TalentId}", id);
        
        var talent = await _talentRepository.GetByIdAsync(id);
        
        if (talent == null)
        {
            _logger.LogWarning("Talent with ID {TalentId} not found", id);
            throw new NotFoundException($"Talent with ID {id} not found");
        }
        
        _logger.LogDebug("Successfully retrieved talent: {TalentName}", talent.FullName);
        return _mapper.Map<TalentProfileDto>(talent);
    }
}
```

### 3. Log Levels

| Level | Usage |
|-------|-------|
| Trace | Very detailed logs (rarely used) |
| Debug | Debugging information |
| Information | General flow information |
| Warning | Unexpected but handled situations |
| Error | Errors and exceptions |
| Critical | Critical failures |

---

## 🧪 Testing Guidelines

### 1. Unit Testing (Services)

```csharp
public class TalentServiceTests
{
    private readonly Mock<ITalentProfileRepository> _mockRepository;
    private readonly Mock<IMapper> _mockMapper;
    private readonly Mock<ILogger<TalentService>> _mockLogger;
    private readonly TalentService _service;
    
    public TalentServiceTests()
    {
        _mockRepository = new Mock<ITalentProfileRepository>();
        _mockMapper = new Mock<IMapper>();
        _mockLogger = new Mock<ILogger<TalentService>>();
        _service = new TalentService(
            _mockRepository.Object,
            _mockMapper.Object,
            _mockLogger.Object);
    }
    
    [Fact]
    public async Task GetTalentByIdAsync_WhenTalentExists_ReturnsTalentDto()
    {
        // Arrange
        var talentId = Guid.NewGuid();
        var talent = new TalentProfile { TalentId = talentId, FullName = "John Doe" };
        var dto = new TalentProfileDto { TalentId = talentId, FullName = "John Doe" };
        
        _mockRepository.Setup(r => r.GetByIdAsync(talentId))
            .ReturnsAsync(talent);
        _mockMapper.Setup(m => m.Map<TalentProfileDto>(talent))
            .Returns(dto);
            
        // Act
        var result = await _service.GetTalentByIdAsync(talentId);
        
        // Assert
        Assert.NotNull(result);
        Assert.Equal(talentId, result.TalentId);
        Assert.Equal("John Doe", result.FullName);
    }
    
    [Fact]
    public async Task GetTalentByIdAsync_WhenTalentNotFound_ThrowsNotFoundException()
    {
        // Arrange
        var talentId = Guid.NewGuid();
        _mockRepository.Setup(r => r.GetByIdAsync(talentId))
            .ReturnsAsync((TalentProfile)null);
            
        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() => 
            _service.GetTalentByIdAsync(talentId));
    }
}
```

### 2. Integration Testing (Controllers)

```csharp
public class TalentsControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    
    public TalentsControllerIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }
    
    [Fact]
    public async Task GetAllTalents_ReturnsSuccessAndCorrectContentType()
    {
        // Act
        var response = await _client.GetAsync("/api/talents");
        
        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal("application/json; charset=utf-8", 
            response.Content.Headers.ContentType.ToString());
    }
}
```

---

## 🔧 Configuration Management

### 1. appsettings.json

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=skillsync;Username=postgres;Password=***"
  },
  "Jwt": {
    "Key": "your-secret-key-here-minimum-32-characters",
    "Issuer": "SkillSyncAPI",
    "Audience": "SkillSyncClient",
    "ExpiryMinutes": 60
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

### 2. Environment-Specific Settings

**appsettings.Development.json:**
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Debug",
      "Microsoft.AspNetCore": "Information"
    }
  }
}
```

**appsettings.Production.json:**
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Warning",
      "Microsoft.AspNetCore": "Error"
    }
  }
}
```

---

## 📦 Package Management

### Essential Packages

```xml
<ItemGroup>
  <!-- Core Framework -->
  <PackageReference Include="Microsoft.AspNetCore.OpenApi" Version="8.0.0" />
  
  <!-- Database -->
  <PackageReference Include="Microsoft.EntityFrameworkCore" Version="8.0.0" />
  <PackageReference Include="Npgsql.EntityFrameworkCore.PostgreSQL" Version="8.0.0" />
  <PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="8.0.0" />
  
  <!-- Authentication -->
  <PackageReference Include="Microsoft.AspNetCore.Authentication.JwtBearer" Version="8.0.0" />
  
  <!-- Mapping -->
  <PackageReference Include="AutoMapper.Extensions.Microsoft.DependencyInjection" Version="12.0.0" />
  
  <!-- Validation (Optional) -->
  <PackageReference Include="FluentValidation.AspNetCore" Version="11.3.0" />
  
  <!-- Logging (Optional) -->
  <PackageReference Include="Serilog.AspNetCore" Version="8.0.0" />
  
  <!-- API Documentation -->
  <PackageReference Include="Swashbuckle.AspNetCore" Version="6.5.0" />
</ItemGroup>
```

---

## 📝 Code Comments

### 1. XML Documentation

```csharp
/// <summary>
/// Retrieves a talent profile by ID
/// </summary>
/// <param name="id">The talent's unique identifier</param>
/// <returns>The talent profile if found</returns>
/// <exception cref="NotFoundException">Thrown when talent is not found</exception>
public async Task<TalentProfileDto> GetTalentByIdAsync(Guid id)
{
    // Implementation...
}
```

### 2. Comment Guidelines

**✅ Good Comments:**
```csharp
// Calculate match score based on skill overlap and level bonuses
var matchScore = CalculateMatchScore(talent, project);

// Auto-update availability when talent is assigned to project
talent.AvailabilityStatus = "ON_PROJECT";
```

**❌ Bad Comments:**
```csharp
// Get talent
var talent = GetTalent(id);

// Set status
talent.AvailabilityStatus = "ON_PROJECT";
```

---

## 🚀 Performance Best Practices

### 1. Async/Await

```csharp
// ✅ Good - Use async/await
public async Task<List<TalentProfileDto>> GetAllTalentsAsync()
{
    var talents = await _repository.GetAllAsync();
    return _mapper.Map<List<TalentProfileDto>>(talents);
}

// ❌ Bad - Blocking calls
public List<TalentProfileDto> GetAllTalents()
{
    var talents = _repository.GetAllAsync().Result;
    return _mapper.Map<List<TalentProfileDto>>(talents);
}
```

### 2. EF Core Optimization

```csharp
// ✅ Good - AsNoTracking for read-only
public async Task<IEnumerable<TalentProfile>> GetAvailableTalentsAsync()
{
    return await _context.TalentProfiles
        .AsNoTracking()
        .Where(t => t.AvailabilityStatus == "AVAILABLE")
        .ToListAsync();
}

// ✅ Good - Eager loading with Include
public async Task<TalentProfile> GetTalentWithSkillsAsync(Guid id)
{
    return await _context.TalentProfiles
        .Include(t => t.TalentSkills)
            .ThenInclude(ts => ts.Skill)
        .FirstOrDefaultAsync(t => t.TalentId == id);
}

// ❌ Bad - N+1 query problem
public async Task<List<TalentProfileDto>> GetAllTalentsAsync()
{
    var talents = await _context.TalentProfiles.ToListAsync();
    foreach (var talent in talents)
    {
        // This causes N additional queries!
        talent.TalentSkills = await _context.TalentSkills
            .Where(ts => ts.TalentId == talent.TalentId)
            .ToListAsync();
    }
    return _mapper.Map<List<TalentProfileDto>>(talents);
}
```

### 3. Pagination

```csharp
public async Task<PagedResult<TalentProfileDto>> GetTalentsPagedAsync(
    int page = 1, 
    int pageSize = 10)
{
    var query = _context.TalentProfiles.AsNoTracking();
    
    var totalRecords = await query.CountAsync();
    
    var talents = await query
        .Skip((page - 1) * pageSize)
        .Take(pageSize)
        .ToListAsync();
        
    return new PagedResult<TalentProfileDto>
    {
        Data = _mapper.Map<List<TalentProfileDto>>(talents),
        TotalRecords = totalRecords,
        CurrentPage = page,
        PageSize = pageSize
    };
}
```

---

**Next:** [Future Enhancements](./10-FutureEnhancements.md)
