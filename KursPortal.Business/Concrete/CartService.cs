using AutoMapper;
using KursPortal.Business.Abstract;
using KursPortal.DataAccess.Abstract;
using KursPortal.DataAccess.Context;
using KursPortal.DTOs.DTOs.CartDtos;
using KursPortal.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KursPortal.Business.Concrete
{
    public class CartService : GenericService<Cart>, ICartService
    {
        readonly ICartRepository _cartRepository;
        readonly IMapper _mapper;
        public CartService(IRepository<Cart> repository, ICartRepository cartRepository, IMapper mapper) : base(repository)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
        }

        public async Task<string> AddToCartAsync(Guid userId, Guid courseId)
        {
            var cart = await _cartRepository.GetUserCartAsync(userId);

            if (cart == null)
            {
                cart = new Cart
                {
                    UserId = userId
                };

                await _cartRepository.AddAsync(cart);
                await _cartRepository.SaveAsync(); 
            }

            if (cart.CartItems == null)
                cart.CartItems = new List<CartItem>();

            if (cart.CartItems.Any(x => x.CourseId == courseId))
                return "Course already in cart";

            cart.CartItems.Add(new CartItem
            {
                CourseId = courseId
            });

            await _cartRepository.SaveAsync();

            return "Course added to cart";
        }

        public async Task<ResultCartDto> GetUserCartAsync(Guid userId)
        {
            var cart = await _cartRepository.GetUserCartAsync(userId);
            if (cart == null)
                return new ResultCartDto();

            return _mapper.Map<ResultCartDto>(cart);
        }

        public async Task<string> RemoveFromCartAsync(Guid userId, Guid courseId)
        {
            var cart = await _cartRepository.GetUserCartAsync(userId);

            if (cart == null || cart.CartItems == null)
                return "Cart not found";

            var item = cart.CartItems.FirstOrDefault(x => x.CourseId == courseId);

            if (item == null)
                return "Course not found in cart";

            cart.CartItems.Remove(item);

            await _cartRepository.SaveAsync();

            return "Course removed from cart";
        }

        public async Task ClearCartAsync(Guid userId)
        {
            var cart = await _cartRepository.GetUserCartAsync(userId);

            if (cart != null && cart.CartItems != null && cart.CartItems.Any())
            {
                cart.CartItems.Clear();
                await _cartRepository.SaveAsync();
            }
        }
    }
}
