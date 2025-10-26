using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PlayerSponsor.Server.Auth;
using PlayerSponsor.Server.Data.Context;
using PlayerSponsor.Server.Models;
using PlayerSponsor.Server.Repositories;
using PlayerSponsor.Server.Services;
using PlayerSponsor.Server.Services.ClubService;

var builder = WebApplication.CreateBuilder(args);

// Add MySQL to the container
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("Default"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("Default"))));

// Add authentication & authorization
builder.Services.AddAuthentication();

// Configure Identity
builder.Services.AddApplicationIdentity();

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("ClubAdminPolicy", policy =>
        policy.Requirements.Add(new ClubAccessRequirement()));

// Add AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register Dependencies
builder.Services.AddScoped<IClubService, ClubService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IClubRepository, ClubRepository>();
builder.Services.AddScoped<IClubAdminRepository, ClubAdminRepository>();
builder.Services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, ClubAdminClaimsPrincipalFactory>();
builder.Services.AddScoped<IAuthorizationHandler, ClubAccessHandler>();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapIdentityApi<ApplicationUser>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
