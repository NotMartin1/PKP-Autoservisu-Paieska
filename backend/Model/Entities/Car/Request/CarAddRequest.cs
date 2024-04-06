namespace Model.Entities.Car.Request
{
    public class CarAddRequest
    {
        public int Id { get; set; }
        public int? MakeId { get; set; }
        public string? Model { get; set; }
        public string? Engine { get; set; }
        public int? Mileage { get; set; }
        public DateOnly? ProductionYear { get; set; }
        public int? ClientId { get; set; }
    }
}
