using System;
namespace GunShop.Models
{
	public class Category
	{
        public int Id { get; set; }
        public string Name { get; set; }

        // Relation: One Category → Many Weapons
        public List<Weapon> Weapons { get; set; } = new List<Weapon>();

        public Category() { }
    }
}

