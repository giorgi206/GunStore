using System;
namespace GunShop.DTOs.Orders
{
    public class OrderCreateDto
    {
        public int UserId { get; set; }
        public List<OrderItemCreateDto> Items { get; set; }
    }

}

