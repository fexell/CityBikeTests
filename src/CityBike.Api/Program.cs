using CityBike.Api;
using CityBike.Core;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<PriceCalculator>();
builder.Services.AddSingleton<IBikeRepository, InMemoryBikeRepository>();

var app = builder.Build();

app.MapGet("/api/price", (int seconds, PriceCalculator calculator) =>
{
    if (seconds < 0)
    {
        return Results.BadRequest(new { error = "seconds får inte vara negativt." });
    }

    return Results.Ok(new { seconds, price = calculator.Calculate(seconds) });
});

app.MapGet("/api/bikes/{id:int}", (int id, IBikeRepository bikes) =>
{
    Bike? bike = bikes.GetById(id);

    return bike is null ? Results.NotFound() : Results.Ok(bike);
});

app.MapGet("/api/admin/customers", (HttpContext context) =>
{
    bool hasKey = context.Request.Headers.TryGetValue("X-Api-Key", out var key);

    if (!hasKey || key != "test-admin-key")
    {
        return Results.Unauthorized();
    }

    return Results.Ok(new[] { "Anna Svensson", "Bo Karlsson" });
});

app.Run();

public partial class Program { }
