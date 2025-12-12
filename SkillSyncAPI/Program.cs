using System.Reflection;
using System.Text;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using SkillSyncAPI.Data;
using SkillSyncAPI.Repositories;
using SkillSyncAPI.Repositories.Data;
using SkillSyncAPI.Repositories.Interfaces;
using SkillSyncAPI.Services;
using SkillSyncAPI.Services.Interfaces;
using SkillSyncAPI.Utilities;
using SkillSyncAPI.Validator.Projects;
using TokenHandler = SkillSyncAPI.Utilities.TokenHandler;

var builder = WebApplication.CreateBuilder(args);

// Konfigurasi DbContext di sini
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<SkillSyncDbContext>(options =>
    options.UseSqlServer(connectionString)
);

// Add repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITalentProfileRepository, TalentProfileRepository>();
builder.Services.AddScoped<ISkillRepository, SkillRepository>();
builder.Services.AddScoped<ITalentSkillRepository, TalentSkillRepository>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IProjectAssignmentRepository, ProjectAssignmentRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Add services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITalentProfileService, TalentProfileService>();
builder.Services.AddScoped<ISkillService, SkillService>();
builder.Services.AddScoped<ITalentSkillService, TalentSkillService>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IProjectAssignmentService, ProjectAssignmentService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IHashHandler, HashHandler>();

// Add services to the container.
builder.Services.AddControllers();

// Add global exception handler
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

// Add FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<CreateProjectValidator>();

builder
    .Services.AddFluentValidationAutoValidation()
    .AddValidatorsFromAssembly(typeof(Program).Assembly);

// Add EmailHandler
var smtpServer = builder.Configuration["EmailSettings:SmtpServer"];
var smtpPort = int.Parse(builder.Configuration["EmailSettings:SmtpPort"] ?? "25");
var smtpUsername = builder.Configuration["EmailSettings:smtpUsername"];
var smtpPassword = builder.Configuration["EmailSettings:smtpPassword"];
var mailFrom = builder.Configuration["EmailSettings:MailFrom"];

builder.Services.AddTransient<IEmailHandler, EmailHandler>(_ => new EmailHandler(
    smtpServer ?? "localhost",
    Convert.ToInt32(smtpPort),
    smtpUsername ?? "unknown",
    smtpPassword ?? "unknown",
    mailFrom ?? "unknown@mail.com"
));

// Authentication Configuration
builder
    .Services.AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(x =>
    {
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)
            ),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ClockSkew = TimeSpan.Zero,
        };
    });

var jwtKey = builder.Configuration["Jwt:Key"] ?? "InvalidKey";
var jwtIssuer = builder.Configuration["Jwt:Issuer"] ?? "InvalidIssuer";
var jwtAudience = builder.Configuration["Jwt:Audience"] ?? "InvalidAudience";
var durationInMinutes = Convert.ToInt16(builder.Configuration["Jwt:DurationInMinutes"] ?? "5");
builder.Services.AddScoped<ITokenHandler, TokenHandler>(_ => new TokenHandler(
    jwtKey,
    jwtIssuer,
    jwtAudience,
    durationInMinutes
));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition(
        "Bearer",
        new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Description =
                "Enter the Bearer Authorization string as following: `Bearer Generated-JWT-Token`",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
            Scheme = "Bearer",
            BearerFormat = "JWT",
        }
    );
    options.AddSecurityRequirement(
        new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Name = "Bearer",
                    In = ParameterLocation.Header,
                    Reference = new OpenApiReference
                    {
                        Id = "Bearer",
                        Type = ReferenceType.SecurityScheme,
                    },
                },
                new List<string>()
            },
        }
    );
});

// Add CORS
builder.Services.AddCors(cfg =>
    cfg.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin();
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
    })
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
