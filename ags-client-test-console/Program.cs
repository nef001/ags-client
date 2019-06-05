using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ags_client;
using ags_client.Types;
using ags_client.Types.Geometry;
using ags_client.Operations.LayerOps;
using ags_client.Utility;

using Newtonsoft.Json;

namespace ags_client_test_console
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new AgsClient("http://agatstgis1.int.atco.com.au/arcgis/rest/services");

            var query = new LayerQueryOp<VehicleF, Point, VehicleA>
            {
                where = "1=1",
                outFields = new List<string> { "*" }
            };

            var response = query.Execute(client, "NDV/NDVEditing", "MapServer", 2);

            var featuresToUpdate = new List<VehicleF>();
            var featuresToAdd = new List<VehicleF>();

            var updateOp = new AddOrUpdateFeaturesOp<VehicleF, Point, VehicleA>
            {
                rollbackOnFailure = false
            };

            var addOp = new AddOrUpdateFeaturesOp<VehicleF, Point, VehicleA>
            {
                rollbackOnFailure = true
            };

            foreach (var f in response.features)
            {
                f.attributes.date_time = DateTime.Now;
                f.geometry.spatialReference = new SpatialReference { wkid = 28350 };
                featuresToUpdate.Add(f);


                featuresToAdd.Add(new VehicleF
                {
                    geometry = new Point { spatialReference = new SpatialReference { wkid = 28350 }, x = f.geometry.x + 2000, y = f.geometry.y },
                    attributes = new VehicleA { date_time = DateTime.Now, description = f.attributes.description, type_descr = f.attributes.type_descr, node_id = f.attributes.node_id }
                });

                //if (features.Count > 54)
                //    break;
            }
            
            updateOp.features = featuresToUpdate;
            addOp.features = featuresToAdd;

            var updateResponse = updateOp.Execute(client, "NDV/NDVEditing", 2, "updateFeatures");

            if (updateResponse != null)
            {
                if (updateResponse.error != null)
                    Console.WriteLine("Update error. Code: {0}. {1}", updateResponse.error.code, updateResponse.error.message);
                else
                {
                    Console.WriteLine("update operation success");

                    var errored = updateResponse.updateResults.Where(x => x.success == false).ToList();
                    if (errored.Count > 0)
                    {
                        Console.WriteLine("The following {0} of {1} updates failed:");
                        foreach ( var x in errored)
                        {
                            Console.WriteLine("objectid: {0}, Error code {1) {2}", x.objectId, x.error.code, x.error.description);
                        }
                    }
                }
                    
            }

            
            var addResponse = addOp.Execute(client, "NDV/NDVEditing", 2, "addFeatures");

            if (addResponse != null)
            {
                if (addResponse.error != null)
                    Console.WriteLine("Add error. Code: {0}. {1}", addResponse.error.code, addResponse.error.message);
                else
                {
                    Console.WriteLine("Add operation success");

                    var errored = addResponse.addResults.Where(x => x.success == false).ToList();
                    if (errored.Count > 0)
                    {
                        Console.WriteLine("The following {0} of {1} adds failed:");
                        foreach (var x in errored)
                        {
                            Console.WriteLine("objectid: {0}, Error code {1) {2}", x.objectId, x.error.code, x.error.description);
                        }
                    }
                }

            }

            /*
            var pgr = new Pager<VehicleF>(featuresToUpdate, 1);
            IEnumerable<VehicleF> page;
            while ((page = pgr.NextPage()) != null)
            {
                updateOp.features = page.ToList();

                Console.WriteLine(updateOp.features[0].attributes.objectid);

                var updateResponse = updateOp.Execute(client, "NDV/NDVEditing", 2, "updateFeatures");

                if (updateResponse != null)
                {
                    if (updateResponse.error != null)
                        Console.WriteLine("Code: {0}. {1}", updateResponse.error.code, updateResponse.error.message);
                    else
                        Console.WriteLine("success");
                }
            }*/



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
