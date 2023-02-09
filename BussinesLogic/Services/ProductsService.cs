using Core.DTOs;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using System.Runtime.CompilerServices;

namespace Core.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IMapper mapper;

        private readonly IRepository<Product> productRepo;
        private readonly IRepository<Category> categoryRepo;

        public ProductsService(IRepository<Product> productRepo,
                               IRepository<Category> categoryRepo,
                               IMapper mapper)
        {
            this.productRepo = productRepo;
            this.categoryRepo = categoryRepo;
            this.mapper = mapper;
        }

        public async Task<List<ProductDto>> GetAll()
        {
            // get products from DB
            // Include() - LEFT JOIN
            var result = await productRepo.Get(includeProperties: new[] { nameof(Product.Category) });
            return mapper.Map<List<ProductDto>>(result);

            //foreach (var i in result)
            //{
            //    yield return new ProductDto()
            //    {
            //        Id = i.Id,
            //        Name = i.Name,
            //        Price = i.Price,
            //        CategoryId = i.CategoryId,
            //        CategoryName = i.Category?.Name ?? "not loaded"
            //    };
            //}
        }

        public async Task<ProductDto?> Get(int id)
        {
            if (id < 0) return null; // exception handling

            var product = await productRepo.GetByID(id);

            if (product == null) return null; // exception handling

            return mapper.Map<ProductDto>(product);
            //return new ProductDto()
            //{
            //    Id = product.Id,
            //    Name = product.Name,
            //    Price = product.Price,
            //    CategoryId = product.CategoryId,
            //    CategoryName = product.Category?.Name ?? "not loaded" 
            //};
        }

        public async Task Create(ProductDto dto)
        {
            // create product in db
            await productRepo.Insert(mapper.Map<Product>(dto));
            await productRepo.Save(); // submit changes in db
        }

        public async Task Update(ProductDto dto)
        {
            await productRepo.Update(mapper.Map<Product>(dto));
            await productRepo.Save();
        }

        public async Task Delete(int id)
        {
            var product = await Get(id);

            if (product == null) return; // exception

            await productRepo.Delete(id);
            await productRepo.Save();
        }

        public async Task<List<CategoryDto>> GetAllCategories()
        {
            return mapper.Map<List<CategoryDto>>(await categoryRepo.Get());
        }
    }
}
