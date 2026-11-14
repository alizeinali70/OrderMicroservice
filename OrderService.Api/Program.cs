using Microsoft.EntityFrameworkCore;
using OrderService.Api.DTOs;
using OrderService.Application;
using OrderService.Infrastructure.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IOrderService, OrderService.Infrastructure.Services.OrderService>();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<OrderDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddHealthChecks();

builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//using (var scope = app.Services.CreateScope())
//{
//    var dbContext = scope.ServiceProvider.GetRequiredService<OrderDbContext>();
//    dbContext.Database.EnsureCreated();
//}

app.MapGet("/health", () => Results.Ok("Healthy"));

app.MapGet("/orders", async (IOrderService orderService, CancellationToken cancellationToken) =>
{
    var orders = await orderService.GetAllOrdersAsync(cancellationToken);
    return Results.Ok(orders);
});

app.MapPost("/orders", async (CreateOrderRequest request, IOrderService orderService, CancellationToken cancellationToken) =>
{
    try
    {
        var order = await orderService.CreateOrderAsync(request.TotalAmount, cancellationToken);
        return Results.Created($"/orders/{order.Id}", order);
    }
    catch (ArgumentException ex)
    {
        return Results.BadRequest(new { error = ex.Message });
    }
});

app.Run();


