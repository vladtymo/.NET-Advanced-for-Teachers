using DataAccess.Interfaces;

namespace DataAccess
{
    public enum CategoryTypes : int
    {
        Electronics = 1, Sport, FashionAndArt, HomAndGarder
    }

    public class Category : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // ------------ Navigation Property
        public ICollection<Product> Products { get; set; } = new HashSet<Product>();
    }
}