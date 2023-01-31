using Core.Entities;

namespace Core.Interfaces
{
    public interface IProductsService
    {
        List<Product> GetAll();
        List<Category> GetAllCategories();
        Product? Get(int id);
        void Create(Product product);
        void Update(Product product);
        void Delete(int id);
    }
}
