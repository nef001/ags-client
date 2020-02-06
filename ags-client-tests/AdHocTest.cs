using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using ags_client;
using ags_client.Types.Geometry;
using ags_client.Requests;
using ags_client.Requests.MapService;

using ags_client_tests.Models;

namespace ags_client_tests
{
    [TestClass]
    public class AdHocTest
    {
        [TestMethod]
        public void LayerQueryTest()
        {
            string host = "agaprdgisags1.int.atco.com.au";
            string instance = "arcgis";
            string folder = "IBIS";
            string mapServiceName = "LandBase";
            string layerName = "Centroid";
            string whereClause = "TAG = '12374071'";

            try
            {
                AgsClient agsClient = new AgsClient(host, instance, null, false, null, null);
                var folderCatalog = new CatalogRequest(folder).Execute(agsClient);
                Assert.IsNull(folderCatalog.error);
                var mapService = new MapServiceRequest(mapServiceName).Execute(agsClient, folderCatalog);
                Assert.IsNull(mapService.error);
                int layerId = mapService.LayerIdByName(layerName);
                var layer = new LayerOrTableRequest(layerId).Execute(agsClient, mapService);
                Assert.IsNull(layer.error);
                var queryResult = new LayerQueryRequest<Centroid, Point, CentroidA>()
                {
                    outFields = "*",
                    where = whereClause
                }.Execute(agsClient, layer);
                Assert.IsNull(queryResult.error);
                Assert.IsTrue(queryResult.features.Count == 1);
                Assert.IsTrue(queryResult.features[0].attributes.pin == 1367349);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

        }
    }
}
