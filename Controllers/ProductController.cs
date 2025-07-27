using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Annotations;
using Smockerie.Services;
using Smockerie.DTO;
using BoutiqueApi.Models;

namespace Smockerie.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _svc;
        public ProductsController(IProductService svc) => _svc = svc;

        // GET: api/Products
        [HttpGet]
        [SwaggerOperation(Summary = "Liste les produits", Description = "Retourne tous les produits")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            var list = await _svc.GetAllAsync();
            return Ok(list.Select(p => new ProductDto
            {
                Id= p.Id,
                Name = p.Name,
                Price = p.Price,
                Stock = p.Stock,
                imageUrl = p.ImageUrl
            }));
        }

        // GET: api/Products/{id}
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Détail d'un produit", Description = "Retourne un produit par son identifiant")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var p = await _svc.GetByIdAsync(id);
            if (p == null) return NotFound();
            return Ok(new ProductDto
            {
                Name = p.Name,
                Price = p.Price,
                Stock = p.Stock,
            });
        }

        // POST: api/Products
        [HttpPost]
        [SwaggerOperation(Summary = "Crée un produit", Description = "Ajoute un nouveau produit")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<ProductDto>> PostProduct([FromBody] ProductCreateDto input)
        {
            var created = await _svc.CreateAsync(input);

            var dto = new ProductDto
            {
                Id = created.Id,
                Name = created.Name,
                Price = created.Price,
                Stock = created.Stock,
                imageUrl = created.ImageUrl,
                CategoryId = created.CategoryId,
                FlavorId = created.FlavorId
            };

            return CreatedAtAction(nameof(GetProduct), new { id = dto.Id }, dto);
        }

        // PUT: api/Products/{id}
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Met à jour un produit", Description = "Modifie les informations d'un produit existant")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutProduct(int id, [FromBody] ProductCreateDto input)
        {
            var existing = await _svc.GetByIdAsync(id);
            if (existing == null) return NotFound();

            existing.Name = input.Name;
            existing.Price = input.Price;
            existing.Stock = input.Stock;

            var updated = await _svc.UpdateAsync(existing);
            return updated ? NoContent() : NotFound();
        }

        // DELETE: api/Products/{id}
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Supprime un produit", Description = "Supprime le produit spécifié")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var deleted = await _svc.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
        // GET: api/product/category/5
        [HttpGet("category/{categoryId}")]
        [SwaggerOperation(Summary = "Liste les produits d'une catégorie", Description = "Retourne les produits pour la catégorie donnée")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetByCategory(int categoryId)
        {
            var products = await _svc.GetByCategoryAsync(categoryId);
            return Ok(products);
        }
    }
}
