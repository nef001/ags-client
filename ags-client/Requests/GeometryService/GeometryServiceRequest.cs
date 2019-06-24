﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RestSharp;
using Newtonsoft.Json;
using ags_client.Resources;
using ags_client.Resources.GeometryService;

namespace ags_client.Requests.GeometryService
{
    public class GeometryServiceRequest : BaseRequest
    {
        private string _serviceName;

        public GeometryServiceRequest(string serviceName) //typically Geometry
        {
            _serviceName = serviceName;
        }

        public GeometryServiceResource Execute(AgsClient client, CatalogResource parent) //parent is typically the Utilities folder
        {
            string resourcePath = String.Format("{0}/{1}/GeometryServer", parent.resourcePath, _serviceName);
            return Execute(client, resourcePath);
        }

        public GeometryServiceResource Execute(AgsClient client, string resourcePath) //this overload takes the absolute path - typically Utilities/Geometry/GeometryServer 
        {
            var request = new RestRequest(resourcePath);
            request.Method = Method.GET;

            var result = client.Execute<GeometryServiceResource>(request, Method.GET);
            result.resourcePath = resourcePath;

            return result;
        }
    }
}
