//using System;
//using System.Collections.Generic;
//using System.Linq;

//using RestSharp;
//using Newtonsoft.Json;

//using ags_client.Operations.CatalogOps;
//using ags_client.Types;

//namespace ags_client
//{
//    public class Catalog
//    {
//        public int GetServiceLayerId(AgsClient client, string servicePath, string layerName)
//        {
//            // servicePath should include service type such as MapServer or FeatureServer. e.g. /IBIS/Operational/MapServer

//            var request = new RestRequest(String.Format("{0}/layers", servicePath));

//            var response = client.Execute<LayersResponse>(request, Method.GET);

//            if (response.error != null)
//            {
//                string msg = String.Format("Error. Code: {0}. {1}", response.error.code, response.error.message);
//                throw new ApplicationException(msg);
//            }

//            Table tbl = null;
//            if (response.layers != null)
//            {
//                var lyrs = response.layers.Where(x => x.name.Equals(layerName, StringComparison.CurrentCultureIgnoreCase));
//                if (lyrs.Count() > 1)
//                {
//                    //warn more than 1 layer with that name
//                }
//                tbl = lyrs.FirstOrDefault();
//                if (tbl == null)
//                {
//                    if (response.tables != null)
//                    {
//                        var tbls = response.tables.Where(x => x.name.Equals(layerName, StringComparison.CurrentCultureIgnoreCase));
//                        if (tbls.Count() > 1)
//                        {
//                            //warn more than 1 table with that name
//                        }
//                        tbl = tbls.FirstOrDefault();
//                    }
//                }
//            }
//            else
//            {
//                if (response.tables != null)
//                {
//                    var tbls = response.tables.Where(x => x.name.Equals(layerName, StringComparison.CurrentCultureIgnoreCase));
//                    if (tbls.Count() > 1)
//                    {
//                        //warn more than 1 table with that name
//                    }
//                    tbl = tbls.FirstOrDefault();
//                }
//            }
//            if (tbl == null)
//            {
//                string msg = String.Format("Layer or Table '{0}' was not found at the resource: '{1}'", layerName, request.Resource);
//                throw new ApplicationException(msg);
//            }
//            return tbl.id;
//        }
//    }
//}
