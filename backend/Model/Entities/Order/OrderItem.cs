namespace Model.Entities.Order
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int ServiceId { get; set; }
        public int CarId { get; set; }
        public int Status { get; set; }
        public string Description { get; set; }
        public DateTime ArrivalTime { get; set; }
    }
}
