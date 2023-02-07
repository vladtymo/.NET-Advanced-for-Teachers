using Core.DTOs;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;

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

        public List<ProductDto> GetAll()
        {
            // get products from DB
            // Include() - LEFT JOIN
            var result = productRepo.Get(includeProperties: new[] { nameof(Product.Category) }).ToList();
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

        public ProductDto? Get(int id)
        {
            if (id < 0) return null; // exception handling

            var product = productRepo.GetByID(id);

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

        public void Create(ProductDto dto)
        {
            // create product in db
            productRepo.Insert(mapper.Map<Product>(dto));
            productRepo.Save(); // submit changes in db
        }

        public void Update(ProductDto dto)
        {
            productRepo.Update(mapper.Map<Product>(dto));
            productRepo.Save();
        }

        public void Delete(int id)
        {
            var product = Get(id);

            if (product == null) return; // exception

            productRepo.Delete(id);
            productRepo.Save();
        }

        public List<CategoryDto> GetAllCategories()
        {
            return mapper.Map<List<CategoryDto>>(categoryRepo.Get().ToList());
        }
    }
}
