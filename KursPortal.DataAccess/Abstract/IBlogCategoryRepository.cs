using KursPortal.DTOs.DTOs.BlogCategoryDtos;
using KursPortal.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KursPortal.DataAccess.Abstract
{
    public interface IBlogCategoryRepository : IRepository<BlogCategory>
    {
        Task<List<ResultBlogCategoryDto>> GetCategoriesWithBlogCountAsync();
    }
}
