using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
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

builder.Services.AddCors(x => x.AddPolicy("example", x =>
{
    x.AllowAnyMethod();
    x.AllowAnyOrigin();
    x.AllowAnyHeader();
}));

builder.Services.AddAuthorization(auth =>
{
    auth.AddPolicy("admin", role =>
    {
        role.RequireClaim(ClaimTypes.Role, "admin");
    });
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

})
.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
{
    var tokenOptions = builder.Configuration.GetSection("JwtTokenInformation").Get<CommonTokenOption>();
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = tokenOptions.Issuer,
        ValidAudience = tokenOptions.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.SecurityKey)), //Token için bir key identifier bulunmama hatasý geliyor. Buradan devam edilecek.
        ClockSkew = TimeSpan.Zero,


        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true
    };
    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            throw new Exception(context.Exception.Message);
        }
        //OnTokenValidated = context =>
        //{
        //    context.HttpContext.User
        //}

    };
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

app.UseCors("example");

app.UseStaticFiles();

app.Use(async (context, next) =>
{
    var authHeader = context.Request.Headers["Authorization"];
    if(authHeader.Count == 0)
    {
        context.Session.TryGetValue("session", out var token);
        if (token != null)
            context.Request.Headers["Authorization"] = JwtBearerDefaults.AuthenticationScheme + " " + Encoding.UTF8.GetString(token);
    }
    await next();
});

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoint =>
{

    endpoint.MapAreaControllerRoute(
    name: "Main",
    areaName: "Main",
    pattern: "MainArea/{controller=Person}/{action=Index}/{id?}"
    );


    endpoint.MapControllerRoute(
        name: "default",
        pattern: "{controller=Account}/{action=Index}/{id?}"
    );
});

app.Run();
