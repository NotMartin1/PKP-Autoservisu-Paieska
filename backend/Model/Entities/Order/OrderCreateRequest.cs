namespace Model.Entities.Order
{
    public class OrderCreateRequest
    {
        public int ClientId { get; set; }
        public int ServiceId { get; set; }
        public int CarId { get; set; }
        public string? Description { get; set; }
        public DateTime? ArrivalTime { get; set; }
    }
}
