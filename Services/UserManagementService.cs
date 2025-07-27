// Smockerie/Services/UserManagementService.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Smockerie.DTO;
using Smockerie.Enum;
using BoutiqueApi.Data;
using BoutiqueApi.Models;

namespace Smockerie.Services
{
    public class UserManagementService : IUserManagementService
    {
        private readonly BoutiqueContext _ctx;
        public UserManagementService(BoutiqueContext ctx) => _ctx = ctx;

        public async Task<IEnumerable<UserDTO>> GetAllAsync()
        {
            var users = await _ctx.Users.ToListAsync();
            return users.Select(u => new UserDTO
            {
                Id = u.Id,
                Username = u.Username,
                Email = u.Email,
                Role = u.Role,
                CreatedAt = u.CreatedAt,
                UpdatedAt = u.UpdatedAt
            });
        }

        public async Task<UserDTO?> GetByIdAsync(Guid id)
        {
            var u = await _ctx.Users.FindAsync(id);
            if (u == null) return null;
            return new UserDTO
            {
                Id = u.Id,
                Username = u.Username,
                Email = u.Email,
                Role = u.Role,
                CreatedAt = u.CreatedAt,
                UpdatedAt = u.UpdatedAt
            };
        }

        public async Task<UserDTO> CreateAsync(UserCreateDto input)
        {
            // vérifs d'unicité ici ou en controller si tu préfères
            var entity = new User
            {
                Id = Guid.NewGuid(),
                Username = input.Username,
                Email = input.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(input.Password),
                Role = input.Role,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            _ctx.Users.Add(entity);
            await _ctx.SaveChangesAsync();

            return new UserDTO
            {
                Id = entity.Id,
                Username = entity.Username,
                Email = entity.Email,
                Role = entity.Role,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };
        }

        public async Task<bool> UpdateAsync(Guid id, UserCreateDto input)
        {
            var u = await _ctx.Users.FindAsync(id);
            if (u == null) return false;

            u.Username = input.Username;
            u.Email = input.Email;
            if (!string.IsNullOrWhiteSpace(input.Password))
                u.PasswordHash = BCrypt.Net.BCrypt.HashPassword(input.Password);
            u.Role = input.Role;
            u.UpdatedAt = DateTime.UtcNow;

            await _ctx.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var u = await _ctx.Users.FindAsync(id);
            if (u == null) return false;
            _ctx.Users.Remove(u);
            await _ctx.SaveChangesAsync();
            return true;
        }
    }
}
