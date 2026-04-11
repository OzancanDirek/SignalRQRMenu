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

        public MessageController(IMessageService MessageService)
        {
            _MessageService = MessageService;
        }

        [HttpGet]
        public IActionResult MessageList()
        {
            var values = _MessageService.TGetListAll();
            return Ok(values);
        }

        [HttpPost]
        public IActionResult CreateMessage(CreateMessageDto createMessageDto)
        {
            Message message = new Message
            {
                Mail = createMessageDto.Mail,
                NameSurname = createMessageDto.NameSurname,
                Phone = createMessageDto.Phone,
                Subject = createMessageDto.Subject,
                MessageContent = createMessageDto.MessageContent,
                MessageSendDate = createMessageDto.MessageSendDate,
                Status = false
            };
            _MessageService.TAdd(message);
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
            _MessageService.TUpdate(new Message
            {
                Mail = updateMessageDto.Mail,
                NameSurname = updateMessageDto.NameSurname,
                Phone = updateMessageDto.Phone,
                Subject = updateMessageDto.Subject,
                MessageContent = updateMessageDto.MessageContent,
                MessageSendDate = updateMessageDto.MessageSendDate,
                Status = false,
                MessageID = updateMessageDto.MessageID
            });
            return Ok("Mesaj başarıyla güncellendi");
        }
    }
}
