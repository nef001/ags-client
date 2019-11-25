
using ags_client.Types;

namespace ags_client
{
    /* Base class for all REST responses.
     * Contains the path to the requested resource.
     * Always check for the error property first.
     */
    public class BaseResponse
    {
        public string resourcePath { get; set; }
        public ErrorDetail error { get; set; }

    }
}
