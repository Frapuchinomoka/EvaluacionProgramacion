using Microsoft.EntityFrameworkCore;
using EvaluacionProgramacion.Models;
using EvaluacionProgramacion.Services.Users.Data;
using EvaluacionProgramacion.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EvaluacionProgramacion.Services
{
    public class ProjectoService
    {
        private readonly EvContext DbContext;

        public ProjectoService(EvContext dbContext)
        {
            DbContext = dbContext;
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

            await DbContext.Proyectos.AddAsync(newProject);
            await DbContext.SaveChangesAsync();

            return newProject;
        }

        // Método para obtener todos los proyectos
        public async Task<List<Proyecto>> GetAllProjectsAsync()
        {
            return await DbContext.Proyectos.ToListAsync();
        }

        // Método para obtener un proyecto por ID
        public async Task<Proyecto> GetProjectByIdAsync(int projectId)
        {
            return await DbContext.Proyectos.FirstOrDefaultAsync(p => p.Id == projectId);
        }

        // Método para actualizar un proyecto existente
        public async Task<bool> UpdateProjectAsync(int projectId, ProyectoDTO projectDTO)
        {
            var existingProject = await DbContext.Proyectos.FindAsync(projectId);

            if (existingProject == null)
            {
                return false; 
            }

            existingProject.Nombre = projectDTO.Nombre;
            existingProject.Descripcion = projectDTO.Descripcion;
            existingProject.HorasTotales = projectDTO.HorasTotales;

            DbContext.Proyectos.Update(existingProject);
            await DbContext.SaveChangesAsync();

            return true;
        }

        // Método para eliminar un proyecto
        public async Task<bool> DeleteProjectAsync(int projectId)
        {
            var projectToDelete = await DbContext.Proyectos.FindAsync(projectId);

            if (projectToDelete == null)
            {
                return false; 
            }

            DbContext.Proyectos.Remove(projectToDelete);
            await DbContext.SaveChangesAsync();

            return true;
        }

        // Método para actualizar el estado de un proyecto
        public async Task<bool> UpdateProjectStatusAsync(int projectId, string newStatus)
        {
            var project = await DbContext.Proyectos.FindAsync(projectId);

            if (project == null || !IsValidStatus(newStatus))
            {
                return false; 
            }

            project.Estado = newStatus;
            DbContext.Proyectos.Update(project);
            await DbContext.SaveChangesAsync();

            return true;
        }

        // Método para agregar horas trabajadas a un proyecto
        public async Task<bool> AddWorkedHoursAsync(int projectId, int workedHours)
        {
            var project = await DbContext.Proyectos.FindAsync(projectId);

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

            DbContext.Proyectos.Update(project);
            await DbContext.SaveChangesAsync();

            return true;
        }

        // Método para validar el estado del proyecto
        private bool IsValidStatus(string status)
        {
            return status == "Pendiente" || status == "En progreso" || status == "Finalizado";
        }
    }
}
