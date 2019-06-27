﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RestSharp;
using ags_client.Resources.FeatureService;
using ags_client.Types;

namespace ags_client.Requests.FeatureService
{
    public class FeatureServiceLayerRequest<TA> : BaseRequest
        where TA : IRestAttributes
    {
        private int _layerId;
        public bool? returnUpdates { get; set; }

        public FeatureServiceLayerRequest(int layerId)
        {
            _layerId = layerId;
        }

        public FeatureServiceLayerResource<TA> Execute(AgsClient client, FeatureServiceResource parent) //parent may be the root catalog or a folder catalog
        {
            string resourcePath = String.Format("{0}/{1}", parent.resourcePath, _layerId);
            return (FeatureServiceLayerResource < TA > )Execute(client, resourcePath);
        }

        public async Task<FeatureServiceLayerResource<TA>> ExecuteAsync(AgsClient client, FeatureServiceResource parent)
        {
            string resourcePath = String.Format("{0}/{1}", parent.resourcePath, _layerId);
            var request = createRequest(resourcePath);

            return await client.ExecuteAsync<FeatureServiceLayerResource<TA>>(request, Method.POST);
        }

        public override BaseResponse Execute(AgsClient client, string resourcePath)
        {
            var request = createRequest(resourcePath);
            var result = client.Execute<FeatureServiceLayerResource<TA>>(request, Method.POST);

            return result;
        }

        private RestRequest createRequest(string resourcePath)
        {
            var request = new RestRequest(resourcePath) { Method = Method.POST };

            if (returnUpdates.HasValue)
                request.AddParameter("returnUpdates", returnUpdates.Value ? "true" : "false");

            return request;
        }
    }
}
