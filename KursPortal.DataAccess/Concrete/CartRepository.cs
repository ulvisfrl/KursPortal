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
    public class CartRepository : Repository<Cart>, ICartRepository
    {
        readonly AppDbContext _context;
        public CartRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Cart> GetUserCartAsync(Guid userId)
        {
            return await _context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(c => c.Course)
                .FirstOrDefaultAsync(c => c.UserId == userId);
        }
    }
}
