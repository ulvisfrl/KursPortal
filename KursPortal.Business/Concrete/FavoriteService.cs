using AutoMapper;
using KursPortal.Business.Abstract;
using KursPortal.DataAccess.Abstract;
using KursPortal.DTOs.DTOs.FavoriteDtos;
using KursPortal.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KursPortal.Business.Concrete
{
    public class FavoriteService : GenericService<Favorite>, IFavoriteService
    {
        readonly IFavoriteRepository _favoriteRepository;
        readonly IMapper _mapper;
        public FavoriteService(IRepository<Favorite> repository, IFavoriteRepository favoriteRepository, IMapper mapper) : base(repository)
        {
            _favoriteRepository = favoriteRepository;
            _mapper = mapper;
        }

        public async Task<string> AddToFavoriteAsync(Guid userId, Guid courseId)
        {
            var favorite = await _favoriteRepository.GetUserFavoriteAsync(userId);
            if (favorite == null)
            {
                favorite = new Favorite
                {
                    UserId = userId
                };
                await _favoriteRepository.AddAsync(favorite);
                await _favoriteRepository.SaveAsync();
            }

            if (favorite.FavoriteItems == null)
                favorite.FavoriteItems = new List<FavoriteItem>();

            if (favorite.FavoriteItems.Any(x => x.CourseId == courseId))
                return "Course already in favorite";

            favorite.FavoriteItems.Add(new FavoriteItem
            {
                CourseId = courseId
            });

            await _favoriteRepository.SaveAsync();
            return "Course added to favorite";
        }

        public async Task ClearFavoriteAsync(Guid userId)
        {
            var favorite = await _favoriteRepository.GetUserFavoriteAsync(userId);
            if (favorite != null && favorite.FavoriteItems != null && favorite.FavoriteItems.Any())
            {
                favorite.FavoriteItems.Clear();
                await _favoriteRepository.SaveAsync();
            }
        }

        public async Task<ResultFavoriteDto> GetUserFavoriteAsync(Guid userId)
        {
            var favorite = await _favoriteRepository.GetUserFavoriteAsync(userId);
            if (favorite == null)
                return new ResultFavoriteDto();
            return _mapper.Map<ResultFavoriteDto>(favorite);
        }

        public async Task<string> RemoveFromFavoriteAsync(Guid userId, Guid courseId)
        {
            var favorite = await _favoriteRepository.GetUserFavoriteAsync(userId);
            if (favorite == null || favorite.FavoriteItems == null)
                return "Favorite not found";

            var item = favorite.FavoriteItems.FirstOrDefault(x => x.CourseId == courseId);
            if (item == null)
                return "Course not found favorite";

            favorite.FavoriteItems.Remove(item);
            await _favoriteRepository.SaveAsync();

            return "Course removed from favorite";
        }
    }
}
