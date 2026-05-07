using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xunit;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Features.Clients;
using KooliProjekt.Application.Features.Items;
using KooliProjekt.Application.Features.Orders;
using KooliProjekt.Application.Features.Invoices;

namespace KooliProjektTestUnitas
{
    public class UnitTests
    {
        private static DbContextOptions<ApplicationDbContext> CreateNewContextOptions()
        {
            return new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: System.Guid.NewGuid().ToString())
                .Options;
        }

        [Fact]
        public async Task DeleteClientCommandHandler_Removes_Client()
        {
            var options = CreateNewContextOptions();

            await using (var context = new ApplicationDbContext(options))
            {
                var client = new Client { Name = "T", Email = "t@test", Address = "A", Phone = "P", Discount = 0m };
                context.Clients.Add(client);
                await context.SaveChangesAsync();

                var handler = new DeleteClientCommandHandler(context);
                var command = new DeleteClientCommand { Id = client.Id };

                var result = await handler.Handle(command, CancellationToken.None);

                Assert.True(result);
                Assert.Null(await context.Clients.FindAsync(client.Id));
            }
        }

        // ... other tests unchanged ...
    }
}