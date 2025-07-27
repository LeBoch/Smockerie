using BoutiqueApi.Models;

namespace Smockerie.Services
{
    public interface IUserService
    {
        Task<User?> ValidateCredentialsAsync(string username, string password);
        Task<User> RegisterAsync(string username, string email, string password);
    }
}