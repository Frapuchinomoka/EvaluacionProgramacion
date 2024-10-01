using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EvaluacionProgramacion.DTOs;
using EvaluacionProgramacion.Models;
using EvaluacionProgramacion.Services.Users.Data;

namespace Practico2.Services
{
    public class TareaService
    {
        private readonly EvContext _dbContext;

        public TareaService(EvContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Crear una nueva tarea usando TareaDTO
        public async Task<Tarea> CrearTareaAsync(TareaDTO tareaDTO)
        {
            // Verificar que el proyecto exista
            var proyectoExistente = await _dbContext.Proyectos.FindAsync(tareaDTO.ProyectoId);
            if (proyectoExistente == null)
            {
                throw new InvalidOperationException("El proyecto no existe.");
            }

            // Verificar que el empleado exista
            var empleadoExistente = await _dbContext.Usuarios.FindAsync(tareaDTO.UsuarioId);
            if (empleadoExistente == null)
            {
                throw new InvalidOperationException("El empleado no existe.");
            }

            // Verificar que el set de herramientas sea válido
            await ValidarSetHerramientas(tareaDTO.SetHerramientas);

            // Crear un nuevo objeto Tarea basado en el DTO
            var nuevaTarea = new Tarea
            {
                FechaInicio = DateTime.Now,
                Estado = "Pendiente",
                Horas = tareaDTO.Horas,
                Area = tareaDTO.Area,
                ProyectoId = tareaDTO.ProyectoId,
                UsuarioId = tareaDTO.UsuarioId,
                SetHerramientas = tareaDTO.SetHerramientas
            };

            _dbContext.Tareas.Add(nuevaTarea);
            await _dbContext.SaveChangesAsync();

            return nuevaTarea;
        }

        // Obtener todas las tareas
        public async Task<List<Tarea>> ObtenerTareasAsync()
        {
            return await _dbContext.Tareas.ToListAsync();
        }

        // Obtener una tarea por ID
        public async Task<Tarea> ObtenerTareaPorIdAsync(int id)
        {
            return await _dbContext.Tareas.FirstOrDefaultAsync(t => t.Id == id);
        }

        // Actualizar una tarea existente usando TareaDTO
        public async Task<bool> ActualizarTareaAsync(int id, TareaDTO tareaDTO)
        {
            var tareaExistente = await _dbContext.Tareas.FindAsync(id);

            if (tareaExistente == null)
            {
                return false; 
            }

            // Verificar que el proyecto exista
            var proyectoExistente = await _dbContext.Proyectos.FindAsync(tareaDTO.ProyectoId);
            if (proyectoExistente == null)
            {
                throw new InvalidOperationException("El proyecto no existe.");
            }

            // Verificar que el empleado exista
            var empleadoExistente = await _dbContext.Usuarios.FindAsync(tareaDTO.UsuarioId);
            if (empleadoExistente == null)
            {
                throw new InvalidOperationException("El empleado no existe.");
            }

            // Verificar que el set de herramientas sea válido
            await ValidarSetHerramientas(tareaDTO.SetHerramientas);

            // Actualizar los campos de la tarea basado en el DTO
            tareaExistente.Horas = tareaDTO.Horas;
            tareaExistente.Area = tareaDTO.Area;
            tareaExistente.ProyectoId = tareaDTO.ProyectoId;
            tareaExistente.UsuarioId = tareaDTO.UsuarioId;
            tareaExistente.SetHerramientas = tareaDTO.SetHerramientas;

            _dbContext.Tareas.Update(tareaExistente);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        // Eliminar una tarea
        public async Task<bool> EliminarTareaAsync(int id)
        {
            var tarea = await _dbContext.Tareas.FindAsync(id);

            if (tarea == null)
            {
                return false; 
            }

            _dbContext.Tareas.Remove(tarea);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        // Método para validar el set de herramientas
        private async Task ValidarSetHerramientas(string setHerramientas)
        {
            if (string.IsNullOrWhiteSpace(setHerramientas))
            {
                throw new InvalidOperationException("El set de herramientas no puede estar vacío.");
            }

            // Separar los IDs por comas y eliminar espacios
            var idsHerramientas = setHerramientas.Split(',')
                .Select(id => id.Trim())
                .ToList();

            // Verificar que cada ID de herramienta exista en la base de datos
            foreach (var id in idsHerramientas)
            {
                if (!int.TryParse(id, out var herramientaId))
                {
                    throw new InvalidOperationException($"ID de herramienta no válido: {id}");
                }

                var herramientaExistente = await _dbContext.Herramientas.FindAsync(herramientaId);
                if (herramientaExistente == null)
                {
                    throw new InvalidOperationException($"La herramienta con ID {herramientaId} no existe.");
                }
            }
        }
    }
}
