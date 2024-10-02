using EvaluacionProgramacion.DTOs;
using EvaluacionProgramacion.Models;
using EvaluacionProgramacion.Services;
using EvaluacionProgramacion.Services.Users.Data;
using Microsoft.AspNetCore.Mvc;

namespace EvaluacionProgramacion.Controllers
{
    public class ProyectoControllers
    {
        [ApiController]
        [Route("api/[controller]")]
        public class ProyectoController : Controller
        {
            private readonly ProjectService _proyectoService;

            public ProyectoController(IServiceProvider serviceProvider)
            {
                var context = serviceProvider.GetRequiredService<EvContext>();
                _proyectoService = new ProjectService(context);
            }

            // Obtener todos los proyectos
            [HttpGet("index")]
            public async Task<ActionResult<List<Proyecto>>> GetProyectos()
            {
                var proyectos = await _proyectoService.GetAllProjectsAsync();
                return Ok(new
                {
                    Data = proyectos,
                    Code = 200,
                    Message = "Proyectos obtenidos correctamente"
                });
            }

            // Obtener un proyecto por su ID
            [HttpGet("show/{id}")]
            public async Task<ActionResult<Proyecto>> GetProyecto(int id)
            {
                var proyecto = await _proyectoService.GetProjectByIdAsync(id);

                if (proyecto == null)
                {
                    return NotFound(new
                    {
                        Code = 404,
                        Message = "Proyecto no encontrado"
                    });
                }

                return Ok(new
                {
                    Data = proyecto,
                    Code = 200,
                    Message = "Proyecto obtenido correctamente"
                });
            }

            // Crear un nuevo proyecto
            [HttpPost("create")]
            public async Task<ActionResult<Proyecto>> CrearProyecto([FromBody] ProyectoDTO proyectoDTO)
            {
                var nuevoProyecto = await _proyectoService.CreateProjectAsync(proyectoDTO);

                return Ok(new
                {
                    Data = nuevoProyecto,
                    Code = 200,
                    Message = "Proyecto creado correctamente"
                });
            }

            // Actualizar un proyecto
            [HttpPut("update/{id}")]
            public async Task<ActionResult> ActualizarProyecto(int id, [FromBody] ProyectoDTO proyectoDTO)
            {
                var actualizado = await _proyectoService.UpdateProjectAsync(id, proyectoDTO);

                if (!actualizado)
                {
                    return NotFound(new
                    {
                        Code = 404,
                        Message = "Proyecto no encontrado"
                    });
                }

                return Ok(new
                {
                    Code = 200,
                    Message = "Proyecto actualizado correctamente"
                });
            }

            // Eliminar un proyecto
            [HttpDelete("delete/{id}")]
            public async Task<ActionResult> EliminarProyecto(int id)
            {
                var eliminado = await _proyectoService.DeleteProjectAsync(id);

                if (!eliminado)
                {
                    return NotFound(new
                    {
                        Code = 404,
                        Message = "Proyecto no encontrado"
                    });
                }

                return Ok(new
                {
                    Code = 200,
                    Message = "Proyecto eliminado correctamente"
                });
            }

            // Actualizar el estado de un proyecto
            [HttpPatch("actualizar-estado/{id}")]
            public async Task<ActionResult> ActualizarEstado(int id, [FromBody] string nuevoEstado)
            {
                var actualizado = await _proyectoService.UpdateProjectStatusAsync(id, nuevoEstado);

                if (!actualizado)
                {
                    return BadRequest(new
                    {
                        Code = 400,
                        Message = "El estado proporcionado no es válido o el proyecto no fue encontrado"
                    });
                }

                return Ok(new
                {
                    Code = 200,
                    Message = "Estado del proyecto actualizado correctamente"
                });
            }

            // Agregar horas trabajadas a un proyecto
            [HttpPatch("agregar-horas/{id}")]
            public async Task<ActionResult> AgregarHorasTrabajadas(int id, [FromBody] int horasTrabajadas)
            {
                var horasAgregadas = await _proyectoService.AddWorkedHoursAsync(id, horasTrabajadas);

                if (!horasAgregadas)
                {
                    return BadRequest(new
                    {
                        Code = 400,
                        Message = "No se pudieron agregar las horas trabajadas. Verifique los datos ingresados o que el proyecto exista."
                    });
                }

                return Ok(new
                {
                    Code = 200,
                    Message = "Horas trabajadas agregadas correctamente"
                });
            }
        }
    }
}
