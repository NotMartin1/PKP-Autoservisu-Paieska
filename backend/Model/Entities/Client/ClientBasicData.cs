namespace Model.Entities.Client
{
    public class ClientBasicData
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public bool IsEnabled { get; set; }
        public string Password { get; internal set; }
    }
}
