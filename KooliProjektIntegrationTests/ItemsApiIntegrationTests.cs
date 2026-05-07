using System.Net;
using System.Net.Http.Json;
using KooliProjekt.Application.Data;

namespace KooliProjektIntegrationTests;

public class ItemsApiIntegrationTests
{
    [Fact]
    public async Task GetItems_Returns_Seeded_Item()
    {
        using var factory = new CustomWebApplicationFactory();
        using var client = factory.CreateClient();

        await factory.ExecuteDbContextAsync(async dbContext =>
        {
            var category = new Category { Name = "Raamatud", Description = "Lugemismaterjal" };
            dbContext.Categories.Add(category);
            await dbContext.SaveChangesAsync();

            dbContext.Items.Add(new Item
            {
                CategoryId = category.Id,
                Name = "C# käsiraamat",
                Description = "Õpperaamat",
                Price = 19.99m,
                Stock = 8,
                Photo = "book.jpg"
            });

            await dbContext.SaveChangesAsync();
        });

        var response = await client.GetAsync("/api/Items");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var items = await response.Content.ReadFromJsonAsync<List<ItemResponse>>();
        Assert.NotNull(items);
        Assert.Contains(items, item => item.Name == "C# käsiraamat");
    }

    [Fact]
    public async Task PostItems_Creates_New_Item()
    {
        using var factory = new CustomWebApplicationFactory();
        using var client = factory.CreateClient();

        var categoryId = 0;

        await factory.ExecuteDbContextAsync(async dbContext =>
        {
            var category = new Category { Name = "Kontor", Description = "Kontorikaubad" };
            dbContext.Categories.Add(category);
            await dbContext.SaveChangesAsync();
            categoryId = category.Id;
        });

        var request = new CreateItemRequest(categoryId, "Pastakas", "Sinine tint", 1.50m, 100, "pen.png");

        var response = await client.PostAsJsonAsync("/api/Items", request);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var createdId = await response.Content.ReadFromJsonAsync<int>();
        Assert.True(createdId > 0);

        await factory.ExecuteDbContextAsync(dbContext =>
        {
            Assert.Contains(dbContext.Items, item => item.Name == "Pastakas");
            return Task.CompletedTask;
        });
    }

    private sealed record CreateItemRequest(
        int CategoryId,
        string Name,
        string Description,
        decimal Price,
        int Stock,
        string Photo);

    private sealed class ItemResponse
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string Photo { get; set; } = string.Empty;
    }
}