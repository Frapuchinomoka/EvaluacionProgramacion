using EvaluacionProgramacion.Models;

namespace EvaluacionProgramacion.Responses
{

    public class TareaResponse : ResponseBase<Tarea>
        {

        }

        public class TareasResponse : ResponseBase<List<Tarea>>
        {

        }

        public class NuevaTareaResponse : ResponseBase<bool>
        {

        }

        public class UpdateTareaResponse : ResponseBase<bool>
        {

        }

        public class DeleteTareaResponse : ResponseBase<bool>
        {

        }

    }


