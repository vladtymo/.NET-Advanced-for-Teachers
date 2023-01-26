using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLogic.Interfaces
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
