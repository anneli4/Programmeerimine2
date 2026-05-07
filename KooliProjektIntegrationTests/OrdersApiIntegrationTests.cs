using System.Net;
using System.Net.Http.Json;
using KooliProjekt.Application.Data;

namespace KooliProjektIntegrationTests;

public class OrdersApiIntegrationTests
{
    [Fact]
    public async Task GetOrders_Returns_Seeded_Order()
    {
        using var factory = new CustomWebApplicationFactory();
        using var client = factory.CreateClient();

        await factory.ExecuteDbContextAsync(async dbContext =>
        {
            var customer = new Client
            {
                Name = "Tellija",
                Email = "tellija@test.ee",
                Address = "Narva 5",
                Phone = "5511111",
                Discount = 2m
            };

            dbContext.Clients.Add(customer);
            await dbContext.SaveChangesAsync();

            dbContext.Orders.Add(new Order
            {
                ClientId = customer.Id,
                Date = new DateTime(2026, 5, 7, 10, 0, 0),
                Discount = 1m
            });

            await dbContext.SaveChangesAsync();
        });

        var response = await client.GetAsync("/api/Orders");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var orders = await response.Content.ReadFromJsonAsync<List<OrderResponse>>();
        Assert.NotNull(orders);
        Assert.Contains(orders, order => order.ClientId > 0);
    }

    [Fact]
    public async Task PostOrders_Creates_New_Order()
    {
        using var factory = new CustomWebApplicationFactory();
        using var client = factory.CreateClient();

        var clientId = 0;

        await factory.ExecuteDbContextAsync(async dbContext =>
        {
            var customer = new Client
            {
                Name = "Juku",
                Email = "juku@test.ee",
                Address = "Pikk 7",
                Phone = "5522222",
                Discount = 0m
            };

            dbContext.Clients.Add(customer);
            await dbContext.SaveChangesAsync();
            clientId = customer.Id;
        });

        var request = new CreateOrderRequest(new DateTime(2026, 5, 7, 12, 0, 0), clientId, 3m);

        var response = await client.PostAsJsonAsync("/api/Orders", request);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var createdId = await response.Content.ReadFromJsonAsync<int>();
        Assert.True(createdId > 0);

        await factory.ExecuteDbContextAsync(dbContext =>
        {
            Assert.Contains(dbContext.Orders, order => order.ClientId == clientId);
            return Task.CompletedTask;
        });
    }

    private sealed record CreateOrderRequest(DateTime Date, int ClientId, decimal Discount);

    private sealed class OrderResponse
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int ClientId { get; set; }
        public decimal Discount { get; set; }
    }
}