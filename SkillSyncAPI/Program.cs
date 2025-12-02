using System.Reflection;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using SkillSyncAPI.Data;
using SkillSyncAPI.Repositories;
using SkillSyncAPI.Repositories.Data;
using SkillSyncAPI.Repositories.Interfaces;
using SkillSyncAPI.Services;
using SkillSyncAPI.Services.Interfaces;
using SkillSyncAPI.Utilities;
using SkillSyncAPI.Validator.Projects;

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

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
