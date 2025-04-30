using Infrastructure;
using Infrastructure.Middleware;
using Microsoft.EntityFrameworkCore;
using Presentation;

var builder = WebApplication.CreateBuilder(args);

var startup = new Startup(builder.Configuration);

startup.ConfigureServices(builder.Services);

var app = builder.Build();

app.UseMiddleware<RequestLoggingMiddleware>();


using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<MonkeyShelterDbContext>();
    dbContext.Database.Migrate();
    var seeder = scope.ServiceProvider.GetRequiredService<InitialDbDataSeed>();

    seeder.Seed();


}

// Configure the HTTP request pipeline
startup.Configure(app, app.Environment);

app.Run();
