using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

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
            // Include() - LEFT JOIN
            var products = context.Products.Include(x => x.Category).ToList();

            // put them to the View
            return View(products); 
        }
        
        // GET: ~/Products/Create
        public IActionResult Create()
        {
            //ViewData["CategoryList"] = null;
            ViewBag.CategoryList = new SelectList(context.Categories.ToList(), nameof(Category.Id), nameof(Category.Name));
        
            return View();
        }

        // POST: ~/Products/Create
        [HttpPost]
        public IActionResult Create([FromForm] Product product)
        {
            // TODO: add validations

            // create product in db
            context.Products.Add(product);

            context.SaveChanges(); // submit changes in db

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            if (id < 0) return BadRequest();

            var product = context.Products.Find(id);

            if (product == null) return NotFound();

            context.Products.Remove(product);
            context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
