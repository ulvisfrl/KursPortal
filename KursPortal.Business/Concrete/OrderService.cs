using KursPortal.Business.Abstract;
using KursPortal.DataAccess.Abstract;
using KursPortal.Entity.Entities;
using Microsoft.Extensions.Configuration;
using Stripe;
using Stripe.Checkout;
using Stripe.FinancialConnections;

namespace KursPortal.Business.Concrete
{
    public class OrderService : GenericService<Order>, IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IRepository<UserCourse> _userCourseRepository;
        private readonly IConfiguration _configuration;

        public OrderService(
            IRepository<Order> repository,
            IOrderRepository orderRepository,
            ICartRepository cartRepository,
            IRepository<UserCourse> userCourseRepository,
            IConfiguration configuration)
            : base(repository)
        {
            _orderRepository = orderRepository;
            _cartRepository = cartRepository;
            _userCourseRepository = userCourseRepository;
            _configuration = configuration;
        }

        public async Task<string> CheckoutAsync(Guid userId)
        {
            var cart = await _cartRepository.GetUserCartAsync(userId);

            if (cart == null || !cart.CartItems.Any())
                throw new Exception("Cart is empty");

            var order = new Order
            {
                UserId = userId,
                TotalPrice = cart.CartItems.Sum(x => x.Course.DiscountPrice ?? x.Course.Price),
                Status = "Pending",
                IsPaid = false,
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

            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];

            var options = new Stripe.Checkout.SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                Mode = "payment",

                LineItems = cart.CartItems.Select(x => new Stripe.Checkout.SessionLineItemOptions
                {
                    Quantity = 1,
                    PriceData = new Stripe.Checkout.SessionLineItemPriceDataOptions
                    {
                        Currency = "azn",
                        UnitAmount = (long)((x.Course.DiscountPrice ?? x.Course.Price) * 100),
                        ProductData = new Stripe.Checkout.SessionLineItemPriceDataProductDataOptions
                        {
                            Name = x.Course.Title
                        }
                    }
                }).ToList(),

                SuccessUrl = $"https://localhost:44356/Order/Success?orderId={order.Id}",
                CancelUrl = "https://localhost:44310/Cart/Index"
            };

            var service = new Stripe.Checkout.SessionService();
            var session = await service.CreateAsync(options);

            order.StripeSessionId = session.Id;
            await _orderRepository.SaveAsync();

            return session.Url;
        }

        public async Task<bool> ConfirmPaymentAsync(Guid orderId)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            if (order == null) return false;
            if (order.IsPaid) return true;

            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];
            var service = new Stripe.Checkout.SessionService();
            var session = await service.GetAsync(order.StripeSessionId);

            if (session.PaymentStatus != "paid")
                throw new Exception("Payment has not been made via Stripe.");

            order.IsPaid = true;
            order.Status = "Completed";

            foreach (var item in order.OrderItems)
            {
                var exists = (await _userCourseRepository.GetWhereAsync(x =>
                    x.UserId == order.UserId &&
                    x.CourseId == item.CourseId)).Any();

                if (!exists)
                {
                    await _userCourseRepository.AddAsync(new UserCourse
                    {
                        UserId = order.UserId,
                        CourseId = item.CourseId
                    });
                }
            }

            _orderRepository.Update(order);

            // Səbəti təmizləmək (İstəyə bağlı, lakin tövsiyə olunur)
            var cart = await _cartRepository.GetUserCartAsync(order.UserId);
            if (cart != null)
            {
                cart.CartItems.Clear();
                await _cartRepository.SaveAsync();
            }

            return true;
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