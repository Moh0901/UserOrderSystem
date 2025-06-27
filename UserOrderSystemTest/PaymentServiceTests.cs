using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserOrderSystem.Models;
using UserOrderSystem.Services;

namespace UserOrderSystemTest
{
    public class PaymentServiceTests
    {
        [Fact]
        public void ProcessPayment_Throws_WhenOrderIsNull()
        {
            var service = new PaymentService();

            Assert.Throws<InvalidOperationException>(() => service.ProcessPayment(null));
        }

        [Fact]
        public void ProcessPayment_Throws_WhenAlreadyPaid()
        {
            var service = new PaymentService();
            var order = new Order { Id = 1, IsPaid = true };

            Assert.Throws<InvalidOperationException>(() => service.ProcessPayment(order));
        }

        [Fact]
        public void ProcessPayment_Succeeds_WhenValid()
        {
            var service = new PaymentService();
            var order = new Order { Id = 1, IsPaid = false };

            service.ProcessPayment(order);

            Assert.True(order.IsPaid);
        }
        [Fact]
        public void ProcessPayment_Succeeds_WhenOrderHasMissingOptionalData()
        {
            var service = new PaymentService();
            var order = new Order { Id = 42, IsPaid = false, ItemName = null! }; // nullable string

            service.ProcessPayment(order);

            Assert.True(order.IsPaid);
        }

        [Fact]
        public void ProcessPayment_ThrowsWithMessage_WhenAlreadyPaid()
        {
            var service = new PaymentService();
            var order = new Order { Id = 1, IsPaid = true };

            var ex = Assert.Throws<InvalidOperationException>(() => service.ProcessPayment(order));

            Assert.Equal("Order is already paid.", ex.Message);
        }

    }
}
