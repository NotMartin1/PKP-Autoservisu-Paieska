namespace Model.Entities.Order
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int ServiceId { get; set; }
        public int CarId { get; set; }
        public string OrderStatusDescription { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public string Description { get; set; }
        public DateTime ArrivalTime { get; set; }
    }
}
