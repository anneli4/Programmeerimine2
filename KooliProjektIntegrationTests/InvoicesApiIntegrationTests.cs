using System.Net;
using System.Net.Http.Json;
using KooliProjekt.Application.Data;

namespace KooliProjektIntegrationTests;

public class InvoicesApiIntegrationTests
{
    [Fact]
    public async Task GetInvoices_Returns_Seeded_Invoice()
    {
        using var factory = new CustomWebApplicationFactory();
        using var client = factory.CreateClient();

        await factory.ExecuteDbContextAsync(async dbContext =>
        {
            var customer = new Client
            {
                Name = "Arve klient",
                Email = "arve@test.ee",
                Address = "Mere 8",
                Phone = "5533333",
                Discount = 0m
            };

            dbContext.Clients.Add(customer);
            await dbContext.SaveChangesAsync();

            var order = new Order
            {
                ClientId = customer.Id,
                Date = new DateTime(2026, 5, 7, 13, 0, 0),
                Discount = 0m
            };

            dbContext.Orders.Add(order);
            await dbContext.SaveChangesAsync();

            dbContext.Invoices.Add(new Invoice
            {
                InvoiceNumber = "ARV-2026-001",
                OrderId = order.Id,
                ClientId = customer.Id,
                Date = new DateTime(2026, 5, 7, 14, 0, 0),
                TotalAmount = 25m,
                Discount = 0m,
                Paid = 25m
            });

            await dbContext.SaveChangesAsync();
        });

        var response = await client.GetAsync("/api/Invoices");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var invoices = await response.Content.ReadFromJsonAsync<List<InvoiceResponse>>();
        Assert.NotNull(invoices);
        Assert.Contains(invoices, invoice => invoice.InvoiceNumber == "ARV-2026-001");
    }

    [Fact]
    public async Task PostInvoices_Creates_New_Invoice()
    {
        using var factory = new CustomWebApplicationFactory();
        using var client = factory.CreateClient();

        var clientId = 0;
        var orderId = 0;

        await factory.ExecuteDbContextAsync(async dbContext =>
        {
            var customer = new Client
            {
                Name = "Maksja",
                Email = "maksja@test.ee",
                Address = "Pargi 9",
                Phone = "5544444",
                Discount = 0m
            };

            dbContext.Clients.Add(customer);
            await dbContext.SaveChangesAsync();
            clientId = customer.Id;

            var order = new Order
            {
                ClientId = clientId,
                Date = new DateTime(2026, 5, 7, 15, 0, 0),
                Discount = 0m
            };

            dbContext.Orders.Add(order);
            await dbContext.SaveChangesAsync();
            orderId = order.Id;
        });

        var request = new CreateInvoiceRequest(
            "ARV-2026-002",
            orderId,
            clientId,
            new DateTime(2026, 5, 7, 16, 0, 0),
            49.90m,
            4.90m,
            45.00m);

        var response = await client.PostAsJsonAsync("/api/Invoices", request);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var createdId = await response.Content.ReadFromJsonAsync<int>();
        Assert.True(createdId > 0);

        await factory.ExecuteDbContextAsync(dbContext =>
        {
            Assert.Contains(dbContext.Invoices, invoice => invoice.InvoiceNumber == "ARV-2026-002");
            return Task.CompletedTask;
        });
    }

    private sealed record CreateInvoiceRequest(
        string InvoiceNumber,
        int OrderId,
        int ClientId,
        DateTime Date,
        decimal TotalAmount,
        decimal Discount,
        decimal Paid);

    private sealed class InvoiceResponse
    {
        public int Id { get; set; }
        public string InvoiceNumber { get; set; } = string.Empty;
        public int OrderId { get; set; }
        public int ClientId { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal Discount { get; set; }
        public decimal Paid { get; set; }
    }
}