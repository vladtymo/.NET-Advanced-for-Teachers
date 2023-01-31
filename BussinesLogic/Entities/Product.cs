using Core.Interfaces;

namespace Core.Entities
{
    public class Product : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }

        // ------------ Navigation Property
        public Category Category { get; set; }
    }
}