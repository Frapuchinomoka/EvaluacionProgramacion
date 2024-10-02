using EvaluacionProgramacion.Models;
using EvaluacionProgramacion.Services.Users.Data;
using Microsoft.EntityFrameworkCore;

namespace EvaluacionProgramacion.Services
{
    public class RolesService
    {
        private readonly EvContext DbContext;

        public RolesService(EvContext dbcontext)
        {
            DbContext = dbcontext;
        }

        public async Task<Rol> ObtenerRoles(int id)
        {
            Rol rol = await DbContext.Roles.FindAsync(id); 
            return rol;
        }
    }
}
