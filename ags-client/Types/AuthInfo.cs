using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ags_client.Types
{
    public class AuthInfo
    {
        public bool isTokenBasedSecurity { get; set; }
        public string tokenServicesUrl { get; set; }
        public int shortLivedTokenValidity { get; set; }

    }
}
