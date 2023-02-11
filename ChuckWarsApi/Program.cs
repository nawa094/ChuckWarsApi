using ChuckWarsApi.Configuration;
using ChuckWarsApi.Services.Chuck;
using ChuckWarsApi.Services.Swapi;
using ChuckWarsWebAssembly.Shared.Profiles;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "ChuckWars API",
        Description = "An ASP.NET Core Web API to abstract the Chuck Norris API and Star Wars API"
    });
});

// Services
var chuckApiConfig = new ChuckApiConfiguration();
var starWarsApiConfig = new SWApiConfiguration();

builder.Configuration.GetSection(ChuckApiConfiguration.SectionName).Bind(chuckApiConfig);
builder.Configuration.GetSection(SWApiConfiguration.SectionName).Bind(starWarsApiConfig);

builder.Services.AddHttpClient<IChuckService, ChuckService>(client =>
{
    client.BaseAddress = new Uri(chuckApiConfig.BaseUrl);
});

builder.Services.AddHttpClient<ISwapiService, SwapiService>(client =>
{
    client.BaseAddress = new Uri(starWarsApiConfig.BaseUrl);
});

// Automapper
builder.Services.AddMapperProfiles();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
