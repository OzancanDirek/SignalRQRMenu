using Microsoft.AspNetCore.Mvc;
using SignalR.BusinessLayer.Abstract;
using SignalR.DtoLayer.BookingDto;
using SignalR.EntityLayer.Entities;

[Route("api/[controller]")]
[ApiController]
public class BookingController : ControllerBase
{
    private readonly IBookingService _bookingService;

    public BookingController(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    [HttpGet]
    public IActionResult BookingList()
    {
        var values = _bookingService.TGetListAll();
        return Ok(values);
    }

    [HttpPost]
    public IActionResult CreateBooking(CreateBookingDto createBookingDto)
    {
        Booking booking = new Booking()
        {
            Name = createBookingDto.Name,
            Phone = createBookingDto.Phone,
            Mail = createBookingDto.Mail,
            PersonCount = createBookingDto.PersonCount,
            Date = createBookingDto.Date,
            Description = createBookingDto.Description
        };
        _bookingService.TAdd(booking);
        return Ok("Rezervasyon Yapıldı");
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteBooking(int id)
    {
        var value = _bookingService.TGetById(id);
        _bookingService.TDelete(value);
        return Ok("Rezervasyon Silindi");
    }

    [HttpGet("{id}")]
    public IActionResult GetBooking(int id)
    {
        var value = _bookingService.TGetById(id);
        return Ok(value);
    }

    [HttpPut]
    public IActionResult UpdateBooking(UpdateBookingDto updateBookingDto)
    {
        Booking booking = new Booking()
        {
            BookingId = updateBookingDto.BookingId,
            Name = updateBookingDto.Name,
            Phone = updateBookingDto.Phone,
            Mail = updateBookingDto.Mail,
            PersonCount = updateBookingDto.PersonCount,
            Date = updateBookingDto.Date,
            Description = updateBookingDto.Description
        };
        _bookingService.TUpdate(booking);
        return Ok("Rezervasyon Güncellendi");
    }

    [HttpGet("BookingStatusApproved/{id}")]
    public IActionResult BookingStatusApproved(int id)
    {
        _bookingService.TBookingStatusApproved(id);
        return Ok("Rezervasyon Onaylandı");
    }

    [HttpGet("BookingStatusCanceled/{id}")]
    public IActionResult BookingStatusCanceled(int id)
    {
        _bookingService.TBookingStatusCanceled(id);
        return Ok("Rezervasyon İptal Edildi");
    }
     
}