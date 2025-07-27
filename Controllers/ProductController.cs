using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var deleted = await _svc.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
        // GET: api/product/category/5
        [HttpGet("category/{categoryId}")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetByCategory(int categoryId)
        {
            var products = await _svc.GetByCategoryAsync(categoryId);
            return Ok(products);
        }
    }
}
