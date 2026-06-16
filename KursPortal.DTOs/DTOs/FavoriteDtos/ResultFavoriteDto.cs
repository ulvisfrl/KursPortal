using KursPortal.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KursPortal.DTOs.DTOs.FavoriteDtos
{
    public class ResultFavoriteDto
    {
        public Guid Id { get; set; }
        public ICollection<FavoriteItemDto> FavoriteItems { get; set; }
    }
}
