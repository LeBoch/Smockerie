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

    }
}
