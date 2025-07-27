using BoutiqueApi.Models;
using Smockerie.DTO;

namespace Smockerie.Services
{
    public interface IUserManagementService
    {
        Task<IEnumerable<UserDTO>> GetAllAsync();
        Task<UserDTO?> GetByIdAsync(Guid id);
   
    }
}
