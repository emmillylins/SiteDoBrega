using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IAuthService
    {
        Task<object> GetUsersAsync();
        Task<object> Login(LoginDTO login);
        Task Logout(string token);
        Task<string> Register(CreateApplicationUserDTO registerModel);
    }
}
