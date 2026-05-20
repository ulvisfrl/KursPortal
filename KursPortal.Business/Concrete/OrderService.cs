using KursPortal.Business.Abstract;
using KursPortal.DataAccess.Abstract;
using KursPortal.Entity.Entities;

namespace KursPortal.Business.Concrete
{
    public class OrderService : GenericService<Order>, IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IRepository<UserCourse> _userCourseRepository;

        public OrderService(
            IRepository<Order> repository,
            IOrderRepository orderRepository,
            ICartRepository cartRepository,
            IRepository<UserCourse> userCourseRepository)
            : base(repository)
        {
            _orderRepository = orderRepository;
            _cartRepository = cartRepository;
            _userCourseRepository = userCourseRepository;
        }

        public async Task CheckoutAsync(Guid userId)
        {
            var cart = await _cartRepository.GetUserCartAsync(userId);

            if (cart is null || cart.CartItems is null || !cart.CartItems.Any())
                throw new Exception("Cart is empty");

            decimal totalPrice = cart.CartItems
                .Sum(x => x.Course.DiscountPrice ?? x.Course.Price);

            var order = new Order
            {
                UserId = userId,
                TotalPrice = totalPrice,
                IsPaid = true,
                OrderItems = new List<OrderItem>()
            };

            foreach (var item in cart.CartItems)
            {
                order.OrderItems.Add(new OrderItem
                {
                    CourseId = item.CourseId,
                    Price = item.Course.DiscountPrice ?? item.Course.Price
                });
            }

            await _orderRepository.AddAsync(order);

            foreach (var item in cart.CartItems)
            {
                bool exists = (await _userCourseRepository.GetWhereAsync(x =>
                    x.UserId == userId &&
                    x.CourseId == item.CourseId))
                    .Any();

                if (!exists)
                {
                    await _userCourseRepository.AddAsync(new UserCourse
                    {
                        UserId = userId,
                        CourseId = item.CourseId
                    });
                }
            }

            cart.CartItems.Clear();

            await _orderRepository.SaveAsync();
        }

        public async Task<Order> GetOrderByIdAsync(Guid orderId)
        {
            return await _orderRepository.GetOrderByIdAsync(orderId);
        }

        public async Task<List<Order>> GetUserOrdersAsync(Guid userId)
        {
            return await _orderRepository.GetUserOrdersAsync(userId);
        }
    }
}