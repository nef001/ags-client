using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Security;

namespace ags_client.Utility
{
    public static class Ssl
    {
        private static readonly string[] TrustedHosts = new[] {
          "agadevgis1.int.atco.com.au",
          "agatstgis1.int.atco.com.au",
          "agaprdags1.int.atco.com.au"
        };

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
