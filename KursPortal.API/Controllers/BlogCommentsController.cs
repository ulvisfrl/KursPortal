using AutoMapper;
using KursPortal.Business.Abstract;
using KursPortal.DTOs.DTOs.BlogCommentDtos;
using KursPortal.Entity.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KursPortal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogCommentsController : ControllerBase
    {
        readonly IBlogCommentService _blogCommentService;
        readonly IMapper _mapper;

        public BlogCommentsController(IBlogCommentService blogCommentService, IMapper mapper)
        {
            _blogCommentService = blogCommentService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBlogComment(CreateBlogCommentDto dto)
        {
            var blogComment = _mapper.Map<BlogComment>(dto);
            await _blogCommentService.AddAsync(blogComment);
            return Ok();
        }
    }
}
