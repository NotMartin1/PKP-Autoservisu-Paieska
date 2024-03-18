namespace Model.Entities.Filter
{
    public class ListArgs
    {
        public string? OrderColumnName { get; set; }
        public OrderDirection? OrderDirection { get; set; }
        public int? Take { get; set; }
        public int? Skip { get; set; }
        public List<FilterArgs> Filters { get; set; }
    }
}
