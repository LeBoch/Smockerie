using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Annotations;
using Smockerie.Services;
using Smockerie.DTO;
using BoutiqueApi.Models;

namespace Smockerie.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderProductsController : ControllerBase
    {
        private readonly IOrderProductService _svc;
        public OrderProductsController(IOrderProductService svc) => _svc = svc;

        // GET all
        [HttpGet]
        [Authorize]
        [SwaggerOperation(Summary = "Liste les produits commandés", Description = "Retourne toutes les lignes de commande")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<OrderProductDto>>> GetOrderProducts()
        {
            var list = await _svc.GetAllAsync();
            var dto = list.Select(op => new OrderProductDto
            {
                Id = op.Id,
                OrderId = op.OrderId,
                ProductId = op.ProductId,
                Name = op.Name,
                Price = op.Price,
                Quantity = op.Quantity,
                CreatedAt = op.CreatedAt
            });
            return Ok(dto);
        }

        // GET by id
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Détail d'un produit commandé", Description = "Retourne une ligne de commande par son identifiant")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OrderProductDto>> GetOrderProduct(Guid id)
        {
            var op = await _svc.GetByIdAsync(id);
            if (op == null) return NotFound();

            var dto = new OrderProductDto
            {
                Id = op.Id,
                OrderId = op.OrderId,
                ProductId = op.ProductId,
                Name = op.Name,
                Price = op.Price,
                Quantity = op.Quantity,
                CreatedAt = op.CreatedAt
            };
            return Ok(dto);
        }

        // PUT update
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Met à jour un produit commandé", Description = "Modifie une ligne de commande existante")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutOrderProduct(Guid id, [FromBody] OrderProductCreateDto input)
        {
            // on récupère l’entité existante pour garder CreatedAt
            var existing = await _svc.GetByIdAsync(id);
            if (existing == null) return NotFound();

            // map input sur l’entité
            existing.OrderId = input.OrderId;
            existing.ProductId = input.ProductId;
            existing.Name = input.Name;
            existing.Price = input.Price;
            existing.Quantity = input.Quantity;

            var updated = await _svc.UpdateAsync(existing);
            return updated ? NoContent() : NotFound();
        }


        // DELETE
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Supprime un produit commandé", Description = "Supprime la ligne de commande spécifiée")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteOrderProduct(Guid id)
        {
            var deleted = await _svc.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
