using System;
namespace GunShop.DTOs.Orders
{
    public class OrderItemDto
    {
        public int WeaponId { get; set; }
        public string WeaponName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }

}

