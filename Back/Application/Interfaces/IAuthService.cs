using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IAuthService
    {
        Task<List<ApplicationUser>> GetUsersAsync();
        Task<Usuario> Login(LoginDTO login);
        Task Logout(string token);
        Task<string> Register(RegisterDTO registerModel);
    }
}
