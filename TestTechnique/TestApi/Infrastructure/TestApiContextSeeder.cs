using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestApi.Model;

namespace TestApi.Infrastructure
{
    public class TestApiContextSeeder
    {
        public async Task SeedAsync(TestApiDbContext context)
        {
            if(await context.Products.AnyAsync())
                return;
            
            var random = new Random();
            var products = Enumerable.Range(1, 100).Select(x => new Product
            {
                Name = $"Product - {x}",
                Reference = $"Ref-{x:001}"
            });

            await context.AddRangeAsync(products);
            var customers = Enumerable.Range(1, 5).Select(x => new Customer
            {
                Firstname = $"Firstname - {x}",
                Lastname = $"Lastname-{x:001}",
                Email = $"test{x}@citeo.com",
                AccountNumber = $"Account{x}"
            });
            
            await context.AddRangeAsync(customers);

            foreach (var customer in customers)
            {
                var productIds = random.Next(10, 60);
                for (var i = 1; i < productIds; i++)
                {
                    var salesOrder = new SalesOrder
                    {
                        Customer = customer,
                        Status = "Test",
                        OrderDate = DateTime.UtcNow,
                        OrderItems = Enumerable.Range(1, productIds).Select(x => new SalesOrderItem
                        {
                            ProductId = x,
                            Quantity = x,
                            TrackingNumber = "12345",
                            UnitPrice = 2536.59m,
                        }).ToList()
                    };
                    await context.AddAsync(salesOrder);
                    customer.SalesOrders.Add(salesOrder);
                }
            }

            await context.SaveChangesAsync();
        }
    }
}