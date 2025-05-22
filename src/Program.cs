using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entity;
using ExtensionMethods;
using Mapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();

// API Controllers
builder.Services.AddControllers();

// API Services Helpers and Repos
builder.Services.AddServices();

// Configura el acceso a la base de datos
builder.Services.AddDbContext<ApiDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Mapper
builder.Services.AddAutoMapper(typeof(MapperProfiles));

// Authorization
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
      options.Cookie.SameSite = SameSiteMode.Lax;
      options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
      options.SlidingExpiration = true;
      options.Cookie.HttpOnly = true;
      options.Cookie.IsEssential = true;
      options.Events = new CookieAuthenticationEvents
      {
        OnRedirectToLogin = context =>
        {
          context.Response.StatusCode = StatusCodes.Status401Unauthorized;
          return Task.CompletedTask;
        },
        OnRedirectToAccessDenied = context =>
        {
          context.Response.StatusCode = StatusCodes.Status403Forbidden;
          return Task.CompletedTask;
        }
      };
    });



// CORS
builder.Services.AddCors(options =>
{
  options.AddPolicy("AllowAll",
      policy => policy.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader());
});


// Swagger configuration
builder.Services.AddSwaggerGen(c =>
{
  c.SwaggerDoc("v1", new OpenApiInfo
  {
    Title = "Mi API",
    Version = "v1",
    Description = "Documentación de la API usando Swagger",
    Contact = new OpenApiContact
    {
      Name = "Tu Nombre",
      Email = "tuemail@example.com",
      Url = new Uri("https://tuweb.com")
    }
  });

  builder.Services.Configure<HostOptions>(options =>
  {
      options.BackgroundServiceExceptionBehavior = BackgroundServiceExceptionBehavior.Ignore;
  });

  // Add Swagger authentication
  c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
  {
    Name = "Authorization",
    Type = SecuritySchemeType.Http,
    Scheme = "Bearer",
    BearerFormat = "JWT",
    In = ParameterLocation.Header,
    Description = "Introduce el token JWT con el prefijo 'Bearer'. Ejemplo: Bearer <token>"
  });

  c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new List<string>()
        }
    });
});

var app = builder.Build();
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
app.Urls.Clear();
app.Urls.Add($"http://*:{port}");
Console.WriteLine($"Server running on port {port}");

app.UseSwagger();
app.UseSwaggerUI(c =>
{
  c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mi API v1");
  c.RoutePrefix = string.Empty; // Permite acceder desde la raíz
});
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
