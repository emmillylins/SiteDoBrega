using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Application.DTOs;

namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IBaseRepository<ApplicationUser> _repository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly DataDbContext _context;

        public AuthService(IConfiguration configuration, IBaseRepository<ApplicationUser> repository, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, DataDbContext context)
        {
            _configuration = configuration;
            _repository = repository;
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public async Task<List<ApplicationUser>> GetUsersAsync()
        {
            try
            {
                return await _repository.SelectAsync();
            }
            catch (Exception) { throw; }
        }

        public async Task<Usuario> Login(LoginDTO login)
        {
            try
            {
                var users = await _repository.SelectAsync();
                var user = users.SingleOrDefault(u => u.Email == login.Email || u.UserName == login.Email) ?? throw new Exception("Usuário não encontrado.");
                
                if (!await _userManager.IsEmailConfirmedAsync(user)) 
                    throw new Exception("Email não confirmado, acesse seu email para confirmar sua conta.");

                var loginResult = await _signInManager.PasswordSignInAsync(user, login.Password, false, true);

                if (loginResult.Succeeded)
                {
                    Log.Information("Usuário {0} efetuou o login", user.Id);
                    await _signInManager.SignInAsync(user, false);

                    var token = await GerarJwt(user);

                    await _context.Tokens.AddAsync(new ApplicationUserToken
                    {
                        UserId = user.Id,
                        Value = token,
                        Name = "login token",
                        IsExpired = false,
                        CreationDate = DateTime.Now,
                        LoginProvider = ""
                    });

                    await _context.SaveChangesAsync();

                    return new Usuario()
                    {
                        TipoUsuario = user.TipoUsuario,
                        NomeUsuario = user.UserName ?? ""
                    };
                }
                else throw new Exception("Erro no login.");
            }
            catch (Exception) { throw; }
        }

        public async Task Logout(string token)
        {
            try
            {
                // Efetua o logout do usuário atual 
                await _signInManager.SignOutAsync();

                // Certifique-se de que _context.Tokens é um DbSet<Token>
                var userToken = await _context.Tokens.FirstOrDefaultAsync(t => t.Value == token);
                if (userToken != null)
                {
                    userToken.IsExpired = true;

                    // Atualiza o token no banco de dados
                    _context.Tokens.Update(userToken);
                    await _context.SaveChangesAsync(); // Não esqueça de salvar as alterações no contexto
                }

                Log.Information("Usuário efetuou logout com sucesso.");
            }
            catch (Exception ex)
            {
                Log.Error("Erro ao efetuar logout: {Message}", ex.Message);
                throw new Exception("Erro ao efetuar logout.");
            }
        }

        public async Task<string> Register(RegisterDTO registerModel)
        {
            try
            {
                // Verifica se o e-mail já está em uso
                var existingUser = await _userManager.FindByEmailAsync(registerModel.Email);
                if (existingUser != null) throw new Exception("E-mail já cadastrado.");

                // Criação do objeto ApplicationUser com os dados fornecidos
                var user = new ApplicationUser(registerModel.TipoUsuario)
                {
                    UserName = registerModel.UserName,
                    Email = registerModel.Email, 
                    EmailConfirmed = true // Considera o e-mail automaticamente confirmado
                };

                // Cria o usuário no banco de dados
                var result = await _userManager.CreateAsync(user, registerModel.Password);

                // Verifica se houve sucesso na criação
                if (result.Succeeded)
                {
                    Log.Information("Usuário {0} cadastrado com sucesso", user.Id);
                    return "Cadastro realizado com sucesso.";
                }
                else
                {
                    // Retorna os erros de criação
                    throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
                }
            }
            catch (Exception ex)
            {
                Log.Error("Erro ao registrar usuário: {0}", ex.Message);
                throw;
            }
        }

        public async Task<string> GerarJwt(ApplicationUser user)
        {
            // 1. Definir as claims do token
            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, user.Id),           
                new(JwtRegisteredClaimNames.Email, user.Email),       
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) 
            };

            // Adiciona roles como claims
            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            // 2. Chave de assinatura do token
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // 3. Configuração do token
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],          
                audience: _configuration["Jwt:Audience"],       
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),           
                signingCredentials: creds                      
            );

            // 4. Gerar a string do token
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public bool AllowsAnonymousRoute(Endpoint endpoint)
        {
            if (endpoint == null) return false;

            var allowAnonymousAttribute = endpoint.Metadata
                .OfType<AllowAnonymousAttribute>()
                .FirstOrDefault();

            return allowAnonymousAttribute != null;
        }
    }
}
