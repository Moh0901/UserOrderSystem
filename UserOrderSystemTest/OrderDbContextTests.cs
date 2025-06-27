using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserOrderSystem.Context;
using UserOrderSystem.Models;

namespace UserOrderSystemTest
{
    public class OrderDbContextTests
    {
        [Fact]
        public async Task AddOrder_SavesToInMemoryDatabase()
        {
            var options = new DbContextOptionsBuilder<OrderDbContext>()
                .UseInMemoryDatabase(databaseName: "TestOrders")
                .Options;

            using (var context = new OrderDbContext(options))
            {
                var order = new Order { Id = 1, ItemName = "Widget", IsPaid = false };
                context.Orders.Add(order);
                await context.SaveChangesAsync();
            }

            using (var context = new OrderDbContext(options))
            {
                var retrieved = await context.Orders.FindAsync(1);
                Assert.NotNull(retrieved);
                Assert.Equal("Widget", retrieved!.ItemName);
            }
        }
    }
}