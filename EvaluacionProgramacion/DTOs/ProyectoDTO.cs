namespace EvaluacionProgramacion.DTOs
{
    public class ProyectoDTO
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public string Estado { get; set; }

        public int HorasTrabajadas { get; set; }

        public int HorasTotales { get; set; }

        public DateTime FechaCreacion { get; set; }
    }
}
