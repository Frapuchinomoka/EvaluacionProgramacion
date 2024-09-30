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

        public async Task<List<Rol>> ListaRoles()
        {
            List<Rol> roles = await _context.Roles.ToListAsync();
            return roles;
        }

        public async Task<Rol> ObtenerRoles(int id)
        {
            Rol rol = await _context.Roles.FindAsync(id);
            return rol;
        }
    }
}
