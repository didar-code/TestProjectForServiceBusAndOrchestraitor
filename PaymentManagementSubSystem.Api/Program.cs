using Microsoft.EntityFrameworkCore;
using PaymentManagementSubSystem.DTOs.Commands;
using PaymentManagementSubSystem.Handler.Commands;
using PaymentManagementSubSystem.Repository.Data;
using PaymentManagementSubSystem.Repository.Interfaces;
using PaymentManagementSubSystem.Repository.Repositories;
using SharedSubSystem.Generics;
using PaymentManagementSubSystem.Handler.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();



builder.Services.AddPaymentManagement(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();