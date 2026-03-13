using Microsoft.AspNetCore.Mvc;
using SignalR.BusinessLayer.Abstract;
using SignalR.DtoLayer.AboutDto;
using SignalR.EntityLayer.Entities;

[Route("api/[controller]")]
[ApiController]
public class AboutController : ControllerBase
{
    private readonly IAboutService _aboutService;

    public AboutController(IAboutService aboutService)
    {
        _aboutService = aboutService;
    }

    [HttpGet]
    public IActionResult AboutList()
    {
        var values = _aboutService.TGetListAll();
        return Ok(values);
    }

    [HttpPost]
    public IActionResult CreateAbout(CreateAboutDto createAboutDto)
    {
        _aboutService.TAdd(new About
        {
            ImageUrl = createAboutDto.ImageUrl,
            Title = createAboutDto.Title,
            Description = createAboutDto.Description
        });
        return Ok("Hakkında kısmı başarıyla oluşturuldu");
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteAbout(int id)
    {
        var value = _aboutService.TGetById(id);
        _aboutService.TDelete(value);
        return Ok("Hakkında kısmı başarıyla silindi");
    }

    [HttpGet("{id}")]
    public IActionResult GetAbout(int id)
    {
        var value = _aboutService.TGetById(id);
        return Ok(value);
    }

    [HttpPut]
    public IActionResult UpdateAbout(UpdateAboutDto updateAboutDto)
    {
        _aboutService.TUpdate(new About
        {
            AboutId = updateAboutDto.AboutId,
            ImageUrl = updateAboutDto.ImageUrl,
            Title = updateAboutDto.Title,
            Description = updateAboutDto.Description
        });
        return Ok("Hakkında kısmı başarıyla güncellendi");
    }
}