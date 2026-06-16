using KursPortal.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KursPortal.Business.Abstract
{
    public interface IBlogService : IGenericService<Blog>
    {
        Task<List<Blog>> GetBlogWithDetailsAsync();
        Task<Blog> GetBlogWithDetailsAsync(Guid id);
        Task<IEnumerable<Blog>> GetPagedBlogsAsync(Guid? categoryId, int page, int pageSize);
        Task<int> GetBlogCountAsync(Guid? categoryId);
    }
}
