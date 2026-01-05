using System;
using GunShop.DTOs.Orders;

namespace GunShop.Services.Interfaces
{
	public interface IOrderService
	{
        Task<List<OrderDto>> GetAllOrders();
        Task<OrderDto> GetOrderById(int id);
        Task<OrderDto> CreateOrder(OrderCreateDto dto);
    }
}

