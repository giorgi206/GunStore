using System;
using GunShop.Data;
using GunShop.DTOs.Orders;
using GunShop.Models;
using GunShop.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GunShop.Services
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _dbContext;

        public OrderService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<OrderDto>> GetAllOrders()
        {
            var orders = await _dbContext.Orders
                .Include(o => o.User)
                .Include(o => o.Items)
                    .ThenInclude(i => i.Weapon)
                .ToListAsync();

            return orders.Select(o => new OrderDto
            {
                Id = o.Id,
                CreatedAt = o.CreatedAt,
                TotalPrice = o.TotalPrice,
                UserFullName = o.User.FullName,
                Items = o.Items.Select(i => new OrderItemDto
                {
                    WeaponId = i.WeaponId,
                    WeaponName = i.Weapon.Name,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                }).ToList()
            }).ToList();
        }

        public async Task<OrderDto> GetOrderById(int id)
        {
            var order = await _dbContext.Orders
                .Include(o => o.User)
                .Include(o => o.Items)
                    .ThenInclude(i => i.Weapon)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null) throw new Exception("Order not found");

            return new OrderDto
            {
                Id = order.Id,
                CreatedAt = order.CreatedAt,
                TotalPrice = order.TotalPrice,
                UserFullName = order.User.FullName,
                Items = order.Items.Select(i => new OrderItemDto
                {
                    WeaponId = i.WeaponId,
                    WeaponName = i.Weapon.Name,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                }).ToList()
            };
        }

        public async Task<OrderDto> CreateOrder(OrderCreateDto dto)
        {
            var user = await _dbContext.Users.FindAsync(dto.UserId);
            if (user == null) throw new Exception("User not found");

            var orderItems = new List<OrderItem>();
            decimal totalPrice = 0;

            foreach (var itemDto in dto.Items)
            {
                var weapon = await _dbContext.Weapons.FindAsync(itemDto.WeaponId);
                if (weapon == null) throw new Exception($"Weapon with ID {itemDto.WeaponId} not found");

                var orderItem = new OrderItem
                {
                    WeaponId = weapon.Id,
                    Quantity = itemDto.Quantity,
                    UnitPrice = weapon.Price
                };
                totalPrice += weapon.Price * itemDto.Quantity;
                orderItems.Add(orderItem);
            }

            var order = new Order
            {
                UserId = dto.UserId,
                CreatedAt = DateTime.UtcNow,
                TotalPrice = totalPrice,
                Items = orderItems
            };

            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync();

            return await GetOrderById(order.Id); 
        }
    }
}

