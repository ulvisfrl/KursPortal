using KursPortal.DTOs.DTOs.CartDtos;
using KursPortal.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KursPortal.Business.Abstract
{
    public interface ICartService : IGenericService<Cart>
    {
        Task<ResultCartDto> GetUserCartAsync(Guid userId);
        Task<string> AddToCartAsync(Guid userId, Guid courseId);
        Task<string> RemoveFromCartAsync(Guid userId, Guid courseId);
        Task ClearCartAsync(Guid userId);

    }
}
