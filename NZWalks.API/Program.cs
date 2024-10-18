using Microsoft.EntityFrameworkCore; // Enables Entity Framework Core functionality
using NZWalks.API.Data; // Imports the NZWalksDbContext for database access

var builder = WebApplication.CreateBuilder(args); // Creates a WebApplication builder to configure the app and services

// Add services to the container.

builder.Services.AddControllers(); // Adds controller support to the app (for handling API requests)

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer(); // Adds support for minimal APIs exploration (documentation, API discovery)
builder.Services.AddSwaggerGen(); // Adds Swagger generation for API documentation (helpful for testing and documentation)

// Inject DbContext (Dependency Injection for database access)
builder.Services.AddDbContext<NZWalksDbContext>(options =>
{
    // Configures the DbContext to use SQL Server and the connection string from app settings
    options.UseSqlServer(builder.Configuration.GetConnectionString("NZWalksDbConnectionString"));
});

var app = builder.Build(); // Builds the app pipeline with the configured services

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) // Checks if the app is running in a development environment
{
    app.UseSwagger(); // Enables Swagger middleware for generating API docs
    app.UseSwaggerUI(); // Enables a user-friendly Swagger UI to explore the API endpoints
}

app.UseHttpsRedirection(); // Forces the use of HTTPS for security

app.UseAuthorization(); // Adds authorization middleware to ensure access control for API requests

app.MapControllers(); // Maps incoming requests to the corresponding controllers (routes HTTP requests to API endpoints)

app.Run(); // Runs the application
