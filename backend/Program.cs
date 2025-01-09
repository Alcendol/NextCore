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

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi(); 
}

app.UseCors("AllowFrontend"); 
app.UseRouting();
app.MapControllers();  

app.Run();
