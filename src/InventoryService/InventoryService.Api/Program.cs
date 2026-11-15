using InventoryService.Application.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddScoped<IInventoryService, InventoryService.Infrastructure.Services.InventoryService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();


app.Run();

app.MapGet("/health", () => Results.Ok("Inventory Service is healthy!"));

app.MapGet("/Inventory/{productId}", async (Guid productId, IInventoryService inventoryService, CancellationToken cancellationToken) =>
{
    var inventory = await inventoryService.GetInventoryByProductIdAsync(productId, cancellationToken);
    return inventory is not null ? Results.Ok(inventory) : Results.NotFound();
});
