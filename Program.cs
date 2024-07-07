using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Microsoft.AspNetCore.Mvc;
using DotNetEnv;
using Urri;
using Urri.Entities;

var builder = WebApplication.CreateBuilder(args);

Env.Load();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDynamoDatabase();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};


app.MapPost("/new", async ([FromServices] IAmazonDynamoDB _dynamo, [FromBody] string target) =>
{
    var code = NanoidDotNet.Nanoid.Generate(size: 4);

    var context = new DynamoDBContext(_dynamo);

    var item = new UriShorter(
        UrlCode: code,
        Target: target
        );
    ;

    await context.SaveAsync(item);

    return code;
})
.WithName("Short Url")
.WithOpenApi()
;

app.MapGet("/{code}", async ([FromServices] IAmazonDynamoDB _dynamo, string code) =>
{
    var context = new DynamoDBContext(_dynamo);

    var res = await context.LoadAsync<UriShorter>(code);

    return Results.Redirect(res.Target, permanent: true); // Or use false for temporary
})
.WithName("GetUrl")
.WithOpenApi()
;

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
