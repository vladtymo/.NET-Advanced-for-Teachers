using AspNetMvcApplication.Models;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AspNetMvcApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductsService productService;

        public HomeController(IProductsService productService)
        {
            this.productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            var products = await productService.GetAll();
            return View(products); // ~/Views/Home/Index.cshtml
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}