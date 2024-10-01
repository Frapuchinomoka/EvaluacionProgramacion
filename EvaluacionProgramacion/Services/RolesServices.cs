using EvaluacionProgramacion.Models;
using EvaluacionProgramacion.Services.Users.Data;
using Microsoft.EntityFrameworkCore;

namespace EvaluacionProgramacion.Services
{
    public class RolesService
    {
        private readonly EvContext _dbcontext;

        public RolesService(EvContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<Rol> ObtenerRoles(int id)
        {
            Rol rol = await _dbcontext.Roles.FindAsync(id); 
            return rol;
        }
    }
}
