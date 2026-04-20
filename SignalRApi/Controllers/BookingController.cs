using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SignalR.BusinessLayer.Abstract;
using SignalR.DtoLayer.BookingDto;
using SignalR.EntityLayer.Entities;

[Route("api/[controller]")]
[ApiController]
public class BookingController : ControllerBase
{
    private readonly IBookingService _bookingService;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateBookingDto> _createValidator;

    public BookingController(IBookingService bookingService, IMapper mapper, IValidator<CreateBookingDto> createValidator)
    {
        _bookingService = bookingService;
        _mapper = mapper;
        _createValidator = createValidator;
    }

    [HttpGet]
    public IActionResult BookingList()
    {
        var values = _bookingService.TGetListAll();
        return Ok(_mapper.Map<List<ResultBookingDto>>(values));
    }

    [HttpPost]
    public IActionResult CreateBooking(CreateBookingDto createBookingDto)
    {
        var validationResult = _createValidator.Validate(createBookingDto);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }
        var value = _mapper.Map<Booking>(createBookingDto);
        _bookingService.TAdd(value);
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
        return Ok(_mapper.Map<GetBookingDto>(value));
    }

    [HttpPut]
    public IActionResult UpdateBooking(UpdateBookingDto updateBookingDto)
    {
        var value = _mapper.Map<Booking>(updateBookingDto);
        _bookingService.TUpdate(value);
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