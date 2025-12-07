using System;
namespace GunShop.Models
{
	public class Weapon
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public string Caliber { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string ImageUrl { get; set; }

        // FK → Category
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        // Relation: Weapon → OrderItems
        public List<OrderItem> OrderItems { get; set; } = new();
    }
}

