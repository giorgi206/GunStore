using System;
using GunShop.Data;
using GunShop.DTOs.Users;
using GunShop.Models;
using GunShop.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GunShop.Services
{
	public class UserService : IUserService
    {
        private readonly ApplicationDbContext _dbContext;
        public UserService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<UserDto>> GetAllUsers()
        {
            return await _dbContext.Users
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    FullName = u.FullName,
                    Email = u.Email,
                    Role = "User"
                })
                .ToListAsync();
        }

        public async Task<UserDto> CreateUser(UserCreateDto dto)
        {
            var existingUser = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Email == dto.Email);

            if (existingUser != null)
                throw new Exception("This email is already registered.");

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var user = new User
            {
                FullName = dto.FullName,
                Email = dto.Email,
                Password = hashedPassword
            };

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            return new UserDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Role = "User"
            };
        }

        public async Task<UserDto> Login(UserLoginDto dto)
        {
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Email == dto.Email);

            if (user == null)
                throw new Exception("Invalid email or password.");

            bool passwordValid = BCrypt.Net.BCrypt.Verify(dto.Password, user.Password);

            if (!passwordValid)
                throw new Exception("Invalid email or password.");

            return new UserDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Role = "User"
            };
        }
    }
}

