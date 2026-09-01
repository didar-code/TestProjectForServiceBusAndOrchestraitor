using Microsoft.EntityFrameworkCore;
using OrderManagementSubsystem.DTOs.Commands;
using OrderManagementSubsystem.DTOs.Responses;
using OrderManagementSubsystem.Handler.Commands;
using OrderManagementSubsystem.Repository.Data;
using OrderManagementSubsystem.Repository.Interfaces;
using OrderManagementSubsystem.Repository.Repositories;
using SharedSubSystem.Generics;
using OrderManagementSubsystem.Handler.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();





builder.Services.AddOrderManagement(builder.Configuration);

var app = builder.Build();




if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();