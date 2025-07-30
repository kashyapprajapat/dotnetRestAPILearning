using dotrestapiwithmongo.Models;
using dotrestapiwithmongo.Services;
using DotNetEnv;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// ----------------------------------------------
// 🌱 Load .env file and bind MongoDBSettings
// ----------------------------------------------
DotNetEnv.Env.Load(); // Load .env file from project root

var mongoSettings = new MongoDBSettings
{
    ConnectionString = Environment.GetEnvironmentVariable("MONGO_CONN"),
    DatabaseName = Environment.GetEnvironmentVariable("MONGO_DB_NAME"),
    StudentsCollection = Environment.GetEnvironmentVariable("MONGO_COLLECTION")
};

// ----------------------------------------------
// 💉 Register dependencies (DI)
// ----------------------------------------------
builder.Services.AddSingleton(mongoSettings);       // Inject MongoDB settings
builder.Services.AddSingleton<StudentService>();    // Inject StudentService

// ----------------------------------------------
// 🔧 Web API setup
// ----------------------------------------------
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// ----------------------------------------------
// 📘 Swagger setup (OpenAPI)
// ----------------------------------------------
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Student API",
        Version = "v1"
    });

    // Force Swagger to use HTTPS
    options.AddServer(new OpenApiServer
    {
        Url = "https://localhost:7103" // Replace with your actual HTTPS port if different
    });
});

// ----------------------------------------------
// 🌐 CORS Policy
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
// 🌍 Enable Swagger always (Dev or Prod)
// ----------------------------------------------
app.UseSwagger();
app.UseSwaggerUI();

// ----------------------------------------------
// 🛣️ Middleware Pipeline
// ----------------------------------------------
app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();

// 🚀 Run the app
app.Run();
