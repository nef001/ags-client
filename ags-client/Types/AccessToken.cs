using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ags_client.Types
{
    public class AccessToken
    {
        public string access_token { get; set; }
        public int? expires_in { get; set; }
    }
}
