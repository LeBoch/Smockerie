// Smockerie/Controllers/UsersController.cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers() =>
            Ok(await _svc.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUser(Guid id)
        {
            var dto = await _svc.GetByIdAsync(id);
            return dto is null ? NotFound() : Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<UserDTO>> PostUser([FromBody] UserCreateDto input)
        {
            var dto = await _svc.CreateAsync(input);
            return CreatedAtAction(nameof(GetUser), new { id = dto.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(Guid id, [FromBody] UserCreateDto input) =>
            (await _svc.UpdateAsync(id, input)) ? NoContent() : NotFound();

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id) =>
            (await _svc.DeleteAsync(id)) ? NoContent() : NotFound();
    }
}
