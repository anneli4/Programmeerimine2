using FluentValidation;
using KooliProjekt.Application.Behaviors;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace KooliProjekt.WebAPI;

public partial class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        if (builder.Environment.IsEnvironment("Testing"))
        {
            var databaseName = builder.Configuration["InMemoryDatabaseName"] ?? "KooliProjektIntegrationTests";
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase(databaseName));
        }
        else
        {
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
        }

        // Add controllers
        builder.Services.AddControllers();

        // Swagger (API docs)
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // FluentValidation and MediatR configuration
        var applicationAssembly = typeof(ErrorHandlingBehavior<,>).Assembly;
        builder.Services.AddValidatorsFromAssembly(applicationAssembly);
        builder.Services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(applicationAssembly);
            config.AddOpenBehavior(typeof(ErrorHandlingBehavior<,>));
            config.AddOpenBehavior(typeof(ValidationBehavior<,>));
            config.AddOpenBehavior(typeof(TransactionalBehavior<,>));
        });

        // Register repositories for both interface-based and concrete handler dependencies.
        builder.Services.AddScoped<CategoryRepository>();
        builder.Services.AddScoped<ClientRepository>();
        builder.Services.AddScoped<ItemRepository>();
        builder.Services.AddScoped<OrderRepository>();
        builder.Services.AddScoped<OrderItemRepository>();
        builder.Services.AddScoped<InvoiceRepository>();
        builder.Services.AddScoped<InvoiceLineRepository>();

        builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
        builder.Services.AddScoped<IClientRepository, ClientRepository>();
        builder.Services.AddScoped<IItemRepository, ItemRepository>();
        builder.Services.AddScoped<IOrderRepository, OrderRepository>();
        builder.Services.AddScoped<IOrderItemRepository, OrderItemRepository>();
        builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
        builder.Services.AddScoped<IInvoiceLineRepository, InvoiceLineRepository>();

        var app = builder.Build();

        // Optionally migrate/seed DB here. To use async seeding, change Main to:
        // public static async Task Main(string[] args)
        // and then run:
        // using var scope = app.Services.CreateScope();
        // var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        // await SeedData.GenerateAsync(db);

        // HTTP pipeline
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}