namespace ags_client.Types
{
    public class EditResult
    {
        public int? objectId { get; set; }
        public string globalId { get; set; }
        public bool? success { get; set; }
        public ErrorResult error { get; set; }
    }
}
