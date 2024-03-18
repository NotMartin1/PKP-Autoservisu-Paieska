namespace Model.Entities.Feedback.Request
{
    public class FeedbackCreateRequest
    {
        public int? ShopId { get; set; }
        public int? ClientId { get; set; }
        public string? Message { get; set; }
        public int? Rating { get; set; }
    }
}
