using AutoMapper;
using KursPortal.Business.Abstract;
using KursPortal.DTOs.DTOs.FaqDtos;
using KursPortal.Entity.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KursPortal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FaqsController : ControllerBase
    {
        readonly IFaqService _faqService;
        readonly IMapper _mapper;

        public FaqsController(IFaqService faqService, IMapper mapper)
        {
            _faqService = faqService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateFaq(CreateFaqDto dto)
        {
            var faq = _mapper.Map<Faq>(dto);
            await _faqService.AddAsync(faq);
            return Ok();
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllFaqs()
        {
            var faqs = await _faqService.GetAllAsync();
            var result = _mapper.Map<List<ResultFaqDto>>(faqs);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFaq(Guid id)
        {
            var faq = await _faqService.GetByIdAsync(id);
            if (faq == null)
                return NotFound("tapilmadi.");
            await _faqService.DeleteAsync(faq);
            return Ok("silindi.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFaq(Guid id, UpdateFaqDto dto)
        {
            var faq = await _faqService.GetByIdAsync(id);
            if (faq == null)
                return NotFound("tapilmadi.");
            _mapper.Map(dto, faq);
            await _faqService.UpdateAsync(faq);
            return Ok("guncellendi");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFaq(Guid id)
        {
            var faq = await _faqService.GetByIdAsync(id);
            if (faq == null)
                return NotFound("tapilmadi.");
            var result = _mapper.Map<ResultFaqDto>(faq);
            return Ok(result);
        }
    }
}
