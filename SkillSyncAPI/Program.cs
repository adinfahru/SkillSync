using Microsoft.EntityFrameworkCore;
using SkillSyncAPI.Data;
using SkillSyncAPI.Repositories.Data;
using SkillSyncAPI.Repositories.Interfaces;
using SkillSyncAPI.Services;
using SkillSyncAPI.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Tambahkan konfigurasi DbContext di sini
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<SkillSyncDbContext>(options =>
    options.UseSqlServer(connectionString)
);

// Add repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<ITalentProfileRepository, TalentProfileRepository>();

// Add services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITalentProfileService, TalentProfileService>();

// Add services to the container.
builder.Services.AddControllers();

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
