using Microsoft.EntityFrameworkCore;
using FluentValidation;
using FluentValidation.AspNetCore;
using MiniOrderManagement.Application.Commands.Customers.CreateCustomer;
using MiniOrderManagement.Application.Commands.Orders.CreateOrder;
using MiniOrderManagement.Application.Queries.Customers;
using MiniOrderManagement.Application.Queries.Orders;
using MiniOrderManagement.Application.Interfaces;
using MiniOrderManagement.Infrastructure.Data;
using MiniOrderManagement.Infrastructure.Repository;

var builder = WebApplication.CreateBuilder(args);

#region Database

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

#endregion

#region Dependency Injection

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Command Handlers
builder.Services.AddScoped<CreateCustomerHandler>();
builder.Services.AddScoped<CreateOrderHandler>();

// Query Handlers
builder.Services.AddScoped<GetCustomerByIdHandler>();
builder.Services.AddScoped<GetOrdersByCustomerIdHandler>();

// Validation
builder.Services.AddValidatorsFromAssemblyContaining<CreateCustomerValidator>();
builder.Services.AddFluentValidationAutoValidation();

#endregion

#region Controllers (API)

builder.Services.AddControllers();

#endregion

#region Swagger

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#endregion

var app = builder.Build();

#region Middleware

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

#endregion

app.Run();
