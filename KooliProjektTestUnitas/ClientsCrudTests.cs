using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xunit;
using KooliProjekt.Application.Data;

namespace KooliProjektTestUnitas
{
    public class ClientsCrudTests
    {
        private static DbContextOptions<ApplicationDbContext> CreateNewContextOptions()
        {
            return new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: System.Guid.NewGuid().ToString())
                .Options;
        }

        [Fact]
        public async Task CreateClient_Adds_Client_To_DbContext()
        {
            var options = CreateNewContextOptions();

            await using (var context = new ApplicationDbContext(options))
            {
                var client = new Client { Name = "Alice", Email = "alice@test", Address = "Addr", Phone = "555", Discount = 0m };
                context.Clients.Add(client);
                await context.SaveChangesAsync();

                Assert.True(client.Id > 0);
            }

            await using (var verify = new ApplicationDbContext(options))
            {
                var saved = await verify.Clients.FirstOrDefaultAsync(c => c.Email == "alice@test");
                Assert.NotNull(saved);
                Assert.Equal("Alice", saved.Name);
            }
        }

        [Fact]
        public async Task GetClients_Returns_List_From_DbContext()
        {
            var options = CreateNewContextOptions();

            await using (var context = new ApplicationDbContext(options))
            {
                context.Clients.Add(new Client { Name = "A", Email = "a@test", Address = "x", Phone = "1", Discount = 0m });
                context.Clients.Add(new Client { Name = "B", Email = "b@test", Address = "y", Phone = "2", Discount = 0m });
                await context.SaveChangesAsync();
            }

            await using (var verify = new ApplicationDbContext(options))
            {
                var clients = await verify.Clients.ToListAsync();
                Assert.True(clients.Count >= 2);
            }
        }
    }
}