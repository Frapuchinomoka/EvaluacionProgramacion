using System.Threading;

namespace EvaluacionProgramacion.Models
{
    public class Proyecto
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }

        public ICollection<Tarea> Tareas { get; set; }
    }
}
