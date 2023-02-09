using Ardalis.Specification;
using Core.Entities;

namespace Core.Specifications
{
    public static class Products
    {
        // all product specifications
        public class All : Specification<Product>
        {
            public All()
            {
                Query.Include(x => x.Category);
            }
        }

        public class ByPrice : Specification<Product>
        {
            public ByPrice(decimal from, decimal to)
            {
                Query
                    .Where(x => x.Price >= from && x.Price <= to)
                    .Include(x => x.Category);
            }
        }
    }
}
