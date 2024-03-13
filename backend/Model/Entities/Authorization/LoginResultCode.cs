namespace Model.Entities.Authorization
{
    public enum LoginResultCode
    {
        InvalidCredentials,
        UnknownError,
        UserDisabled,
        Authorized,
    }
}
