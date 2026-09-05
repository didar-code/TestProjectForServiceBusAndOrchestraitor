using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using OrderManagementSubsystem.Api.Middlerware;
using OrderManagementSubsystem.DTOs.Commands;
using OrderManagementSubsystem.DTOs.Responses;
using OrderManagementSubsystem.Handler.Commands;
using OrderManagementSubsystem.Handler.DependencyInjection;
using OrderManagementSubsystem.Repository.Data;
using OrderManagementSubsystem.Repository.Interfaces;
using OrderManagementSubsystem.Repository.Repositories;

using SharedSubSystem.Generics;
using SharedSubSystem.Security;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.CustomSchemaIds(type => type.FullName);

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter JWT token like: Bearer {your token}"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            Array.Empty<string>()
        }
    });
});

builder.Services.AddTokenVarification(builder.Configuration);


builder.Services.AddOrderManagement(builder.Configuration);

var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();



if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();