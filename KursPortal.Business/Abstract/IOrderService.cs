using KursPortal.Entity.Entities;

namespace KursPortal.Business.Abstract
{
    public interface IOrderService : IGenericService<Order>
    {
        Task<string> CheckoutAsync(Guid userId);

        Task<List<Order>> GetUserOrdersAsync(Guid userId);

        Task<Order> GetOrderByIdAsync(Guid orderId);
        Task<bool> ConfirmPaymentAsync(Guid userId);
    }
}