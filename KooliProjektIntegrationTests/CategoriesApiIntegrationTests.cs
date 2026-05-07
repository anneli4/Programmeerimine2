using System.Net;
using System.Net.Http.Json;
using KooliProjekt.Application.Data;

namespace KooliProjektIntegrationTests;

public class CategoriesApiIntegrationTests
{
    [Fact]
    public async Task GetCategories_Returns_Seeded_Category()
    {
        using var factory = new CustomWebApplicationFactory();
        using var client = factory.CreateClient();

        await factory.ExecuteDbContextAsync(async dbContext =>
        {
            dbContext.Categories.Add(new Category
            {
                Name = "Joogid",
                Description = "Karastusjoogid ja mahlad"
            });

            await dbContext.SaveChangesAsync();
        });

        var response = await client.GetAsync("/api/Categories");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var categories = await response.Content.ReadFromJsonAsync<List<CategoryResponse>>();
        Assert.NotNull(categories);
        Assert.Contains(categories, category => category.Name == "Joogid");
    }

    [Fact]
    public async Task PostCategories_Creates_New_Category()
    {
        using var factory = new CustomWebApplicationFactory();
        using var client = factory.CreateClient();

        var request = new CreateCategoryRequest("Tehnika", "Arvutid ja lisad");

        var response = await client.PostAsJsonAsync("/api/Categories", request);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var createdId = await response.Content.ReadFromJsonAsync<int>();
        Assert.True(createdId > 0);

        await factory.ExecuteDbContextAsync(dbContext =>
        {
            Assert.Contains(dbContext.Categories, category => category.Name == "Tehnika");
            return Task.CompletedTask;
        });
    }

    private sealed record CreateCategoryRequest(string Name, string Description);

    private sealed class CategoryResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}