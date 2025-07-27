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
using Smockerie.Enum;

namespace Smockerie.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _svc;
        public OrdersController(IOrderService svc) => _svc = svc;

        // GET: api/Orders
        [HttpGet]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Liste les commandes", Description = "Retourne toutes les commandes enregistrées")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrders()
        {
            var list = await _svc.GetAllAsync();
            var dto = list.Select(o => new OrderDTO
            {
                Id = o.Id,
                FullName = o.FullName,
                Email = o.Email,
                Phone = o.Phone,
                Address = o.Address,
                Status = OrderStatus.Pending,
                CreatedAt = o.CreatedAt
            });
            return Ok(dto);
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Détail d'une commande", Description = "Retourne une commande par son identifiant")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OrderDTO>> GetOrder(Guid id)
        {
            var o = await _svc.GetByIdAsync(id);
            if (o == null) return NotFound();

            var dto = new OrderDTO

            {
                Id = o.Id,
                FullName = o.FullName,
                Email = o.Email,
                Phone = o.Phone,
                Address = o.Address,
                Status = OrderStatus.Pending,
                CreatedAt = o.CreatedAt
            };
            return Ok(dto);
        }

        //// POST: api/Orders
        //[HttpPost]
        //[SwaggerOperation(Summary = "Crée une commande", Description = "Ajoute une nouvelle commande")]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //public async Task<ActionResult<OrderDTO>> PostOrder([FromBody] OrderCreateDto input)
        //{
        //    var order = new Order
        //    {
        //        Id = Guid.NewGuid(),
        //        FullName = input.FullName,
        //        Email = input.Email,
        //        Phone = input.Phone,
        //        Address = input.Address,
        //        Status = OrderStatus.Pending,
        //        CreatedAt = DateTime.UtcNow
        //    };

        //    var created = await _svc.CreateAsync(order);

        //    var dto = new OrderDTO
        //    {
        //        Id = created.Id,
        //        FullName = created.FullName,
        //        Email = created.Email,
        //        Phone = created.Phone,
        //        Address = created.Address,
        //        Status = OrderStatus.Pending,
        //        CreatedAt = created.CreatedAt
        //    };

        //    return CreatedAtAction(nameof(GetOrder), new { id = dto.Id }, dto);
        //}

        //// PUT: api/Orders/5
        //[HttpPut("{id}")]
        //[SwaggerOperation(Summary = "Met à jour une commande", Description = "Modifie les informations d'une commande existante")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<IActionResult> PutOrder(Guid id, [FromBody] OrderCreateDto input)
        //{
        //    var existing = await _svc.GetByIdAsync(id);
        //    if (existing == null) return NotFound();

        //    // mappe les champs modifiables
        //    existing.FullName = input.FullName;
        //    existing.Email = input.Email;
        //    existing.Phone = input.Phone;
        //    existing.Address = input.Address;

        //    var updated = await _svc.UpdateAsync(existing);
        //    return updated ? NoContent() : NotFound();
        //}

        //// DELETE: api/Orders/5
        //[HttpDelete("{id}")]
        //[SwaggerOperation(Summary = "Supprime une commande", Description = "Supprime la commande spécifiée")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<IActionResult> DeleteOrder(Guid id)
        //{
        //    var deleted = await _svc.DeleteAsync(id);
        //    return deleted ? NoContent() : NotFound();
        //}
    }
}
