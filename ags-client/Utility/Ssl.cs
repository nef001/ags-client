using System.Linq;
using System.Net;
using System.Net.Security;

namespace ags_client.Utility
{
    public static class Ssl
    {
        private static readonly string[] TrustedHosts = new[] { "localhost" };

        public static void EnableTrustedHosts()
        {
            ServicePointManager.ServerCertificateValidationCallback =
            (sender, certificate, chain, errors) =>
            {
                if (errors == SslPolicyErrors.None)
                {
                    return true;
                }

                var request = sender as HttpWebRequest;
                if (request != null)
                {
                    return TrustedHosts.Contains(request.RequestUri.Host);
                }

                return false;
            };
        }
    }
}
