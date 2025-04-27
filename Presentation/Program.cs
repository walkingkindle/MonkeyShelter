using Infrastructure;
using Infrastructure.Middleware;
using Presentation;

var builder = WebApplication.CreateBuilder(args);

var startup = new Startup(builder.Configuration);

startup.ConfigureServices(builder.Services);

var app = builder.Build();

app.UseMiddleware<ResultMiddleware>();


using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<InitialDbDataSeed>();

    seeder.Seed();
}

// Configure the HTTP request pipeline
startup.Configure(app, app.Environment);

app.Run();
