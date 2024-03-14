namespace Model.Entities.Authorization.Response
{
    public class RegistrationResponse
    {
        public RegistrationResultCode ResultCode { get; set; }

        public RegistrationResponse(RegistrationResultCode resultCode)
        {
            ResultCode = resultCode;
        }
    }
}
