// CategoryController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Annotations;
using Smockerie.DTO;
using Smockerie.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Smockerie.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        /// <summary>
        /// GET /api/category
        /// Retourne la liste de toutes les catégories.
        /// </summary>
        [HttpGet]
        [SwaggerOperation(Summary = "Liste toutes les catégories", Description = "Retourne toutes les catégories disponibles")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetAll()
        {
            var dtos = await _categoryService.GetAllCategoriesAsync();
            return Ok(dtos);
        }
    }
}
