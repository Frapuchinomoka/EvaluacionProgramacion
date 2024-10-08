﻿using Microsoft.EntityFrameworkCore;
using EvaluacionProgramacion.Models;
using EvaluacionProgramacion.Services.Users.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvaluacionProgramacion.Services
{
    public class HerramientaService
    {
        private readonly EvContext DbContext;

        public HerramientaService(EvContext dbcontext)
        {
            DbContext = dbcontext;
        }

        // Método para añadir una nueva herramienta
        public async Task<Herramienta> AddHerramientaAsync(Herramienta nuevaHerramienta)
        {
            if (string.IsNullOrWhiteSpace(nuevaHerramienta.Nombre))
            {
                throw new ArgumentException("El nombre de la herramienta es necesario.");
            }

            // Comprobar si la herramienta ya existe
            var existingTool = await DbContext.Herramientas
                .FirstOrDefaultAsync(h => h.Nombre.Equals(nuevaHerramienta.Nombre, StringComparison.OrdinalIgnoreCase));

            if (existingTool != null)
            {
                throw new InvalidOperationException("La herramienta ya se encuentra registrada.");
            }

            await DbContext.Herramientas.AddAsync(nuevaHerramienta);
            await DbContext.SaveChangesAsync();

            return nuevaHerramienta;
        }

        // Método para obtener la lista de herramientas
        public async Task<List<Herramienta>> GetAllHerramientasAsync()
        {
            return await DbContext.Herramientas.ToListAsync();
        }

        // Método para obtener una herramienta por su ID
        public async Task<Herramienta> GetHerramientaByIdAsync(int herramientaId)
        {
            return await DbContext.Herramientas.FindAsync(herramientaId);
        }

        // Método para actualizar una herramienta existente
        public async Task<bool> UpdateHerramientaAsync(int herramientaId, Herramienta herramientaActualizada)
        {
            var herramientaEnDb = await DbContext.Herramientas.FindAsync(herramientaId);

            if (herramientaEnDb == null)
            {
                return false; // La herramienta no se encuentra
            }

            if (string.IsNullOrWhiteSpace(herramientaActualizada.Nombre))
            {
                throw new ArgumentException("El nombre de la herramienta es necesario.");
            }

            // Modificar el nombre de la herramienta
            herramientaEnDb.Nombre = herramientaActualizada.Nombre;

            DbContext.Herramientas.Update(herramientaEnDb);
            await DbContext.SaveChangesAsync();

            return true;
        }

        // Método para eliminar una herramienta
        public async Task<bool> DeleteHerramientaAsync(int herramientaId)
        {
            var herramientaToDelete = await DbContext.Herramientas.FindAsync(herramientaId);

            if (herramientaToDelete == null)
            {
                return false; // La herramienta no se encuentra
            }

            DbContext.Herramientas.Remove(herramientaToDelete);
            await DbContext.SaveChangesAsync();

            return true;
        }
    }
}