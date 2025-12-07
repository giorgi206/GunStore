using System;
namespace GunShop.DTOs.Orders
{
    public class OrderDto
    {
        public int Id { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UserFullName { get; set; }
        public List<OrderItemDto> Items { get; set; }
    }

}

