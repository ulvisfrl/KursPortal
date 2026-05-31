using KursPortal.Business.Abstract;
using KursPortal.DataAccess.Abstract;
using KursPortal.DTOs.DTOs.BlogCategoryDtos;
using KursPortal.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KursPortal.Business.Concrete
{
    public class BlogCategoryService : GenericService<BlogCategory>, IBlogCategoryService
    {
        readonly IBlogCategoryRepository _blogCategoryRepository;
        public BlogCategoryService(IRepository<BlogCategory> repository, IBlogCategoryRepository blogCategoryRepository) : base(repository)
        {
            _blogCategoryRepository = blogCategoryRepository;
        }

        public async Task<List<ResultBlogCategoryDto>> GetCategoriesWithBlogCountAsync()
        {
            return await _blogCategoryRepository.GetCategoriesWithBlogCountAsync();
        }
    }
}
