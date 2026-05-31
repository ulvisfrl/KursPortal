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
    public class BlogRepository : Repository<Blog>, IBlogRepository
    {
        readonly AppDbContext _context;
        public BlogRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Blog>> GetBlogWithDetailsAsync()
        {
            return await _context.Blogs.Include(x => x.BlogCategory).Include(x => x.BlogComments).Include(x => x.Teacher).ToListAsync();
        }

        public async Task<Blog> GetBlogWithDetailsAsync(Guid id)
        {
            return await _context.Blogs.Include(x => x.BlogCategory).Include(x => x.BlogComments).Include(x => x.Teacher).FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
