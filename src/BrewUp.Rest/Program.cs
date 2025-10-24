using BrewUp.Rest.Modules;

var builder = WebApplication.CreateBuilder(args);
builder.RegisterModules();
builder.Configuration.AddEnvironmentVariables();

var app = builder.Build();
app.ConfigureModules();

app.Logger.LogInformation("Starting BrewUp API...");

app.Run();
