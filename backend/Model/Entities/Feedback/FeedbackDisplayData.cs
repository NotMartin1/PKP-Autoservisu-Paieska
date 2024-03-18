namespace Model.Entities.Feedback
{
    public class FeedbackDisplayData
    {
        public string? ClientName { get; set; }
        public string? WorkshopCompanyName { get; set; }
        public string? Message { get; set; }
        public int? Rating { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
