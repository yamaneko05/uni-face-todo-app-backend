using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connection = String.Empty;
var clientOrigin = Environment.GetEnvironmentVariable("CLIENT_ORIGIN")!;
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddEnvironmentVariables().AddJsonFile("appsettings.Development.json");
    connection = builder.Configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");
}
else
{
    connection = Environment.GetEnvironmentVariable("AZURE_SQL_CONNECTIONSTRING");
}

builder.Services.AddDbContext<Db>(options => {
    options.UseSqlServer(connection);
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        name: MyAllowSpecificOrigins,
        policy => {
            policy
                .WithOrigins(clientOrigin)
                .AllowAnyHeader()
                .AllowAnyMethod();
        }
    );
});

var app = builder.Build();
var apiRouteGroup = app.MapGroup("/api");
var taskRouteGroup = apiRouteGroup.MapGroup("/tasks");

TaskEndpoints.Map(taskRouteGroup);

app.UseCors(MyAllowSpecificOrigins);
app.Run();
