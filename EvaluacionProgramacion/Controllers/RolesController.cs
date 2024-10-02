using Microsoft.AspNetCore.Mvc;
using EvaluacionProgramacion.Services;
using EvaluacionProgramacion.Models;
using EvaluacionProgramacion.Responses;
using EvaluacionProgramacion.Services.Users.Data;

namespace EvaluacionProgramacion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class RolesController : Controller
    {
        private readonly RolesService _rolesService;

        public RolesController(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<EvContext>();
            _rolesService = new RolesService(context);
        }

        // Obtener un rol por su ID
        [HttpGet("show/{id}")]
        public async Task<ActionResult<RolesResponse>> GetRol(int id)
        {
            var rol = await _rolesService.ObtenerRoles(id);

            if (rol == null)
            {
                return NotFound(new
                {
                    Code = 404,
                    Message = "Rol no encontrado"
                });
            }

            return Ok(new RolesResponse
            {
                Data = rol,
                Code = 200,
                Message = "Rol obtenido correctamente"
            });
        }
    }
}
