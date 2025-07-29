using dotrestapiwithmongo.Models;
using dotrestapiwithmongo.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// ----------------------------------------------
// 📦 MongoDB Configuration from appsettings.json
// ----------------------------------------------
builder.Services.Configure<MongoDBSettings>(
    builder.Configuration.GetSection("MongoDBSettings"));

// 🧠 Register custom service to handle MongoDB logic
builder.Services.AddSingleton<StudentService>();

// ----------------------------------------------
// 🔧 Add services required for Web API
// ----------------------------------------------
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// ----------------------------------------------
// 📘 Swagger setup (OpenAPI)
// - Also add server URL to force Swagger to use HTTPS
// ----------------------------------------------
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Student API",
        Version = "v1"
    });

    // Make sure Swagger uses HTTPS
    options.AddServer(new OpenApiServer
    {
        Url = "https://localhost:7103" // ❗ Replace with your actual HTTPS port
    });
});

// ----------------------------------------------
// 🌐 CORS Policy - to allow Swagger UI & browser requests
// ----------------------------------------------
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// ----------------------------------------------
// 🌍 Enable Swagger UI only in Development
// ----------------------------------------------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ----------------------------------------------
// 🛣️ Middleware Pipeline
// ----------------------------------------------
app.UseHttpsRedirection();    // Redirect HTTP → HTTPS
app.UseCors("AllowAll");      // Enable the CORS policy
app.UseAuthorization();       // Authorization middleware
app.MapControllers();         // Map API controller routes

// 🚀 Run the app
app.Run();
