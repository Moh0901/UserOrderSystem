using UserOrderSystem.Models;

namespace UserOrderSystem.Services
{
    public class PaymentService
    {
        public void ProcessPayment(Order? order)
        {
            if (order == null)
                throw new InvalidOperationException("Order cannot be null.");

            if (order.IsPaid)
                throw new InvalidOperationException("Order is already paid.");

            order.IsPaid = true;
        }
    }
}
