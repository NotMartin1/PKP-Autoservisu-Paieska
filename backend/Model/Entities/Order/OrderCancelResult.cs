namespace Model.Entities.Order
{
    public enum OrderCancelResult
    {
        ValidationFailed,
        OrderNotFound,
        AlreadyCancelled,
        Success,
        UnknownError
    }
}
