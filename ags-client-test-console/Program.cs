using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ags_client;
using ags_client.Types;
using ags_client.Types.Geometry;
using ags_client.Operations;
using ags_client.Utility;

using Newtonsoft.Json;

namespace ags_client_test_console
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new AgsClient("http://agatstgis1.int.atco.com.au/arcgis/rest/services");

            var query = new LayerQueryOperation<VehicleF, Point, VehicleA>
            {
                where = "1=1",
                outFields = new List<string> { "*" }
            };

            var response = query.Execute(client, "NDV/NDVEditing", "MapServer", 2);

            var features = new List<VehicleF>();
            var updateOp = new UpdateFeaturesOperation<VehicleF, Point, VehicleA>
            {
                rollbackOnFailure = true
            };

            foreach (var f in response.features)
            {
                f.attributes.date_time = DateTime.Now;
                f.geometry.spatialReference = new SpatialReference { wkid = 28350 };
                //f.geometry.x += 20000; //shift them 20 km
                features.Add(f);

                if (features.Count > 54)
                    break;
            }

            updateOp.features = features;

            var updateResponse = updateOp.Execute2(client, "NDV/NDVEditing", 2);

            if (updateResponse != null)
            {
                if (updateResponse.error != null)
                    Console.WriteLine("Code: {0}. {1}", updateResponse.error.code, updateResponse.error.message);
                else
                    Console.WriteLine("success");
            }

            Console.ReadKey();

        }
    }


    public class VehicleF : IRestFeature<Point, VehicleA>
    {
        public Point geometry { get; set; }
        public VehicleA attributes { get; set; }
    }

    public class VehicleA : IRestAttributes
    {
        public int objectid { get; set; }
        public int? node_id { get; set; }

        [JsonConverter(typeof(DateTimeUnixConverter))]
        public DateTime? date_time { get; set; }
        public string description { get; set; }
        public string type_descr { get; set; }
    }
}
