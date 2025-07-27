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

    }
}
