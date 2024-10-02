using EvaluacionProgramacion.DTOs;
using EvaluacionProgramacion.Responses;
using EvaluacionProgramacion.Services;
using EvaluacionProgramacion.Services.Users.Data;
using Microsoft.AspNetCore.Mvc;

namespace EvaluacionProgramacion.Controllers
{
    
        [ApiController]
        [Route("api/[controller]")]
        public class UsuarioController : Controller
        {
            private readonly UsuarioService _usuarioService;

            public UsuarioController(IServiceProvider serviceProvider)
            {
                var context = serviceProvider.GetRequiredService<EvContext>();
                _usuarioService = new UsuarioService(context);
            }

            // Obtener todos los usuarios
            [HttpGet("index")]
            public async Task<ActionResult<UsuariosResponse>> GetUsuarios()
            {
                var usuarios = await _usuarioService.ObtenerUsuariosAsync();
                return Ok(new UsuariosResponse
                {
                    Data = usuarios,
                    Code = 200,
                    Message = "Usuarios obtenidos correctamente"
                });
            }

            // Obtener un usuario por su ID
            [HttpGet("show/{id}")]
            public async Task<ActionResult<UsuarioResponse>> GetUsuario(int id)
            {
                var usuario = await _usuarioService.ObtenerUsuarioPorIdAsync(id);

                if (usuario == null)
                {
                    return NotFound(new
                    {
                        Code = 404,
                        Message = "Usuario no encontrado"
                    });
                }

                return Ok(new UsuarioResponse
                {
                    Data = usuario,
                    Code = 200,
                    Message = "Usuario obtenido correctamente"
                });
            }

            // Crear un nuevo usuario
            [HttpPost("create")]
            public async Task<ActionResult<NuevoUsuarioResponse>> CrearUsuario([FromBody] UsuarioDTO usuarioDTO)
            {
                var nuevoUsuario = await _usuarioService.CrearUsuarioAsync(usuarioDTO);

                return Ok(new NuevoUsuarioResponse
                {
                    Data = true,
                    Code = 200,
                    Message = "Usuario creado correctamente"
                });
            }

            // Actualizar un usuario
            [HttpPut("update/{id}")]
            public async Task<ActionResult<UpdateUsuarioResponse>> ActualizarUsuario(int id, [FromBody] UsuarioDTO usuarioDTO)
            {
                var actualizado = await _usuarioService.ActualizarUsuarioAsync(id, usuarioDTO);

                if (!actualizado)
                {
                    return NotFound(new
                    {
                        Code = 404,
                        Message = "Usuario no encontrado"
                    });
                }

                return Ok(new UpdateUsuarioResponse
                {
                    Data = true,
                    Code = 200,
                    Message = "Usuario actualizado correctamente"
                });
            }

            // Eliminar un usuario
            [HttpDelete("delete/{id}")]
            public async Task<ActionResult<DeleteUsuarioResponse>> EliminarUsuario(int id)
            {
                var eliminado = await _usuarioService.EliminarUsuarioAsync(id);

                if (!eliminado)
                {
                    return NotFound(new
                    {
                        Code = 404,
                        Message = "Usuario no encontrado"
                    });
                }

                return Ok(new DeleteUsuarioResponse
                {
                    Data = true,
                    Code = 200,
                    Message = "Usuario eliminado correctamente"
                });
            }
        }
    }

