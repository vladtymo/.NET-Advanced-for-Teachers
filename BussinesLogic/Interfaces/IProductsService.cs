using Core.DTOs;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IProductsService
    {
        Task<List<ProductDto>> GetAll();
        Task<List<CategoryDto>> GetAllCategories();
        Task<ProductDto?> Get(int id);
        Task<List<ProductDto>> Get(params int[] ids);
        Task Create(ProductDto product);
        Task Update(ProductDto product);
        Task Delete(int id);
        Task<List<ProductDto>> GetByPrice(decimal from, decimal to);
    }
}
