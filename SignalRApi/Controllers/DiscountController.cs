using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalR.BusinessLayer.Abstract;
using SignalR.DtoLayer.DiscountDto;
using SignalR.EntityLayer.Entities;

namespace SignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountService _DiscountService;
        private readonly IMapper _mapper;

        public DiscountController(IMapper mapper, IDiscountService DiscountService)
        {
            _mapper = mapper;
            _DiscountService = DiscountService;
        }

        [HttpGet]
        public IActionResult DiscountList()
        {
            var value = _mapper.Map<List<ResultDiscountDto>>(_DiscountService.TGetListAll());
            return Ok(value);
        }

        [HttpPost]
        public IActionResult CreateDiscount(CreateDiscountDto createDiscountDto)
        {
            var value = _mapper.Map<Discount>(createDiscountDto);
            _DiscountService.TAdd(value);
            return Ok("İndirim Bilgisi eklendi");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteDiscount(int id)
        {
            var value = _DiscountService.TGetById(id);
            _DiscountService.TDelete(value);
            return Ok("İndirim Bilgisi silindi");
        }

        [HttpGet("{id}")]
        public IActionResult GetDiscount(int id)
        {
            var value = _DiscountService.TGetById(id);
            return Ok(_mapper.Map<GetDiscountDto>(value));
        }

        [HttpPut]
        public IActionResult UpdateDiscount(UpdateDiscountDto updateDiscountDto)
        {
            var value = _mapper.Map<Discount>(updateDiscountDto);
            _DiscountService.TUpdate(value);
            return Ok("İndirim Bilgisi güncellendi");
        }

        [HttpGet("ChangeStatusToTrue/{id}")]
        public IActionResult ChangeStatusToTrue(int id)
        {
            _DiscountService.TChangeStatusToTrue(id);
            return Ok("İndirim Bilgisi aktif edildi");
        }

        [HttpGet("ChangeStatusToFalse/{id}")]
        public IActionResult ChangeStatusToFalse(int id)
        {
            _DiscountService.TChangeStatusToFalse(id);
            return Ok("İndirim Bilgisi pasif edildi");
        }

        [HttpGet("GetActiveDiscounts")]
        public IActionResult GetActiveDiscounts()
        {
            return Ok(_DiscountService.TGetActiveDiscounts());
        }
    }
}
