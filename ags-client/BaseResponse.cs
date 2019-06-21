
using ags_client.Types;

namespace ags_client
{
    public class BaseResponse
    {
        public string resourcePath { get; set; }
        public ErrorDetail error { get; set; }
    }
}
