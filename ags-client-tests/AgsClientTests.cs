using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using ags_client;
using ags_client.Requests;
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
    }
}
