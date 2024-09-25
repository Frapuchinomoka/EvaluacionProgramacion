namespace EvaluacionProgramacion.Responses
{
    public class ResponseBase<T>
    {
        public T Data { get; set; }
        public int Code { get; set; }
        public string Message { get; set; }
    }
}
