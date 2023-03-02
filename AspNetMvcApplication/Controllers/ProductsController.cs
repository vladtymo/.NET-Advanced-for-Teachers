using AspNetMvcApplication.Helpers;
using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AspNetMvcApplication.Controllers
{
    //[Authorize] // can access by authorized user only
    [Authorize(Roles = "Admin")]
    public class ProductsController : Controller
    {
        private readonly IProductsService productsService;

        public ProductsController(IProductsService productsService)
        {
            this.productsService = productsService;
        }

        private async Task LoadCategories()
        {
            //ViewData["CategoryList"] = null;
            ViewBag.CategoryList = new SelectList(await productsService.GetAllCategories(), nameof(CategoryDto.Id), nameof(CategoryDto.Name));
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            // put them to the View
            return View(await productsService.GetAll());
        }
        
        // GET: ~/Products/Create
        public async Task<IActionResult> Create()
        {
            await LoadCategories();
            return View();
        }

        // POST: ~/Products/Create
        [HttpPost]
        public async Task<IActionResult> Create(ProductDto product)
        {
            // TODO: add validations

            await productsService.Create(product);

            return RedirectToAction(nameof(Index));
        }

        // GET: ~/Products/Edit/{id}
        public async Task<IActionResult> Edit(int id)
        {
            var product = await productsService.Get(id);

            if (product == null) return NotFound();

            await LoadCategories();
            return View(product);
        }

        // POST: ~/Products/Edit
        [HttpPost]
        public async Task<IActionResult> Edit(ProductDto product) // 1-FromForm, 2-FromRoute, 
        {
            // TODO: add validations

            await productsService.Update(product);

            return RedirectToAction(nameof(Index));
        }

        // GET: ~/Products/Delete/{id}
        public async Task<IActionResult> Delete(int id)
        {
            await productsService.Delete(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
