using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using PersonBookWebApplication.Consts;
using System.Net.Http.Headers;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using Utilities;
using static System.Net.WebRequestMethods;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddHttpContextAccessor();

builder.Services.AddMvc(x =>
{
    x.EnableEndpointRouting = true;
})
.AddRazorOptions(option =>
{
    option.AreaViewLocationFormats.Clear();
    option.AreaViewLocationFormats.Add("/Areas/{2}/Views/{1}/{0}.cshtml");
    option.AreaViewLocationFormats.Add("/Areas/{2}/Views/Shared/{0}.cshtml");
    option.AreaViewLocationFormats.Add("/Views/Shared/{0}.cshtml");
});

builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.IdleTimeout = TimeSpan.FromDays(1);
});

var tokenOpt = builder.Configuration.GetSection("JwtTokenInformation").Get<CommonTokenOption>();

var s = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOpt.SecurityKey));

var c = tokenOpt;

builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, option =>
{
    option.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidIssuer = tokenOpt.Issuer,
        ValidAudience = tokenOpt.Audiances,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOpt.SecurityKey)),
        ClockSkew = TimeSpan.Zero,
        
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true
    };
});

builder.Services.AddAuthorization(auth =>
{
    auth.AddPolicy("admin", role =>
    {
        role.RequireUserName("Mert");
    });
});

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddHttpClient(ClientConsts.AuthenticationServerName, x =>
{
    x.BaseAddress = new Uri(ClientConsts.AuthenticationServerBaseAddress);
    x.DefaultRequestHeaders.Accept.Clear();
    x.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
});

var app = builder.Build();


app.UseSession();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();

app.Use(async (context, next) =>
{
    
    var authHeader = context.Request.Headers["Authorization"];
    if (authHeader.Count == 0)
    {
        context.Session.TryGetValue("session", out var token);
        if (token != null)
        {
            string tokenString = Encoding.UTF8.GetString(token);
            context.Request.Headers["Authorization"] = "Bearer " + tokenString;

            var s = context.Request.Headers.Authorization;
        }
    }
    await next();
});

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Account}/{action=Index}/{id?}"
    );

    endpoints.MapAreaControllerRoute(
        name: "Main",
        areaName: "Main",
        pattern: "Per/{controller=Person}/{action=Index}"
    );

});

app.Run();
