using EvaluacionProgramacion.DTOs;
using EvaluacionProgramacion.Models;
using EvaluacionProgramacion.Services;
using EvaluacionProgramacion.Services.Users.Data;
using Microsoft.AspNetCore.Mvc;

namespace EvaluacionProgramacion.Controllers
{
    
        [ApiController]
        [Route("api/[controller]")]

        public class HerramientaController : Controller
        {
            private readonly HerramientaService _herramientaService;

            public HerramientaController(IServiceProvider serviceProvider)
            {
                var context = serviceProvider.GetRequiredService<EvContext>();
                _herramientaService = new HerramientaService(context);
            }

            // Obtener todas las herramientas
            [HttpGet("index")]
            public async Task<ActionResult<List<Herramienta>>> GetHerramientas()
            {
                var herramientas = await _herramientaService.GetAllHerramientasAsync();
                return Ok(new
                {
                    Data = herramientas,
                    Code = 200,
                    Message = "Herramientas obtenidas correctamente"
                });
            }

            // Obtener una herramienta por su ID
            [HttpGet("show/{id}")]
            public async Task<ActionResult<Herramienta>> GetHerramienta(int id)
            {
                var herramienta = await _herramientaService.GetHerramientaByIdAsync(id);

                if (herramienta == null)
                {
                    return NotFound(new
                    {
                        Code = 404,
                        Message = "Herramienta no encontrada"
                    });
                }

                return Ok(new
                {
                    Data = herramienta,
                    Code = 200,
                    Message = "Herramienta obtenida correctamente"
                });
            }

            // Crear una nueva herramienta
            [HttpPost("create")]
            public async Task<ActionResult<Herramienta>> CrearHerramienta([FromBody] HerramientaDTO herramientaDTO)
            {
                var nuevaHerramienta = new Herramienta
                {
                    Nombre = herramientaDTO.Nombre
                };

                await _herramientaService.AddHerramientaAsync(nuevaHerramienta);

                return Ok(new
                {
                    Data = nuevaHerramienta,
                    Code = 200,
                    Message = "Herramienta creada correctamente"
                });
            }

            // Actualizar una herramienta
            [HttpPut("update/{id}")]
            public async Task<ActionResult<bool>> ActualizarHerramienta(int id, [FromBody] Herramienta herramientaDTO)
            {
                var actualizado = await _herramientaService.UpdateHerramientaAsync(id, new Herramienta
                {
                    Nombre = herramientaDTO.Nombre
                });

                if (!actualizado)
                {
                    return NotFound(new
                    {
                        Code = 404,
                        Message = "Herramienta no encontrada"
                    });
                }

                return Ok(new
                {
                    Data = true,
                    Code = 200,
                    Message = "Herramienta actualizada correctamente"
                });
            }

            // Eliminar una herramienta
            [HttpDelete("delete/{id}")]
            public async Task<ActionResult<bool>> EliminarHerramienta(int id)
            {
                var eliminado = await _herramientaService.DeleteHerramientaAsync(id);

                if (!eliminado)
                {
                    return NotFound(new
                    {
                        Code = 404,
                        Message = "Herramienta no encontrada"
                    });
                }

                return Ok(new
                {
                    Data = true,
                    Code = 200,
                    Message = "Herramienta eliminada correctamente"
                });
            }
        }
    }

