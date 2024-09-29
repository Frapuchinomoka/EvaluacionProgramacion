using System.ComponentModel.DataAnnotations.Schema;

namespace EvaluacionProgramacion.Models
{
    public class Tarea
    {
        public int Id { get; set; }
        public DateTime FechaInicio { get; set; }
        public string Estado { get; set; }
        public int Horas { get; set; }
        public string Area { get; set; }

        [ForeignKey("Proyectos")]
        public int ProyectoId { get; set; }


        public int UsuarioId { get; set; }


        public string SetHerramientas { get; set; }
    }

}
