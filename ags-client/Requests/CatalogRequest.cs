using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using RestSharp;
using Newtonsoft.Json;

using ags_client.Types.Geometry;
using ags_client.Resources;

namespace ags_client.Requests
{
    public class CatalogRequest : BaseRequest
    {
        public string option { get; set; }
        public SpatialReference outSR { get; set; }

        private string _folder;

        public CatalogRequest(string folder)
        {
            _folder = folder;
        }

        public CatalogResource Execute(AgsClient client)
        {
            var request = new RestRequest(_folder);
            request.Method = Method.GET;

            var jss = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
            if (outSR != null)
                request.AddParameter("outSR", outSR.wkid);
            if (!String.IsNullOrWhiteSpace(option))
                request.AddParameter("option", option);

            var result = client.Execute<CatalogResource>(request, Method.GET);
            result.resourcePath = _folder;

            return result;
        }



    }
}
