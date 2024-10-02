using EvaluacionProgramacion.DTOs;
using EvaluacionProgramacion.Responses;
using EvaluacionProgramacion.Services.Users.Data;
using Microsoft.AspNetCore.Mvc;
using EvaluacionProgramacion.Services;

namespace EvaluacionProgramacion.Controllers
{
    public class TareaControllers
    {
        [ApiController]
        [Route("api/[controller]")]
        public class TareaController : Controller
        {
            private readonly TareaService _tareaService;

            public TareaController(IServiceProvider serviceProvider)
            {
                var context = serviceProvider.GetRequiredService<EvContext>();
                _tareaService = new TareaService(context);
            }

            // Obtener todas las tareas
            [HttpGet("index")]
            public async Task<ActionResult<TareasResponse>> GetTareas()
            {
                var tareas = await _tareaService.ObtenerTareasAsync();
                return Ok(new TareasResponse
                {
                    Data = tareas,
                    Code = 200,
                    Message = "Tareas obtenidas correctamente"
                });
            }

            // Obtener una tarea por su ID
            [HttpGet("show/{id}")]
            public async Task<ActionResult<TareaResponse>> GetTarea(int id)
            {
                var tarea = await _tareaService.ObtenerTareaPorIdAsync(id);

                if (tarea == null)
                {
                    return NotFound(new
                    {
                        Code = 404,
                        Message = "Tarea no encontrada"
                    });
                }

                return Ok(new TareaResponse
                {
                    Data = tarea,
                    Code = 200,
                    Message = "Tarea obtenida correctamente"
                });
            }

            // Crear una nueva tarea
            [HttpPost("create")]
            public async Task<ActionResult<NuevaTareaResponse>> CrearTarea([FromBody] TareaDTO tareaDTO)
            {
                try
                {
                    var nuevaTarea = await _tareaService.CrearTareaAsync(tareaDTO);
                    return Ok(new NuevaTareaResponse
                    {
                        Data = true,
                        Code = 200,
                        Message = "Tarea creada correctamente"
                    });
                }
                catch (InvalidOperationException ex)
                {
                    return BadRequest(new
                    {
                        Code = 400,
                        Message = ex.Message
                    });
                }
            }

            // Actualizar una tarea
            [HttpPut("update/{id}")]
            public async Task<ActionResult<UpdateTareaResponse>> ActualizarTarea(int id, [FromBody] TareaDTO tareaDTO)
            {
                try
                {
                    var actualizado = await _tareaService.ActualizarTareaAsync(id, tareaDTO);

                    if (!actualizado)
                    {
                        return NotFound(new
                        {
                            Code = 404,
                            Message = "Tarea no encontrada"
                        });
                    }

                    return Ok(new UpdateTareaResponse
                    {
                        Data = true,
                        Code = 200,
                        Message = "Tarea actualizada correctamente"
                    });
                }
                catch (InvalidOperationException ex)
                {
                    return BadRequest(new
                    {
                        Code = 400,
                        Message = ex.Message
                    });
                }
            }

            // Eliminar una tarea
            [HttpDelete("delete/{id}")]
            public async Task<ActionResult<DeleteTareaResponse>> EliminarTarea(int id)
            {
                var eliminado = await _tareaService.EliminarTareaAsync(id);

                if (!eliminado)
                {
                    return NotFound(new
                    {
                        Code = 404,
                        Message = "Tarea no encontrada"
                    });
                }

                return Ok(new DeleteTareaResponse
                {
                    Data = true,
                    Code = 200,
                    Message = "Tarea eliminada correctamente"
                });
            }
        }
    }
}
