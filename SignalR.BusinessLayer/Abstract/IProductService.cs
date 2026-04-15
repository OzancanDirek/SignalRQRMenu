using SignalR.EntityLayer.Entities;

namespace SignalR.BusinessLayer.Abstract
{
    public interface IProductService : IGenericService<Product>
    {
        List<Product> TGetProductsWithCategories();

        public int TProductCount();

        public int TProductCountByCategoryNameHamburger();
        public int TProductCountByCategoryDrink();
        public decimal TProductPriceAvg();
        public string TProductNameByMaxPrice();
        public string TProductNameByMinPrice();
        public decimal TProductAvgPriceByHamburger();
        public decimal TProductPriceBySteakBurger();
        public decimal TTotalPriceByDrinkCategory();
        public decimal TTotalPriceBySaladCategory();
    }
}
