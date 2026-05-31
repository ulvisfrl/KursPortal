using AutoMapper;
using FluentValidation;
using KursPortal.Business.Abstract;
using KursPortal.DTOs.DTOs.ContactDtos;
using KursPortal.Entity.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KursPortal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        readonly IContactService _contactService;
        readonly IMailService _mailService;
        readonly IMapper _mapper;
        public ContactsController(IContactService contactService, IMapper mapper, IMailService mailService)
        {
            _contactService = contactService;
            _mapper = mapper;
            _mailService = mailService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateContactDto createContactDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var contact = _mapper.Map<Contact>(createContactDto);
            await _contactService.AddAsync(contact);

            string body = $@"
            Yeni contact mesajı:

             Ad: {createContactDto.FullName}
             Email: {createContactDto.Email}
             Mövzu: {createContactDto.Title}

             Mesaj:
                {createContactDto.Subject}";

            await _mailService.SendMailAsync(
        "ulviseferli2021@gmail.com",
        "Yeni Contact Mesajı",
        body,
        true
    );
            return Ok();
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllContacts()
        {
            var contacts = await _contactService.GetAllAsync();
            var result = _mapper.Map<List<ResultContactDto>>(contacts);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetContact(Guid id)
        {
            var contact = await _contactService.GetByIdAsync(id);
            if (contact == null)
                return NotFound("message tapilmadi");
            var result = _mapper.Map<ResultContactDto>(contact);
            return Ok(result);
        }


        [HttpPost("response")]
        public async Task<IActionResult> Response(Response dto)
        {
            await _mailService.SendMailAsync(dto.Email, dto.Title, dto.Subject, true);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(Guid id)
        {
            var contact = await _contactService.GetByIdAsync(id);
            if (contact == null)
                return NotFound("Contact tapilmadi.");
            await _contactService.DeleteAsync(contact);
            return Ok("Contact silindi.");
        }
    }
}
