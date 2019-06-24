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
            string resourcePath = (_folder == null) ? String.Empty : _folder;
            return (CatalogResource)Execute(client, resourcePath);
        }

        public override BaseResponse Execute(AgsClient client, string resourcePath)
        {
            var request = new RestRequest(resourcePath) { Method = Method.GET };

            if (!String.IsNullOrWhiteSpace(option))
                request.AddParameter("option", option);
            if (outSR != null)
                request.AddParameter("outSR", outSR.wkid);

            var result = client.Execute<CatalogResource>(request, Method.GET);
            result.resourcePath = _folder;

            return result;
        }



    }
}
