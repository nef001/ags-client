using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using ags_client;
using ags_client.Types;
using ags_client.Types.Geometry;
using ags_client.Requests;
using ags_client.Requests.MapService;
using ags_client.Resources;

namespace ags_client_tests
{
    [TestClass]
    public class AgsClientTests
    {
        [TestMethod]
        public void SampleServerAnonymousHttpConnectionTest()
        {
            string host = "sampleserver1.arcgisonline.com";
            string instance = "arcgis";

            try
            {
                AgsClient agsClient = new AgsClient(host, instance, null, false, null, null);
                var resp = new ServerInfoRequest().Execute(agsClient);
                Assert.IsNull(resp.error);
            }
            catch(Exception ex)
            {
                Assert.Fail(ex.Message);
            }
            
        }

        [TestMethod]
        public void SampleServerCatalogTest()
        {
            string host = "sampleserver1.arcgisonline.com";
            string instance = "arcgis";

            try
            {
                AgsClient agsClient = new AgsClient(host, instance, null, false, null, null);
                var resp = new CatalogRequest(null).Execute(agsClient);
                Assert.IsNull(resp.error);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

        }

        [TestMethod]
        public void SampleServerFolderCatalogsTest()
        {
            string host = "sampleserver1.arcgisonline.com";
            string instance = "arcgis";

            try
            {
                AgsClient agsClient = new AgsClient(host, instance, null, false, null, null);
                var resp = new CatalogRequest(null).Execute(agsClient);
                Assert.IsNull(resp.error);
                foreach (var folder in resp.folders)
                {
                    var folderCatalog = new CatalogRequest(folder).Execute(agsClient);
                    Assert.IsNull(folderCatalog.error);
                }
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

        }

        [TestMethod]
        public void LayerQueryTest()
        {
            string host = "sampleserver1.arcgisonline.com";
            string instance = "arcgis";
            string folder = "Louisville";
            string mapServiceName = "LOJIC_LandRecords_Louisville";
            string layerName = "LandUse";
            string whereClause = "LANDUSE_CODE = 5";

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
                var queryResult = new LayerQueryRequest<LandUseF, Polygon, LandUseA>()
                {
                    outFields = "*",
                    where = whereClause
                }.Execute(agsClient, layer);
                Assert.IsNull(queryResult.error);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

        }

        
    }

    public class LandUseF : IRestFeature<Polygon, LandUseA>
    {
        public Polygon geometry { get; set; }
        public LandUseA attributes { get; set; }
    }

    public class LandUseA : IRestAttributes
    {
        public int? objectid { get; set; }
        public int landuse_code { get; set; }
        public string landuse_name { get; set; }

    }
}
