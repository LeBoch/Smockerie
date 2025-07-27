// CategoryService.cs
using Microsoft.EntityFrameworkCore;
using BoutiqueApi.Data;
using BoutiqueApi.Models;
using Smockerie.DTO;

namespace Smockerie.Services
{
    /// <summary>
    /// Implémentation concrète de ICategoryService.
    /// </summary>
    public class CategoryService : ICategoryService
    {
        private readonly BoutiqueContext _context;

        public CategoryService(BoutiqueContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllCategoriesAsync()
        {
            // On projette directement les entités en DTO pour ne pas exposer l'entité JPA
            return await _context.Categories
                .AsNoTracking()
                .Select(c => new CategoryDTO
                {
                    Id = c.Id,
                    Name = c.Name,
                })
                .ToListAsync();
        }
    }
}
