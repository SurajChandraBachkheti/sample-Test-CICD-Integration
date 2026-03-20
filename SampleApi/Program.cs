using Microsoft.OpenApi;
using SampleApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Sample API",
        Version = "v1",
        Description = "A simple .NET 10 Web API with static data examples.",
        Contact = new OpenApiContact
        {
            Name = "Sample API Support",
            Email = "support@sampleapi.com"
        }
    });
});

var app = builder.Build();

// Configure middleware pipeline data data

app.UseStaticFiles(); // Enable static file serving for Swagger UI
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Sample API v1");
    options.RoutePrefix = string.Empty; // Swagger at root
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
