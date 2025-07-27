using BoutiqueApi.Models;
using Smockerie.DTO;

namespace Smockerie.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(int id);
        Task<Product> CreateAsync(ProductCreateDto input);
        Task<bool> UpdateAsync(Product p);
        Task<bool> DeleteAsync(Guid id);
        Task<IEnumerable<ProductDto>> GetByCategoryAsync(int categoryId);
    }
}
