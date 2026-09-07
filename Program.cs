using Lab9.Api.Students.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();

builder.Services.Configure<StudentApiOptions>(
    builder.Configuration.GetRequiredSection("StudentApiSettings"));

var app = builder.Build();

app.UseSwagger().UseSwaggerUI();

app.MapControllers();

app.Run();
