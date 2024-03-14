namespace Model.Entities
{
    public class ServiceResult
    { 
        public bool Success {  get; set; }
        public string Message { get; set; }
    }

    public class ServiceResult<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
