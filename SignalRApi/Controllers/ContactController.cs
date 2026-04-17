using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SignalR.BusinessLayer.Abstract;
using SignalR.DtoLayer.ContactDto;
using SignalR.EntitytLayer.Entities;

namespace SignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _ContactService;
        private readonly IMapper _mapper;

        public ContactController(IMapper mapper, IContactService ContactService)
        {
            _mapper = mapper;
            _ContactService = ContactService;
        }

        [HttpGet]
        public IActionResult ContactList()
        {
            var value = _mapper.Map<List<ResultContactDto>>(_ContactService.TGetListAll());
            return Ok(value);
        }

        [HttpPost]
        public IActionResult CreateContact(CreateContactDto createContactDto)
        {
            var value = _mapper.Map<Contact>(createContactDto);
            _ContactService.TAdd(value);
            return Ok("İletişim bilgileri eklendi");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteContact(int id)
        {
            var value = _ContactService.TGetById(id);
            _ContactService.TDelete(value);
            return Ok("İletişim bilgileri silindi");
        }

        [HttpGet("{id}")]
        public IActionResult GetContact(int id)
        {
            var value = _ContactService.TGetById(id);
            return Ok(_mapper.Map<GetContactDto>(value));
        }

        [HttpPut]
        public IActionResult UpdateContact(UpdateContactDto updateContactDto)
        {
            var value = _mapper.Map<Contact>(updateContactDto);
            _ContactService.TUpdate(value);
            return Ok("İletişim bilgileri güncellendi");
        }
    }
}
