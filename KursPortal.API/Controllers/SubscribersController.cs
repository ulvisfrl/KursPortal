using AutoMapper;
using KursPortal.Business.Abstract;
using KursPortal.DTOs.DTOs.SubscriberDtos;
using KursPortal.Entity.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KursPortal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscribersController : ControllerBase
    {
        readonly ISubscriberService _subscriberService;
        readonly IMapper _mapper;
        readonly IMailService _mailService;

        public SubscribersController(ISubscriberService subscriberService, IMapper mapper, IMailService mailService)
        {
            _subscriberService = subscriberService;
            _mapper = mapper;
            _mailService = mailService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateSubscriberDto createSubscriberDto)
        {
            var subscriber = _mapper.Map<Subscriber>(createSubscriberDto);
            await _subscriberService.AddAsync(subscriber);
            return Ok();
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllSubscribers()
        {
            var subscribers = await _subscriberService.GetAllAsync();
            var result = _mapper.Map<List<ResultSubscriberDto>>(subscribers);
            return Ok(result);
        }

    }
}
