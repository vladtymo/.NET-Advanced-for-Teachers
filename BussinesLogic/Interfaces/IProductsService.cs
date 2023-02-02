using Core.DTOs;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IProductsService
    {
        List<ProductDto> GetAll();
        List<CategoryDto> GetAllCategories();
        ProductDto? Get(int id);
        void Create(ProductDto product);
        void Update(ProductDto product);
        void Delete(int id);
    }
}
