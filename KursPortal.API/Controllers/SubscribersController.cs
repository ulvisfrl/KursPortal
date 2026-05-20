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

        public SubscribersController(ISubscriberService subscriberService, IMapper mapper)
        {
            _subscriberService = subscriberService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateSubscriberDto createSubscriberDto)
        {
            var subscriber = _mapper.Map<Subscriber>(createSubscriberDto);
            await _subscriberService.AddAsync(subscriber);
            return Ok();
        }
    }
}
