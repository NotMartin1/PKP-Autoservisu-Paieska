namespace Model.Entities.Authorization.Request
{
    public class RegistrationRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class RegistrationRequest<T> : RegistrationRequest where T : class
    {
        public T AdditionalData { get; set; }
    }
}
