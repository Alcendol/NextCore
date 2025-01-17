using auth.Data;
using auth.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using MySqlConnector;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
        policy.WithOrigins("http://nextcore.test") 
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials());
});


var conString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<UserContext>(options =>
    options.UseMySql(conString!, new MySqlServerVersion(new Version(8,1,0)))
);

builder.Services.AddControllers();  
builder.Services.AddOpenApi();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<JwtService>();

// Add logging services
builder.Logging.ClearProviders(); // Clear default providers
builder.Logging.AddConsole(); // Add console logging
builder.Logging.AddDebug();
builder.Logging.SetMinimumLevel(LogLevel.Debug); // Set the minimum log level


var app = builder.Build();
app.Logger.LogInformation("Adding Routes");


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi(); 
}
app.UseCors("AllowFrontend"); 
app.UseRouting();
app.MapControllers();  

app.UseStaticFiles();

app.Run();
