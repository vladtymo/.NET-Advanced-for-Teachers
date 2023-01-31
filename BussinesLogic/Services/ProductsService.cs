using Core.Entities;
using Core.Interfaces;

namespace Core.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IRepository<Product> productRepo;
        private readonly IRepository<Category> categoryRepo;

        public ProductsService(IRepository<Product> productRepo,
                               IRepository<Category> categoryRepo)
        {
            this.productRepo = productRepo;
            this.categoryRepo = categoryRepo;
        }

        public List<Product> GetAll()
        {
            // get products from DB
            // Include() - LEFT JOIN
            return productRepo.Get(includeProperties: "Category").ToList();
        }

        public Product? Get(int id)
        {
            if (id < 0) return null; // exception handling

            var product = productRepo.GetByID(id);

            if (product == null) return null; // exception handling

            return product;
        }

        public void Create(Product product)
        {
            // create product in db
            productRepo.Insert(product);
            productRepo.Save(); // submit changes in db
        }

        public void Update(Product product)
        {
            productRepo.Update(product);
            productRepo.Save();
        }

        public void Delete(int id)
        {
            var product = Get(id);

            if (product == null) return; // exception

            productRepo.Delete(id);
            productRepo.Save();
        }

        public List<Category> GetAllCategories()
        {
            return categoryRepo.Get().ToList();
        }
    }
}
