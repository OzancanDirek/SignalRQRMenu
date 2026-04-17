using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SignalR.BusinessLayer.Abstract;
using SignalR.DtoLayer.MenuTableDto;
using SignalR.EntitytLayer.Entities;

namespace SignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuTablesController : ControllerBase
    {
        private readonly IMenuTableService _menuTableService;
        private readonly IMapper _mapper;

        public MenuTablesController(IMenuTableService menuTableService, IMapper mapper)
        {
            _menuTableService = menuTableService;
            _mapper = mapper;
        }

        [HttpGet("TableCount")]
        public IActionResult TableCount()
        {
            return Ok(_menuTableService.TTableCount());
        }

        [HttpGet]
        public IActionResult MenuTableList()
        {
            var values = _menuTableService.TGetListAll();
            return Ok(_mapper.Map<List<ResultMenuTableDto>>(values));
        }

        [HttpPost]
        public IActionResult CreateMenuTable(CreateMenuTableDto createMenuTableDto)
        {
            createMenuTableDto.Status = false;
            var value = _mapper.Map<MenuTable>(createMenuTableDto);
            _menuTableService.TAdd(value);
            return Ok("Masa başarıyla oluşturuldu");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMenuTable(int id)
        {
            var value = _menuTableService.TGetById(id);
            _menuTableService.TDelete(value);
            return Ok("Masa başarıyla silindi");
        }

        [HttpGet("{id}")]
        public IActionResult GetMenuTable(int id)
        {
            var value = _menuTableService.TGetById(id);
            return Ok(value);
        }

        [HttpPut]
        public IActionResult UpdateMenuTable(UpdateMenuTableDto updateMenuTableDto)
        {
            var value = _mapper.Map<MenuTable>(updateMenuTableDto);
            _menuTableService.TUpdate(value);
            return Ok("Masa bilgileri başarıyla güncellendi");
        }
    }
}
