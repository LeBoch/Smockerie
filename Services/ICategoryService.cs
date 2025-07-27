// ICategoryService.cs
using Smockerie.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Smockerie.Services
{
    /// <summary>
    /// Service abstraction pour gérer les catégories.
    /// </summary>
    public interface ICategoryService
    {
        /// <summary>
        /// Récupère toutes les catégories sous forme de DTO.
        /// </summary>
        Task<IEnumerable<CategoryDTO>> GetAllCategoriesAsync();
    }
}
