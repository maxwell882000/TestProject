using RestEase.HttpClientFactory;
using TestProject.V1.ProviderOne.Services;
using TestProject.V1.ProviderOne.Services.Interfaces;
using TestProject.V1.ProviderTwo.Services;
using TestProject.V1.ProviderTwo.Services.Interfaces;
using TestProject.V1.Routes.Interfaces;
using TestProject.V1.Routes.Services;
using TestProjectLibrary.RabbitMq.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddRabbitMq(builder.Configuration);

#region Services

builder.Services.AddTransient<IProviderOneService, ProviderOneService>();
builder.Services.AddTransient<IProviderTwoService, ProviderTwoService>();
builder.Services.AddTransient<IRouteService, RouteService>();

#endregion

#region RestEase

builder.Services.AddRestEaseClient<IProviderOneRestEase>(
    builder.Configuration.GetValue<string>("RestEase:ProviderOne"));
builder.Services.AddRestEaseClient<IProviderTwoRestEase>(
    builder.Configuration.GetValue<string>("RestEase:ProviderTwo"));
builder.Services.AddRestEaseClient<ISearchService>(
    builder.Configuration.GetValue<string>("RestEase:Search"));

#endregion


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();