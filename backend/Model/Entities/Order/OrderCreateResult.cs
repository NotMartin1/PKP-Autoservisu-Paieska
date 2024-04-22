namespace Model.Entities.Order
{
    public enum OrderCreateResult
    {
        ValidationFailed,
        ClientNotFound,
        ClientDisabled,
        CarNotFound,
        Success,
        UnknownError,
    }
}
