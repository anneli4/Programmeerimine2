using System.Net;
using System.Net.Http.Json;
using KooliProjekt.Application.Data;

namespace KooliProjektIntegrationTests;

public class ClientsApiIntegrationTests
{
    [Fact]
    public async Task GetClients_Returns_Seeded_Client()
    {
        using var factory = new CustomWebApplicationFactory();
        using var client = factory.CreateClient();

        await factory.ExecuteDbContextAsync(async dbContext =>
        {
            dbContext.Clients.Add(new Client
            {
                Name = "Mari Maasikas",
                Email = "mari@test.ee",
                Address = "Tartu 1",
                Phone = "5551234",
                Discount = 5m
            });

            await dbContext.SaveChangesAsync();
        });

        var response = await client.GetAsync("/api/Clients");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var clients = await response.Content.ReadFromJsonAsync<List<ClientResponse>>();

        Assert.NotNull(clients);
        Assert.Contains(clients, item => item.Email == "mari@test.ee");
    }

    [Fact]
    public async Task PostClients_Creates_New_Client()
    {
        using var factory = new CustomWebApplicationFactory();
        using var client = factory.CreateClient();

        var request = new CreateClientRequest(
            "Jaan Tamm",
            "jaan@test.ee",
            "Tallinn 2",
            "5556789",
            10m);

        var response = await client.PostAsJsonAsync("/api/Clients", request);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var createdId = await response.Content.ReadFromJsonAsync<int>();
        Assert.True(createdId > 0);

        await factory.ExecuteDbContextAsync(dbContext =>
        {
            Assert.Contains(dbContext.Clients, item => item.Email == "jaan@test.ee");
            return Task.CompletedTask;
        });
    }

    [Fact]
    public async Task DeleteClient_Removes_Existing_Client()
    {
        using var factory = new CustomWebApplicationFactory();
        using var client = factory.CreateClient();

        var clientId = 0;

        await factory.ExecuteDbContextAsync(async dbContext =>
        {
            var entity = new Client
            {
                Name = "Kustuta Mind",
                Email = "delete@test.ee",
                Address = "Parnu 3",
                Phone = "5000000",
                Discount = 0m
            };

            dbContext.Clients.Add(entity);
            await dbContext.SaveChangesAsync();
            clientId = entity.Id;
        });

        var response = await client.DeleteAsync($"/api/Clients/{clientId}");

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

        await factory.ExecuteDbContextAsync(dbContext =>
        {
            Assert.DoesNotContain(dbContext.Clients, item => item.Id == clientId);
            return Task.CompletedTask;
        });
    }

    private sealed record CreateClientRequest(
        string Name,
        string Email,
        string Address,
        string Phone,
        decimal Discount);

    private sealed class ClientResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public decimal Discount { get; set; }
    }
}