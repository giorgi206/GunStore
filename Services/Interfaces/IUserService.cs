using System;
using GunShop.DTOs.Users;

namespace GunShop.Services.Interfaces
{
	public interface IUserService
	{
        Task<List<UserDto>> GetAllUsers();
        Task<UserDto> CreateUser(UserCreateDto dto);
        Task<UserDto> Login(UserLoginDto dto);
    }
}

