using KooliProjekt.Application.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Data
{
    public static class SeedData
    {
        public static async Task GenerateAsync(ApplicationDbContext context)
        {
            await context.Database.MigrateAsync();

            // Kui andmed juba olemas, ära lisa uuesti
            if (context.Clients.Any() || context.Items.Any() || context.Orders.Any() || context.Invoices.Any() || context.Categories.Any())
                return;

            // -----------------------------
            // Clients
            // -----------------------------
            var clients = new List<Client>
            {
                new Client { Name="Mari Maasikas", Email="mari@test.ee", Address="Tartu tn 5", Phone="55512345", Discount=0.05m },
                new Client { Name="Jüri Kask", Email="jyri@test.ee", Address="Tallinna mnt 10", Phone="55554321", Discount=0.10m },
                new Client { Name="Kati Lepik", Email="kati@test.ee", Address="Pärnu pst 3", Phone="55511122", Discount=0.00m },
                new Client { Name="Tarmo Tamm", Email="tarmo@test.ee", Address="Viljandi tn 8", Phone="55522233", Discount=0.08m },
                new Client { Name="Liina Lumi", Email="liina@test.ee", Address="Narva mnt 7", Phone="55533344", Discount=0.03m },
                new Client { Name="Peeter Puu", Email="peeter@test.ee", Address="Rakvere tn 12", Phone="55544455", Discount=0.15m },
                new Client { Name="Anneli Aas", Email="anneli@test.ee", Address="Kuressaare tn 4", Phone="55555566", Discount=0.00m },
                new Client { Name="Mati Mänd", Email="mati@test.ee", Address="Jõhvi tn 6", Phone="55566677", Discount=0.05m },
                new Client { Name="Triin Tamm", Email="triin@test.ee", Address="Paide tn 2", Phone="55577788", Discount=0.10m },
                new Client { Name="Kalle Kask", Email="kalle@test.ee", Address="Haapsalu tn 9", Phone="55588899", Discount=0.07m }
            };
            context.Clients.AddRange(clients);

            // -----------------------------
            // Categories
            // -----------------------------
            var categories = new List<Category>
            {
                new Category { Name="Kirjatarbed", Description="Kirjutusvahendid ja vihikud" },
                new Category { Name="Mööbel", Description="Toolid ja lauad" },
                new Category { Name="Elektroonika", Description="Telerid, arvutid, seadmed" },
                new Category { Name="Raamatud", Description="Erinevad raamatud" },
                new Category { Name="Mänguasjad", Description="Laste mänguasjad" },
                new Category { Name="Kodu ja Aiakaup", Description="Kodu ja aeda tarvikud" },
                new Category { Name="Toit", Description="Söögiained" },
                new Category { Name="Joogid", Description="Veinid, mahlad, vesi" },
                new Category { Name="Riided", Description="Riided ja jalatsid" },
                new Category { Name="Sporditarbed", Description="Sport ja vaba aeg" }
            };
            context.Categories.AddRange(categories);

            // -----------------------------
            // Items
            // -----------------------------
            var items = new List<Item>
            {
                new Item { Name="Pliiats", Description="HB pliiats", Price=1.20m, Stock=100, Category=categories[0] },
                new Item { Name="Vihik", Description="A4 vihik 64 lk", Price=2.50m, Stock=50, Category=categories[0] },
                new Item { Name="Tool", Description="Kontoritool", Price=45.00m, Stock=10, Category=categories[1] },
                new Item { Name="Laud", Description="Töölaud", Price=90.00m, Stock=5, Category=categories[1] },
                new Item { Name="Raamat", Description="Seiklusraamat", Price=12.50m, Stock=30, Category=categories[3] },
                new Item { Name="Mänguauto", Description="Laste mänguauto", Price=15.00m, Stock=25, Category=categories[4] },
                new Item { Name="Teler", Description="LED teler 42\"", Price=250.00m, Stock=8, Category=categories[2] },
                new Item { Name="Kohv", Description="Jahvatatud kohv", Price=5.00m, Stock=40, Category=categories[7] },
                new Item { Name="T-särk", Description="Puuvillane T-särk", Price=10.00m, Stock=60, Category=categories[8] },
                new Item { Name="Jalgpall", Description="Standard jalgpall", Price=20.00m, Stock=15, Category=categories[9] }
            };
            context.Items.AddRange(items);

            // -----------------------------
            // Orders ja Order_Items
            // -----------------------------
            var orders = new List<Order>();
            var rand = new Random();
            for (int i = 0; i < 10; i++)
            {
                var order = new Order
                {
                    Client = clients[rand.Next(clients.Count)],
                    Date = DateTime.Now.AddDays(-rand.Next(30)),
                    Discount = 0,
                    Order_Items = new List<Order_Item>()
                };

                int itemsCount = rand.Next(1, 4);
                for (int j = 0; j < itemsCount; j++)
                {
                    order.Order_Items.Add(new Order_Item
                    {
                        Item = items[rand.Next(items.Count)],
                        Quantity = rand.Next(1, 5),
                        UnitPrice = items[rand.Next(items.Count)].Price,
                        Discount = 0,
                        Total = 0
                    });
                }

                orders.Add(order);
            }
            context.Orders.AddRange(orders);

            // -----------------------------
            // Invoices ja Invoice_Lines
            // -----------------------------
            var invoices = new List<Invoice>();
            foreach (var order in orders)
            {
                var invoice = new Invoice
                {
                    Order = order,
                    ClientId = order.Client.Id,
                    InvoiceNumber = $"INV-{order.Id + 1000}",
                    Date = DateTime.Now,
                    Discount = order.Discount,
                    Paid = 0,
                    TotalAmount = 0,
                    Invoice_lines = new List<Invoice_Line>()
                };

                foreach (var oi in order.Order_Items)
                {
                    invoice.Invoice_lines.Add(new Invoice_Line
                    {
                        Item = oi.Item,
                        Quantity = oi.Quantity,
                        UnitPrice = oi.UnitPrice,
                        Discount = oi.Discount,
                        Total = oi.Quantity * oi.UnitPrice
                    });
                }

                invoice.TotalAmount = invoice.Invoice_lines.Sum(il => il.Total);
                invoices.Add(invoice);
            }
            context.Invoices.AddRange(invoices);

            // -----------------------------
            // Salvesta kõik
            // -----------------------------
            await context.SaveChangesAsync();
        }
    }
}
