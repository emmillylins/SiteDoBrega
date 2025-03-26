using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IAuthService
    {
        Task<List<ApplicationUserDTO>> GetUsersAsync();
        Task<ApplicationUserDTO> Login(LoginDTO login);
        Task Logout(string token);
        Task<string> Register(CreateApplicationUserDTO registerModel);
    }
}
