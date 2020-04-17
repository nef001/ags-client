using ags_client;
using ags_client.Requests;
using ags_client.Requests.GeocodeService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ags_client_tests
{
    [TestClass]
    public class GeocodeServiceTests
    {
        [TestMethod]
        public void GeocodeServiceRequestTest()
        {
            // http://sampleserver6.arcgisonline.com/arcgis/rest/services/Locators/SanDiego/GeocodeServer

            string host = "sampleserver6.arcgisonline.com";
            string instance = "arcgis";
            string folder = "Locators";
            string geocodeServiceName = "SanDiego";


            try
            {
                AgsClient agsClient = new AgsClient(host, instance, null, false, null, null);
                var folderCatalog = new CatalogRequest(folder).Execute(agsClient);
                Assert.IsNull(folderCatalog.error);
                var geocodeService = new GeocodeServiceRequest(geocodeServiceName).Execute(agsClient, folderCatalog);
                Assert.IsNull(geocodeService.error);

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}
