using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xunit;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Features.Clients;
using KooliProjekt.Application.Features.Items;

namespace KooliProjektTestUnitas
{
    public class HandlersTests
    {
        private static DbContextOptions<ApplicationDbContext> CreateNewContextOptions()
        {
            return new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: System.Guid.NewGuid().ToString())
                .Options;
        }

        [Fact]
        public async Task GetClientsHandler_Returns_Added_Client()
        {
            var options = CreateNewContextOptions();

            await using (var context = new ApplicationDbContext(options))
            {
                var client = new Client { Name = "HandlerTest", Email = "h@test", Address = "Addr", Phone = "555", Discount = 0m };
                context.Clients.Add(client);
                await context.SaveChangesAsync();

                var repository = new ClientRepository(context);
                var handler = new GetClientsHandler(repository);
                var result = await handler.Handle(new GetClientsQuery(), CancellationToken.None);

                Assert.NotNull(result);
                Assert.True(result.Any(c => c.Email == "h@test"));
            }
        }

        [Fact]
        public async Task GetItemsHandler_Returns_Added_Item()
        {
            var options = CreateNewContextOptions();

            await using (var context = new ApplicationDbContext(options))
            {
                var category = new Category { Name = "CatTest", Description = "D" };
                context.Categories.Add(category);
                await context.SaveChangesAsync();

                var item = new Item
                {
                    CategoryId = category.Id,
                    Name = "HandlerItem",
                    Description = "desc",
                    Price = 2.5m,
                    Stock = 3,
                    Photo = null
                };
                context.Items.Add(item);
                await context.SaveChangesAsync();

                var repository = new ItemRepository(context);
                var handler = new GetItemsHandler(repository);
                var result = await handler.Handle(new GetItemsQuery(), CancellationToken.None);

                Assert.NotNull(result);
                Assert.True(result.Any(i => i.Name == "HandlerItem"));
            }
        }
    }
}