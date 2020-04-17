namespace ags_client.Types
{
    public class Template<TA>
        where TA : IRestAttributes
    {
        public string name { get; set; }
        public string description { get; set; }
        public TA prototype { get; set; } //??
    }
}
