using Logging.Application.DTOs;
using Logging.Domain.Entities;
using Logging.Domain.IRepositories;
using Logging.Infrastructure.Contexts;
using Logging.Infrastructure.Repositories;
using Logging.Infrastructure.Services;
using LoggingService.Application.IServices;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true;
    });

builder.Services.AddDbContext<LoggingDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddScoped<ILogRepository, LogRepository>();
builder.Services.AddScoped<ILogService, LogService>();

builder.Services.AddAutoMapper(cfg =>
{
    cfg.CreateMap<Log, LogDto>();
    cfg.CreateMap<CreateLogDto, Log>();
}, AppDomain.CurrentDomain.GetAssemblies());

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi(options =>
{
    // Define the Metadata (The "Service Contract")
    options.AddDocumentTransformer((document, context, cancellationToken) =>
    {
        document.Info.Title = "SIS Logging Microservice";
        document.Info.Version = "v1.0.0";
        document.Info.Description = "Centralized logging service for the Student Information System (SIS). ";

        // In .NET 10, you can also define contact/license info here
        document.Info.Contact = new Microsoft.OpenApi.OpenApiContact
        {
            Name = "HIAST SUPOORT TEAM",
            Email = "ekarhilli@gmail.com"
        };

        return Task.CompletedTask;
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapScalarApiReference(options =>
{
    options.WithTitle("SIS Logging API Reference")
           .WithTheme(ScalarTheme.Moon);
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
