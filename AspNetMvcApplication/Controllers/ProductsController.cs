using BussinesLogic.Interfaces;
using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AspNetMvcApplication.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductsService productsService;

        public ProductsController(IProductsService productsService)
        {
            this.productsService = productsService;
        }

        private void LoadCategories()
        {
            //ViewData["CategoryList"] = null;
            ViewBag.CategoryList = new SelectList(productsService.GetAllCategories(), nameof(Category.Id), nameof(Category.Name));
        }

        public IActionResult Index()
        {
            // put them to the View
            return View(productsService.GetAll());
        }
        
        // GET: ~/Products/Create
        public IActionResult Create()
        {
            LoadCategories();
            return View();
        }

        // POST: ~/Products/Create
        [HttpPost]
        public IActionResult Create(Product product)
        {
            // TODO: add validations

            productsService.Create(product);

            return RedirectToAction(nameof(Index));
        }

        // GET: ~/Products/Edit/{id}
        public IActionResult Edit(int id)
        {
            var product = productsService.Get(id);

            if (product == null) return NotFound();

            LoadCategories();
            return View(product);
        }

        // POST: ~/Products/Edit
        [HttpPost]
        public IActionResult Edit(Product product) // 1-FromForm, 2-FromRoute, 
        {
            // TODO: add validations

            productsService.Update(product);

            return RedirectToAction(nameof(Index));
        }

        // GET: ~/Products/Delete/{id}
        public IActionResult Delete(int id)
        {
            productsService.Delete(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
