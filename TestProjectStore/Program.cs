using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using TestProjectLibrary.Db.Context;
using TestProjectLibrary.RabbitMq.Events;
using TestProjectLibrary.RabbitMq.Extensions;
using TestProjectLibrary.RabbitMq.Handlers.Interfaces;
using TestProjectStore.Repository;
using TestProjectStore.Repository.Interfaces;
using TestProjectStore.Service;
using TestProjectStore.Service.Interfaces;
using RouteHandler = TestProjectStore.Handlers.RouteHandler;

var builder = WebApplication.CreateBuilder(args);

var dbConnectionString = builder.Configuration.GetValue<string>("DB:Postgres")!;
var rbMqConnection = builder.Configuration.GetValue<string>("EventBusConnection")!;
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRabbitMq(builder.Configuration);
builder.Services.AddDbContext<PgContext>(
    t =>
    {
        t.UseNpgsql(builder.Configuration.GetValue<string>("DB:Postgres"),
            o =>
                o.MigrationsAssembly("TestProjectStore"));
    });

builder.Services.AddHealthChecks()
    .AddNpgSql(dbConnectionString)
    .AddRabbitMQ(rabbitConnectionString: rbMqConnection);

#region EventHandler

builder.Services.AddTransient<RouteHandler>();

#endregion

#region Services

builder.Services.AddTransient<IRouteService, RouteService>();

#endregion

#region Repository

builder.Services.AddTransient<IRouteRepository, RouteRepository>();

#endregion

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<PgContext>();
    db.Database.Migrate();
}

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapHealthChecks("/ping", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.useRabbitMqSubscription().Subscribe<RouteEvent, RouteHandler>();

app.UseAuthorization();

app.MapControllers();

app.Run();