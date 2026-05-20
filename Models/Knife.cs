using System.Collections.Generic;

namespace GunShop.Models
{
    public class Knife
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string BladeType { get; set; } // დამატებულია
        public string BladeMaterial { get; set; }
        public decimal BladeLength { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string ImageUrl { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public List<OrderItem> OrderItems { get; set; } = new();
    }
}