using KursPortal.DataAccess.Abstract;
using KursPortal.DataAccess.Context;
using KursPortal.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KursPortal.DataAccess.Concrete
{
    public class FavoriteRepository : Repository<Favorite>, IFavoriteRepository
    {
        readonly AppDbContext _context;
        public FavoriteRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Favorite> GetUserFavoriteAsync(Guid userId)
        {
            return await _context.Favorites
                .Include(x => x.FavoriteItems)
                .ThenInclude(x => x.Course)
                .FirstOrDefaultAsync(x => x.UserId == userId);
        }
    }
}
