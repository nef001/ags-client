namespace ags_client.Types
{
    public class LabelClass
    {
        public string labelPlacement { get; set; }
        public string labelExpression { get; set; }
        public bool? useCodedValues { get; set; }
        public Symbol symbol { get; set; }
        public double? minScale { get; set; }
        public double? maxScale { get; set; }
        public string where { get; set; }
    }
}
