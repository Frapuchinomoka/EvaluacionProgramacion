using Microsoft.EntityFrameworkCore;
using EvaluacionProgramacion.Models;
using EvaluacionProgramacion.Services.Users.Data;
using EvaluacionProgramacion.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EvaluacionProgramacion.Services
{
    public class ProjectService
    {
        private readonly EvContext _dbContext;

        public ProjectService(EvContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Método para crear un nuevo proyecto
        public async Task<Proyecto> CreateProjectAsync(ProyectoDTO projectDTO)
        {
            var newProject = new Proyecto
            {
                Nombre = projectDTO.Nombre,
                Descripcion = projectDTO.Descripcion,
                HorasTotales = projectDTO.HorasTotales,
                Estado = "Pendiente", 
                HorasTrabajadas = 0,  
                FechaCreacion = DateTime.UtcNow
            };

            await _dbContext.Proyectos.AddAsync(newProject);
            await _dbContext.SaveChangesAsync();

            return newProject;
        }

        // Método para obtener todos los proyectos
        public async Task<List<Proyecto>> GetAllProjectsAsync()
        {
            return await _dbContext.Proyectos.ToListAsync();
        }

        // Método para obtener un proyecto por ID
        public async Task<Proyecto> GetProjectByIdAsync(int projectId)
        {
            return await _dbContext.Proyectos.FirstOrDefaultAsync(p => p.Id == projectId);
        }

        // Método para actualizar un proyecto existente
        public async Task<bool> UpdateProjectAsync(int projectId, ProyectoDTO projectDTO)
        {
            var existingProject = await _dbContext.Proyectos.FindAsync(projectId);

            if (existingProject == null)
            {
                return false; 
            }

            existingProject.Nombre = projectDTO.Nombre;
            existingProject.Descripcion = projectDTO.Descripcion;
            existingProject.HorasTotales = projectDTO.HorasTotales;

            _dbContext.Proyectos.Update(existingProject);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        // Método para eliminar un proyecto
        public async Task<bool> DeleteProjectAsync(int projectId)
        {
            var projectToDelete = await _dbContext.Proyectos.FindAsync(projectId);

            if (projectToDelete == null)
            {
                return false; 
            }

            _dbContext.Proyectos.Remove(projectToDelete);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        // Método para actualizar el estado de un proyecto
        public async Task<bool> UpdateProjectStatusAsync(int projectId, string newStatus)
        {
            var project = await _dbContext.Proyectos.FindAsync(projectId);

            if (project == null || !IsValidStatus(newStatus))
            {
                return false; 
            }

            project.Estado = newStatus;
            _dbContext.Proyectos.Update(project);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        // Método para agregar horas trabajadas a un proyecto
        public async Task<bool> AddWorkedHoursAsync(int projectId, int workedHours)
        {
            var project = await _dbContext.Proyectos.FindAsync(projectId);

            if (project == null || workedHours < 0)
            {
                return false; 
            }

            project.HorasTrabajadas += workedHours;

            // Asegurar que las horas trabajadas no excedan las horas totales
            if (project.HorasTrabajadas > project.HorasTotales)
            {
                project.HorasTrabajadas = project.HorasTotales; 
            }

            _dbContext.Proyectos.Update(project);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        // Método para validar el estado del proyecto
        private bool IsValidStatus(string status)
        {
            return status == "Pendiente" || status == "En progreso" || status == "Finalizado";
        }
    }
}
