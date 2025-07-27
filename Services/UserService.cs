// Dossier : Smockerie/Services/UserService.cs
using System;
using System.Threading.Tasks;
using BoutiqueApi.Data;
using BoutiqueApi.Models;
using Microsoft.EntityFrameworkCore;
using Smockerie.Enum;

namespace Smockerie.Services
{
    public class UserService : IUserService
    {
        private readonly BoutiqueContext _ctx;
        public UserService(BoutiqueContext ctx) => _ctx = ctx;

        public async Task<User?> ValidateCredentialsAsync(string username, string password)
        {
            var user = await _ctx.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null) return null;
            return BCrypt.Net.BCrypt.Verify(password, user.PasswordHash)
                ? user
                : null;
        }

        public async Task<User> RegisterAsync(string username, string email, string password)
        {
            // on suppose que l’unicité a déjà été vérifiée avant appel
            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = username,
                Email = email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
                Role = UserRole.Admin,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            _ctx.Users.Add(user);
            await _ctx.SaveChangesAsync();
            return user;
        }
    }
}
