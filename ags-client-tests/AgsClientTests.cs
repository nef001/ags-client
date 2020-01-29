﻿using System;
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
    }
}
