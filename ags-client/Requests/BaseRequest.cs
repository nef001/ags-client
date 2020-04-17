namespace ags_client.Requests
{
    public abstract class BaseRequest
    {
        public abstract BaseResponse Execute(AgsClient client, string resourcePath);
    }
}
