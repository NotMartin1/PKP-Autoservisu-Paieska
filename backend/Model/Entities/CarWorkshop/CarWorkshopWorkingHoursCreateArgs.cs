namespace Model.Entities.CarWorkshop
{
    public class CarWorkshopWorkingHoursCreateArgs
    {
        public int workshopId { get; set; }
        public string? Monday { get; set; }
        public string? Tuesday { get; set; }
        public string? Wednesday { get; set; }
        public string? Thursday { get; set; }
        public string? Friday { get; set; }
        public string? Saturday { get; set; }
        public string? Sunday { get; set; }
    }
}