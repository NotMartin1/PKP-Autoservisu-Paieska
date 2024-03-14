namespace Model.Entities.Authorization.Response
{
    public class LoginResponse<T> where T : class
    {
        public LoginResultCode ResultCode { get; set; }
        public T UserData { get; set; }

        public LoginResponse() { }

        public LoginResponse(LoginResultCode resultCode)
        {
            ResultCode = resultCode;
        }
    }
}
