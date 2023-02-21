using AspNetMvcApplication.Helpers;
using Core.DTOs;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AspNetMvcApplication.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductsService productsService;

        public CartController(IProductsService productsService)
        {
            this.productsService = productsService;
        }

        public async Task<IActionResult> Index()
        {
            var productIds = HttpContext.Session.Get<List<int>>("cart-list");

            List<ProductDto> products = new();
            if (productIds != null)
                products = await productsService.Get(productIds.ToArray());

            return View(products);
        }

        public IActionResult Add(int productId)
        {
            var productIds = HttpContext.Session.Get<List<int>>("cart-list");

            productIds ??= new List<int>();

            productIds.Add(productId);
            HttpContext.Session.Set("cart-list", productIds);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Remove(int productId)
        {
            var productIds = HttpContext.Session.Get<List<int>>("cart-list");

            if (productIds != null)
            {
                productIds.Remove(productId);
                HttpContext.Session.Set("cart-list", productIds);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
