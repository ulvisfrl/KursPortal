using KursPortal.DataAccess.Abstract;
using KursPortal.DataAccess.Context;
using KursPortal.DTOs.DTOs.BlogCategoryDtos;
using KursPortal.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KursPortal.DataAccess.Concrete
{
    public class BlogCategoryRepository : Repository<BlogCategory>, IBlogCategoryRepository
    {
        readonly AppDbContext _context;
        public BlogCategoryRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<ResultBlogCategoryDto>> GetCategoriesWithBlogCountAsync()
        {
            return await _context.BlogCategories.Select(x => new ResultBlogCategoryDto
            {
                Id = x.Id,
                BlogCategoryName = x.BlogCategoryName,
                Count = x.Blogs.Count
            }).ToListAsync();
        }
    }
}
