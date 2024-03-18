namespace Model.Entities
{
    public class ServiceResult
    {
        public ServiceResult(bool success, string? message)
        {
            Success = success;
            Message = message;
        }

        public ServiceResult(bool success)
        {
            Success = success;
        }

        public bool Success {  get; set; }
        public string? Message { get; set; }
    }


    public class ServiceResult<T>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
    }
}
