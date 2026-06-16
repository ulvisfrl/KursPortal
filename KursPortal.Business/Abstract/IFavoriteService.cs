using KursPortal.DTOs.DTOs.FavoriteDtos;
using KursPortal.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KursPortal.Business.Abstract
{
    public interface IFavoriteService : IGenericService<Favorite>
    {
        Task<ResultFavoriteDto> GetUserFavoriteAsync(Guid userId);
        Task<string> AddToFavoriteAsync(Guid userId, Guid courseId);
        Task<string> RemoveFromFavoriteAsync(Guid userId, Guid courseId);
        Task ClearFavoriteAsync(Guid userId);
    }
}
