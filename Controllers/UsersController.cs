// Smockerie/Controllers/UsersController.cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Annotations;
using Smockerie.Services;
using Smockerie.DTO;

namespace Smockerie.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class UsersController : ControllerBase
    {
        private readonly IUserManagementService _svc;
        public UsersController(IUserManagementService svc) => _svc = svc;

        [HttpGet]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Liste des utilisateurs", Description = "Retourne tous les comptes enregistrés")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers() =>
            Ok(await _svc.GetAllAsync());

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Détail d'un utilisateur", Description = "Retourne un utilisateur par son identifiant")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserDTO>> GetUser(Guid id)
        {
            var dto = await _svc.GetByIdAsync(id);
            return dto is null ? NotFound() : Ok(dto);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Crée un utilisateur", Description = "Ajoute un compte utilisateur")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<UserDTO>> PostUser([FromBody] UserCreateDto input)
        {
            var dto = await _svc.CreateAsync(input);
            return CreatedAtAction(nameof(GetUser), new { id = dto.Id }, dto);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Met à jour un utilisateur", Description = "Modifie un compte existant")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutUser(Guid id, [FromBody] UserCreateDto input) =>
            (await _svc.UpdateAsync(id, input)) ? NoContent() : NotFound();

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Supprime un utilisateur", Description = "Supprime le compte spécifié")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteUser(Guid id) =>
            (await _svc.DeleteAsync(id)) ? NoContent() : NotFound();
    }
}
