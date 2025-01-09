using MySqlConnector;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
        policy.WithOrigins("http://localhost:3001") 
              .AllowAnyMethod()
              .AllowAnyHeader());
});

builder.Services.AddControllers();  

builder.Services.AddOpenApi();
var conString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddMySqlDataSource(conString!);

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

app.Run();
