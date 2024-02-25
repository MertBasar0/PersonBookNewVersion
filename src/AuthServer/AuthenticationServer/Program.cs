using AuthServer.Core;
using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete.Infrastructure;
using Entities;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Utilities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddCors(opt => opt.AddPolicy("AuthServerCorsPolicy", pol =>
{
    pol.AllowAnyHeader();
    pol.AllowAnyMethod();
    pol.AllowAnyOrigin();
}));

builder.Services.Configure<CommonTokenOption>(builder.Configuration.GetSection("JwtTokenInformation"));

builder.Services.AddSqlServer<AuthDbContext>(builder.Configuration.GetConnectionString("sqlContainer"));

builder.Services
    .AddIdentity<AppUser, AppRole>(opt =>
    {
        opt.User.RequireUniqueEmail = true;
        opt.Password.RequiredLength = 8;
        opt.Password.RequireDigit = true;
        opt.Password.RequireLowercase = true;
        opt.Password.RequireUppercase = true;
        opt.Password.RequireNonAlphanumeric = false;
        opt.ClaimsIdentity.UserNameClaimType = ClaimTypes.Name;
        opt.ClaimsIdentity.RoleClaimType = ClaimTypes.Role;
        opt.ClaimsIdentity.EmailClaimType = ClaimTypes.Email;
        opt.Lockout.MaxFailedAccessAttempts = 5;
        //opt.SignIn.RequireConfirmedEmail = true;
    })
    .AddEntityFrameworkStores<AuthDbContext>()
    .AddDefaultTokenProviders();


builder.Services.AddTransient<IAccountManager, AccountService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseCors("AuthServerCorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
