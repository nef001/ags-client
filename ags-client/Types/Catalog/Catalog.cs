using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ags_client.Types.Catalog
{
    public class Catalog
    {
        public string currentVersion { get; set; }
        public List<string> folders { get; set; }
        public List<Service> services { get; set; }

        public Catalog() { }

        public Catalog(AgsClient client, string folder)
        {
            var request = new RestSharp.RestRequest(folder);
            var cat = client.Execute<Catalog>(request, RestSharp.Method.GET);
            currentVersion = cat.currentVersion;
            folders = cat.folders;
            services = cat.services;
        }
    }
}
