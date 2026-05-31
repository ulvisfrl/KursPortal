using KursPortal.Business.Abstract;
using KursPortal.DataAccess.Abstract;
using KursPortal.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KursPortal.Business.Concrete
{
    public class BlogService : GenericService<Blog>, IBlogService
    {
        readonly IBlogRepository _blogRepository;
        public BlogService(IRepository<Blog> repository, IBlogRepository blogRepository) : base(repository)
        {
            _blogRepository = blogRepository;
        }

        public async Task<List<Blog>> GetBlogWithDetailsAsync()
        {
            return await _blogRepository.GetBlogWithDetailsAsync();
        }

        public async Task<Blog> GetBlogWithDetailsAsync(Guid id)
        {
            return await _blogRepository.GetBlogWithDetailsAsync(id);
        }
    }
}
