using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SignalR.BusinessLayer.Abstract;
using SignalR.DataAccessLayer.Concrete;
using SignalR.DtoLayer.ProductDto;
using SignalR.EntityLayer.Entities;

namespace SignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _ProductService;
        private readonly IMapper _mapper;

        public ProductController(IMapper mapper, IProductService ProductService)
        {
            _mapper = mapper;
            _ProductService = ProductService;
        }

        [HttpGet]
        public IActionResult ProductList()
        {
            var value = _mapper.Map<List<ResultProductDto>>(_ProductService.TGetListAll());
            return Ok(value);
        }

        [HttpPost]
        public IActionResult CreateProduct(CreateProductDto createProductDto)
        {
            var values = _mapper.Map<Product>(createProductDto);
            _ProductService.TAdd(values);
            return Ok("Ürün Bilgisi eklendi");
        }

        [HttpDelete]
        public IActionResult DeleteProduct(int id)
        {
            var value = _ProductService.TGetById(id);
            _ProductService.TDelete(value);
            return Ok("Ürün Bilgisi silindi");
        }

        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            var value = _ProductService.TGetById(id);
            return Ok(_mapper.Map<GetProductDto>(value));
        }

        [HttpPut]
        public IActionResult UpdateProduct(UpdateProductDto updateProductDto)
        {
            var values = _mapper.Map<Product>(updateProductDto);
            _ProductService.TAdd(values);
            return Ok("Ürün Bilgisi güncellendi");
        }

        [HttpGet("GetProductsWithCategories")]
        public IActionResult GetProductsWithCategories()
        {
            var context = new SignalRContext();
            var values = context.Products.Include(x => x.Category).Select(y => new ResultProductWithCategory
            {
                Description = y.Description,
                Price = y.Price,
                ImageUrl = y.ImageUrl,
                ProductStatus = y.ProductStatus,
                ProductName = y.ProductName,
                ProductId = y.ProductId,
                CategoryName = y.Category.CategoryName
            });
            return Ok(values.ToList());
        }

        [HttpGet("ProductCount")]
        public IActionResult GetProducts()
        {
            return Ok(_ProductService.TProductCount());
        }

        [HttpGet("ProductCountByNameHamburger")]
        public IActionResult ProductCountByNameHamburger()
        {
            return Ok(_ProductService.TProductCountByCategoryNameHamburger());
        }

        [HttpGet("ProductCountByNameDrink")]
        public IActionResult ProductCountByNameDrink()
        {
            return Ok(_ProductService.TProductCountByCategoryDrink());
        }

        [HttpGet("ProductPriceAvg")]
        public IActionResult ProductPriceAvg()
        {
            return Ok(_ProductService.TProductPriceAvg());
        }

        [HttpGet("TProductNameByMinPrice")]
        public IActionResult TProductNameByMinPrice()
        {
            return Ok(_ProductService.TProductNameByMinPrice());
        }

        [HttpGet("ProductNameByMaxPrice")]
        public IActionResult ProductNameByMaxPrice()
        {
            return Ok(_ProductService.TProductNameByMaxPrice());
        }

        [HttpGet("ProductAvgPriceByHamburger")]
        public IActionResult ProductAvgPriceByHamburger()
        {
            return Ok(_ProductService.TProductAvgPriceByHamburger());
        }

        [HttpGet("ProductPriceBySteakBurger")]
        public IActionResult ProductPriceBySteakBurger()
        {
            return Ok(_ProductService.TProductPriceBySteakBurger());
        }

        [HttpGet("TotalPriceByDrinkCategory")]
        public IActionResult TotalPriceByDrinkCategory()
        {
            return Ok(_ProductService.TTotalPriceByDrinkCategory());
        }

        [HttpGet("TotalPriceBySaladCategory")]
        public IActionResult TotalPriceBySaladCategory()
        {
            return Ok(_ProductService.TTotalPriceBySaladCategory());
        }
    }
}