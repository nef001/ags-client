
using ags_client.Types;

namespace ags_client.Resources
{
    public class ServerInfoResource : BaseResponse
    {
        public string currentVersion { get; set; }
        public string fullVersion { get; set; }
        public string soapUrl { get; set; }
        public string secureSoapUrl { get; set; }
        public string owningSystemUrl { get; set; }
        public AuthInfo authInfo { get; set; }
    }
}
