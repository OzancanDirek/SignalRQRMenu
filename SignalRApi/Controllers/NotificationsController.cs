using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalR.BusinessLayer.Abstract;
using SignalR.DtoLayer.NotificationDto;
using SignalR.EntitytLayer.Entities;

namespace SignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationsController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet]
        public IActionResult NotificationList()
        {
            return Ok(_notificationService.TGetListAll());
        }

        [HttpGet("NotificationCountByStatusFalse")]
        public IActionResult NotificationCountByStatusFalse()
        {
            return Ok(_notificationService.TNotificationCountByStatusFalse());
        }

        [HttpGet("GetAllNotificationByFalse")]
        public IActionResult GetAllNotificationByFalse()
        {
            return Ok(_notificationService.TGetAllNotificationByFalse());
        }


        [HttpPost]
        public IActionResult CreateNotification(CreateNotificationDto createNotificationDto)
        {
            Notification notification = new Notification
            {
                Type = createNotificationDto.Type,
                Icon = createNotificationDto.Icon,
                Description = createNotificationDto.Description,
                Date = Convert.ToDateTime(DateTime.Now.ToShortDateString()),
                Status = false
            };
            _notificationService.TAdd(notification);
            return Ok("Ekleme işlemi başarıyla yapıldı");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteNotification(int id)
        {
            var values = _notificationService.TGetById(id);
            _notificationService.TDelete(values);
            return Ok("Silme işlemi başarıyla yapıldı");
        }

        [HttpGet("{id}")]
        public IActionResult GetNotificationById(int id)
        {
            var values = _notificationService.TGetById(id);
            return Ok(values);
        }

        [HttpPut]
        public IActionResult UpdateNotification(UpdateNotificationDto updateNotificationDto)
        {
           Notification notification = new Notification
            {
                NotificationId = updateNotificationDto.NotificationId,
                Type = updateNotificationDto.Type,
                Icon = updateNotificationDto.Icon,
                Description = updateNotificationDto.Description,
                Date = updateNotificationDto.Date,
               Status = updateNotificationDto.Status
            };
            _notificationService.TUpdate(notification);
            return Ok("Güncelleme işlemi başarıyla yapıldı");
        }

        [HttpGet("NotificationStatusChangeToFalse/{id}")]
        public IActionResult NotificationStatusChangeToFalse(int id)
        {
            _notificationService.TNotificationStatusChangeToFalse(id);
            return Ok("Bildirim durumu false olarak değiştirildi");
        }

        [HttpGet("NotificationStatusChangeToTrue/{id}")]
        public IActionResult NotificationStatusChangeToTrue(int id)
        {
            _notificationService.TNotificationStatusChangeToTrue(id);
            return Ok("Bildirim durumu true olarak değiştirildi");
        }
    }
}
