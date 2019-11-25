﻿using System;
using System.Threading.Tasks;
using RestSharp;
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
            string resourcePath = "rest/services";
            if (!String.IsNullOrWhiteSpace(_folder))
                resourcePath = String.Format("{0}/{1}", resourcePath, _folder);
            return (CatalogResource)Execute(client, resourcePath);
        }

        public async Task<CatalogResource> ExecuteAsync(AgsClient client)
        {
            string resourcePath = "rest/services";
            if (!String.IsNullOrWhiteSpace(_folder))
                resourcePath = String.Format("{0}/{1}", resourcePath, _folder);
            RestRequest request = createRequest(resourcePath);

            return await client.ExecuteAsync<CatalogResource>(request, Method.GET);
        }

        public override BaseResponse Execute(AgsClient client, string resourcePath)
        {
            var request = createRequest(resourcePath);
            return client.Execute<CatalogResource>(request, Method.GET);
        }


        private RestRequest createRequest(string resourcePath)
        {
            var request = new RestRequest(resourcePath) { Method = Method.GET };

            if (!String.IsNullOrWhiteSpace(option))
                request.AddParameter("option", option);
            if (outSR != null)
                request.AddParameter("outSR", outSR.wkid);

            request.AddParameter("f", "json");

            return request;
        }


    }
}
