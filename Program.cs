using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ExtensionMethods;
using Entity;
using Mapper;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Services;

var builder = WebApplication.CreateBuilder(args);

// API Controllers
builder.Services.AddControllers();

// API Services Helpers and Repos
builder.Services.AddServices();

// Configura el acceso a la base de datos, solo pasamos los options al DbContext
builder.Services.AddDbContext<ApiDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Mapper
builder.Services.AddAutoMapper(typeof(MapperProfiles));

var app = builder.Build();

// Configurar la canalización de solicitudes HTTP.
app.MapControllers();

app.Run();
