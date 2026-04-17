using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalR.BusinessLayer.Abstract;
using SignalR.DtoLayer.MessageDto;
using SignalR.EntityLayer.Entities;
using SignalR.EntitytLayer.Entities;

namespace SignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _MessageService;
        private readonly IMapper _mapper;

        public MessageController(IMessageService MessageService, IMapper mapper)
        {
            _MessageService = MessageService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult MessageList()
        {
            var values = _MessageService.TGetListAll();
            return Ok(_mapper.Map<List<ResultMessageDto>>(values));
        }

        [HttpPost]
        public IActionResult CreateMessage(CreateMessageDto createMessageDto)
        {
            createMessageDto.Status = false;
            createMessageDto.MessageSendDate = DateTime.Now;
            var value = _mapper.Map<Message>(createMessageDto);
            _MessageService.TAdd(value);
            return Ok("Mesaj başarıyla gönderildi");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMessage(int id)
        {
            var value = _MessageService.TGetById(id);
            _MessageService.TDelete(value);
            return Ok("Mesaj başarıyla silindi");
        }

        [HttpGet("{id}")]
        public IActionResult GetMessage(int id)
        {
            var value = _MessageService.TGetById(id);
            return Ok(value);
        }

        [HttpPut]
        public IActionResult UpdateMessage(UpdateMessageDto updateMessageDto)
        {
            var value = _mapper.Map<Message>(updateMessageDto);
            _MessageService.TUpdate(value);
            return Ok("Mesaj başarıyla güncellendi");
        }
    }
}
