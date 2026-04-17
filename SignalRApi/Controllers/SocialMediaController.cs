using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SignalR.BusinessLayer.Abstract;
using SignalR.DtoLayer.SocialMediaDto;
using SignalR.EntitytLayer.Entities;

namespace SignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SocialMediaController : ControllerBase
    {
        private readonly ISocialMediaService _SocialMediaService;
        private readonly IMapper _mapper;

        public SocialMediaController(IMapper mapper, ISocialMediaService SocialMediaService)
        {
            _mapper = mapper;
            _SocialMediaService = SocialMediaService;
        }

        [HttpGet]
        public IActionResult SocialMediaList()
        {
            var value = _mapper.Map<List<ResultSocialMediaDto>>(_SocialMediaService.TGetListAll());
            return Ok(value);
        }

        [HttpPost]
        public IActionResult CreateSocialMedia(CreateSocialMediaDto createSocialMediaDto)
        {
            var value = _mapper.Map<SocialMedia>(createSocialMediaDto);
            _SocialMediaService.TAdd(value);
            return Ok("Sosyal Medya Bilgileri eklendi");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteSocialMedia(int id)
        {
            var value = _SocialMediaService.TGetById(id);
            _SocialMediaService.TDelete(value);
            return Ok("Sosyal Medya Bilgileri silindi");
        }

        [HttpGet("{id}")]
        public IActionResult GetSocialMedia(int id)
        {
            var value = _SocialMediaService.TGetById(id);
            return Ok(value);
        }

        [HttpPut]
        public IActionResult UpdateSocialMedia(UpdateSocialMediaDto updateSocialMediaDto)
        {
            var value = _mapper.Map<SocialMedia>(updateSocialMediaDto);
            _SocialMediaService.TUpdate(value);
            return Ok("Sosyal Medya Bilgileri güncellendi");
        }
    }
}
