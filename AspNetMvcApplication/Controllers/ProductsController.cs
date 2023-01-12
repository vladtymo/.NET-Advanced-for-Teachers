using DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace AspNetMvcApplication.Controllers
{
    public class ProductsController : Controller
    {
        private readonly MyAppDbContext context;

        public ProductsController(MyAppDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            // get products from DB
            var products = context.Products.ToList();

            // put them to the View
            return View(products); 
        }
    }
}
