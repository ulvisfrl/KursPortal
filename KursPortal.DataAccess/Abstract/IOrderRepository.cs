using KursPortal.Entity.Entities;

namespace KursPortal.DataAccess.Abstract
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<List<Order>> GetUserOrdersAsync(Guid userId);

        Task<Order> GetOrderByIdAsync(Guid orderId);
    }
}