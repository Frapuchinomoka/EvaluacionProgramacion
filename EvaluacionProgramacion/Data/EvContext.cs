using Microsoft.EntityFrameworkCore;
using EvaluacionProgramacion.Models;
using System.Threading;

namespace EvaluacionProgramacion.Services.Users.Data
{
    public class EvContext : DbContext
    {
        public EvContext(DbContextOptions<EvContext> options) : base(options)
        {
        }

        /* DbSet indica el modelo que se va a mapear (reflejar) a la base de datos */
        public DbSet<Rol> Roles { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Tarea> Tareas { get; set; }

        public DbSet<Proyecto> Proyectos { get; set; }

        public DbSet<Herramienta> Herramientas { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Acá se pueden cargar los datos iniciales de la base de datos

            modelBuilder.Entity<Rol>().HasData(new Rol
            {
                Id = 1,
                Nombre = "Administrador"
            });

            modelBuilder.Entity<Rol>().HasData(new Rol
            {
                Id = 2,
                Nombre = "Empleado"
            });

        }

    }

}