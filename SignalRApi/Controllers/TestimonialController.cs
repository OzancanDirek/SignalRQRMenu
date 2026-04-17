using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SignalR.BusinessLayer.Abstract;
using SignalR.DtoLayer.TestimonialDto;
using SignalR.EntitytLayer.Entities;

namespace SignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestimonialController : ControllerBase
    {
        private readonly ITestimonialService _TestimonialService;
        private readonly IMapper _mapper;

        public TestimonialController(IMapper mapper, ITestimonialService TestimonialService)
        {
            _mapper = mapper;
            _TestimonialService = TestimonialService;
        }

        [HttpGet]
        public IActionResult TestimonialList()
        {
            var value = _mapper.Map<List<ResultTestimonialDto>>(_TestimonialService.TGetListAll());
            return Ok(value);
        }

        [HttpPost]
        public IActionResult CreateTestimonial(CreateTestimonialDto createTestimonialDto)
        {
            var value = _mapper.Map<Testimonial>(createTestimonialDto);
            _TestimonialService.TAdd(value);
            return Ok("Müşteri Yorum Bilgisi eklendi");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTestimonial(int id)
        {
            var value = _TestimonialService.TGetById(id);
            _TestimonialService.TDelete(value);
            return Ok("Müşteri Yorum Bilgisi silindi");
        }

        [HttpGet("{id}")]
        public IActionResult GetTestimonial(int id)
        {
            var value = _TestimonialService.TGetById(id);
            return Ok(value);
        }

        [HttpPut]
        public IActionResult UpdateTestimonial(UpdateTestimonialDto updateTestimonialDto)
        {
            var value = _mapper.Map<Testimonial>(updateTestimonialDto);
            _TestimonialService.TUpdate(value);
            return Ok("Müşteri Yorum Bilgisi güncellendi");
        }
    }
}
