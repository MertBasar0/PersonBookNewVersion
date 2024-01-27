using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using PersonBookWebApplication.Consts;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using Utilities;
using static System.Net.WebRequestMethods;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSession();

builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(option =>
    {
        var tokenOpt = builder.Configuration.GetSection("JwtTokenInformation").Get<CommonTokenOption>();
        option.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidIssuer = tokenOpt.Issuer,
            ValidAudience = tokenOpt.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOpt.SecurityKey))
        };
    });

builder.Services.AddControllersWithViews();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddHttpClient(ClientConsts.AuthenticationServerName, x =>
{
    x.BaseAddress = new Uri(ClientConsts.AuthenticationServerBaseAddress);
    x.DefaultRequestHeaders.Accept.Clear();
    x.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseSession();

app.UseStaticFiles();

app.Use(async (context, next) =>
{
    var authHeader = context.Request.Headers.Authorization;
    if(authHeader == 0)
    {
        context.Session.TryGetValue("session", out var token);
        if(token != null)
            context.Request.Headers.Authorization = "Bearer" + Encoding.UTF8.GetString(token);
    }
    await next();
});

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
