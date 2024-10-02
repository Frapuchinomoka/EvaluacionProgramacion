using Microsoft.EntityFrameworkCore;
using EvaluacionProgramacion.Models;
using EvaluacionProgramacion.Services.Users.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EvaluacionProgramacion.DTOs;

namespace EvaluacionProgramacion.Services
{
    public class UsuarioService
    {
        private readonly EvContext DbContext;

        public UsuarioService(EvContext dbContext) 
        {
            DbContext = dbContext;
        }

        // Crear un nuevo usuario usando UsuarioDTO
        public async Task<Usuario> CrearUsuarioAsync(UsuarioDTO usuarioDTO)
        {
            // Verificar si el email ya existe
            var usuarioExistente = await DbContext.Usuarios
                .FirstOrDefaultAsync(u => u.Email == usuarioDTO.Email);

            if (usuarioExistente != null)
            {
                throw new InvalidOperationException("\nEl email ya está registrado.\n");
            }

            // Verificar que el RolId exista en la tabla Roles
            var rolExistente = await DbContext.Roles.FindAsync(usuarioDTO.RolId);
            if (rolExistente == null)
            {
                throw new InvalidOperationException("\nEl RolId no existe.\n");
            }

            // Crear un nuevo objeto Usuario basado en el DTO
            var nuevoUsuario = new Usuario
            {
                Nombre = usuarioDTO.Nombre,
                Email = usuarioDTO.Email,
                Password = usuarioDTO.Password,
                RolId = usuarioDTO.RolId
            };

            DbContext.Usuarios.Add(nuevoUsuario);
            await DbContext.SaveChangesAsync();

            return nuevoUsuario;
        }

        // Obtener todos los usuarios
        public async Task<List<Usuario>> ObtenerUsuariosAsync()
        {
            return await DbContext.Usuarios.ToListAsync();
        }

        // Obtener un usuario por ID
        public async Task<Usuario> ObtenerUsuarioPorIdAsync(int id)
        {
            return await DbContext.Usuarios.FirstOrDefaultAsync(u => u.Id == id);
        }

        // Actualizar un usuario existente usando UsuarioDTO
        public async Task<bool> ActualizarUsuarioAsync(int id, UsuarioDTO usuarioDTO)
        {
            var usuarioExistente = await DbContext.Usuarios.FindAsync(id);

            if (usuarioExistente == null)
            {
                return false; 
            }

            // Verificar que el RolId exista en la tabla Roles
            var rolExistente = await DbContext.Roles.FindAsync(usuarioDTO.RolId);
            if (rolExistente == null)
            {
                throw new InvalidOperationException("El RolId no existe.");
            }

            // Actualizar los campos del usuario basado en el DTO
            usuarioExistente.Nombre = usuarioDTO.Nombre;
            usuarioExistente.Email = usuarioDTO.Email;
            usuarioExistente.Password = usuarioDTO.Password;
            usuarioExistente.RolId = usuarioDTO.RolId;

            DbContext.Usuarios.Update(usuarioExistente);
            await DbContext.SaveChangesAsync();

            return true;
        }

        // Eliminar un usuario
        public async Task<bool> EliminarUsuarioAsync(int id)
        {
            var usuario = await DbContext.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return false; 
            }

            DbContext.Usuarios.Remove(usuario);
            await DbContext.SaveChangesAsync();

            return true;
        }

        // Obtener usuarios por Rol
        public async Task<List<Usuario>> ObtenerUsuariosPorRolAsync(int rolId)
        {
            return await DbContext.Usuarios
                .Where(u => u.RolId == rolId)
                .ToListAsync();
        }
    }
}
