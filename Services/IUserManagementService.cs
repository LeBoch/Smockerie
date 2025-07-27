using BoutiqueApi.Models;
using Smockerie.DTO;

namespace Smockerie.Services
{
    public interface IUserManagementService
    {
        Task<IEnumerable<UserDTO>> GetAllAsync();
        Task<UserDTO?> GetByIdAsync(Guid id);
        Task<UserDTO> CreateAsync(UserCreateDto input);
        Task<bool> UpdateAsync(Guid id, UserCreateDto input);
        Task<bool> DeleteAsync(Guid id);
    }
}
