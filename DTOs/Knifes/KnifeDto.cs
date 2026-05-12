using System;
namespace GunShop.DTOs.Knifes
{
	public class KnifeDto
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public string BladeMaterial { get; set; }
        public decimal BladeLength { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string ImageUrl { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}

