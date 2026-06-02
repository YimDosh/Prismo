using Prismo.Api.Endpoints;
using Scalar.AspNetCore;
using Prismo.Core.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using Prismo.Core.Infrastructure.Common.Behaviors;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<PrismoDbContext>(options =>
    options.UseNpgsql(connectionString));

// Añade esto en la sección donde registras tus servicios (builder.Services)
builder.Services.AddMediatR(cfg => 
{
    cfg.RegisterServicesFromAssembly(typeof(Prismo.Core.Features.Companies.UpdateCompany.UpdateCompanyHandler).Assembly);
    
    // 🔥 ESTA LÍNEA AGREGA EL VALIDADOR AL PIPELINE DE MEDIATR
    cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
});

// 🔥 ESTA LÍNEA REGISTRA TODOS LOS VALIDATORS (Como CreateCompanyValidator) DEL CORE
builder.Services.AddValidatorsFromAssembly(typeof(Prismo.Core.Features.Companies.CreateCompany.CreateCompanyValidator).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.

app.MapOpenApi();
app.MapScalarApiReference();

app.UseHttpsRedirection();
app.MapCreateCompany();
app.Run();


