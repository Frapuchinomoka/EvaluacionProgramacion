using EvaluacionProgramacion.Models;
using EvaluacionProgramacion.Services.Users.Data;
using Microsoft.EntityFrameworkCore; 

namespace EvaluacionProgramacion.Services
{
    public class RolesService
    {
        private readonly EvContext _context;

        public RolesService(EvContext context)
        {
            _context = context; 
        }

        // Método para obtener un rol específico por ID
        public async Task<Rol> ObtenerRoles(int id)
        {
            Rol rol = await _context.Roles.FindAsync(id);
            return rol;
        }
    }
}
