using NextCore.backend.Repositories;
using NextCore.backend.Helpers;
using NextCore.backend.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using Microsoft.Extensions.FileProviders;
using MySqlConnector;
using Newtonsoft.Json;
using NextCore.Migrations;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
        policy.WithOrigins("http://localhost:3001") 
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials());
});


var conString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseMySql(conString!, new MySqlServerVersion(new Version(8,1,0)))
);
// builder.Services.AddControllers(options => {
//     options.InputFormatters.Insert(0, MyJPIF.GetJsonPatchInputFormatter());
// });  
builder.Services.AddControllers();  
builder.Services.AddOpenApi();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<JwtService>();
builder.Services.AddScoped<IBookRepository, BookRepository>();

// Json Format
// builder.Services.ConfigureHttpJsonOptions(options =>{
//     options.SerializerOptions.Converters.Add(new JsonConverter<DateOnly>{
//         public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
//         {
//             var value = reader.GetString();
//             return DateOnly.Parse(value!);
//         }

//         public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
//         {
//             writer.WriteStringValue(value.ToString("d-M-yyyy")); // Your desired format here
//         }
//     });
// });

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
