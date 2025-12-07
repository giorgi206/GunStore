using System;
namespace GunShop.Models
{
	public class OrderItem
	{
        public int Id { get; set; }

        // FK → Order
        public int OrderId { get; set; }
        public Order Order { get; set; }

        // FK → Weapon
        public int WeaponId { get; set; }
        public Weapon Weapon { get; set; }

        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}

