using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ags_client;
using ags_client.Types;
using ags_client.Types.Geometry;
using ags_client.Operations.LayerOps;
using ags_client.Operations.GeometryOps;
using ags_client.JsonConverters;
using ags_client.Utility;

using Newtonsoft.Json;

namespace ags_client_test_console
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new AgsClient("http://agatstgis1.int.atco.com.au/arcgis/rest/services");

            var cat = new Catalog();

            int All_Vehicles_layerId = cat.GetServiceLayerId(client, "NDV/NDVEditing/MapServer", "All Vehicles");

            var query = new LayerQueryOp<VehicleF, Point, VehicleA>
            {
                where = "objectid <= 10",
                outFields = new List<string> { "*" }
            };

            var response = query.Execute(client, "NDV/NDVEditing", "MapServer", 2);

            var geom1 = response.features[0].geometry;
            var geom2 = response.features[1].geometry;

            var projectOp = new ProjectOp<Point>
            {
                geometries = new Geometries<Point> {
                    geometries = response.features.Select(x => x.geometry).ToList(),
                    geometryType = GeometryHelper.GetGeometryTypeName(GeometryTypes.Point)
                },
                inSR = new SpatialReference { wkid = 28350 },
                outSR = new SpatialReference { wkid = 4326 }
            };

            var projectResponse = projectOp.Execute(client, "Utilities/Geometry");
            if (projectResponse != null)
            { }

            var bufferOp = new BufferOp<Point>
            {
                geometries = new Geometries<Point>
                {
                    geometries = response.features.Select(x => x.geometry).ToList(),
                    geometryType = GeometryHelper.GetGeometryTypeName(GeometryTypes.Point)
                },
                inSR = new SpatialReference { wkid = 28350 },
                distances = new List<double> { 10.1, 20.67 }, //each input geom gets buffered by each distance
                unit = 9001, //esriSRUnit_Meter
                unionResults = false,
                geodesic = false
            };

            var bufferResponse = bufferOp.Execute(client, "Utilities/Geometry");
            if (bufferResponse != null)
            { }


            var areasAndLengthsOp = new AreasAndLengthsOp
            {
                polygons = bufferResponse.geometries,
                sr = new SpatialReference { wkid = 28350 },
                calculationType = "planar"
            };
            var areasAndLengthsResponse = areasAndLengthsOp.Execute(client, "Utilities/Geometry");
            if (areasAndLengthsResponse != null)
            { }

            var distanceOp = new DistanceOp<Point, Point>
            {
                geometry1 = new Geometry<Point>
                {
                    geometryType = GeometryHelper.GetGeometryTypeName(GeometryTypes.Point),
                    geometry = response.features[0].geometry
                },
                geometry2 = new Geometry<Point>
                {
                    geometryType = GeometryHelper.GetGeometryTypeName(GeometryTypes.Point),
                    geometry = response.features[1].geometry
                },
                sr = new SpatialReference { wkid = 28350 },
                distanceUnit = 9001, //esriSRUnit_Meter
                geodesic = false
            };

            var distanceResponse = distanceOp.Execute(client, "Utilities/Geometry");
            if (distanceResponse != null)
            { }

            var convexHullOp = new ConvexHullOp<Point>
            {
                geometries = new Geometries<Point>
                {
                    geometryType = GeometryHelper.GetGeometryTypeName(GeometryTypes.Point),
                    geometries = new List<Point> { response.features[0].geometry, response.features[1].geometry, response.features[2].geometry }
                },
                sr = new SpatialReference { wkid = 28350 }
            };
            var convexHullResponse = convexHullOp.Execute(client, "Utilities/Geometry");
            if (convexHullResponse != null)
            { }

            var q = new LayerQueryOp<CommonF<Polygon>, Polygon, CommonAttributes>
            {
                where = "pin in (1322607,11652291, 1322609)",
                outFields = new List<string> { "*" }
            };

            var qresponse = q.Execute(client, "Misc/MirnDiscovery", "MapServer", 1); //WA_CAD_POLY

            var diffOp = new DifferenceOp<Polygon, Polygon>
            {
                geometries = new Geometries<Polygon>
                {
                    geometries = qresponse.features.Select(x => x.geometry).ToList(),
                    geometryType = GeometryHelper.GetGeometryTypeName(GeometryTypes.Polygon),
                },
                geometry = new Geometry<Polygon>
                {
                    geometryType = GeometryHelper.GetGeometryTypeName(GeometryTypes.Polygon),
                    geometry = qresponse.features[0].geometry
                },
                sr = new SpatialReference { wkid = 28350 }
            };

            var diffResponse = diffOp.Execute(client, "Utilities/Geometry");
            if (diffResponse != null)
            { }

            //var deleteOp = new DeleteFeaturesOp()
            //{
            //    rollbackOnFailure = true
            //};

            //deleteOp.objectIds = response.features.Select(x=>x.attributes.objectid).ToList();

            //var deleteResponse = deleteOp.Execute(client, "NDV/NDVEditing", 2);
            //if (deleteResponse != null)
            //{ }

            //var featuresToUpdate = new List<VehicleF>();
            //var featuresToAdd = new List<VehicleF>();

            //var updateOp = new AddOrUpdateFeaturesOp<VehicleF, Point, VehicleA>
            //{
            //    rollbackOnFailure = false
            //};

            //var addOp = new AddOrUpdateFeaturesOp<VehicleF, Point, VehicleA>
            //{
            //    rollbackOnFailure = true
            //};



            //foreach (var f in response.features)
            //{
            //    f.attributes.date_time = DateTime.Now;
            //    f.geometry.spatialReference = new SpatialReference { wkid = 28350 };
            //    featuresToUpdate.Add(f);


            //    featuresToAdd.Add(new VehicleF
            //    {
            //        geometry = new Point { spatialReference = new SpatialReference { wkid = 28350 }, x = f.geometry.x + 2000, y = f.geometry.y },
            //        attributes = new VehicleA { date_time = DateTime.Now, description = f.attributes.description, type_descr = f.attributes.type_descr, node_id = f.attributes.node_id }
            //    });

            //    //if (features.Count > 54)
            //    //    break;
            //}

            //updateOp.features = featuresToUpdate;
            //addOp.features = featuresToAdd;


            //var updateResponse = updateOp.Execute(client, "NDV/NDVEditing", 2, "updateFeatures");

            //if (updateResponse != null)
            //{
            //    if (updateResponse.error != null)
            //        Console.WriteLine("Update error. Code: {0}. {1}", updateResponse.error.code, updateResponse.error.message);
            //    else
            //    {
            //        Console.WriteLine("update operation success");

            //        var errored = updateResponse.updateResults.Where(x => x.success == false).ToList();
            //        if (errored.Count > 0)
            //        {
            //            Console.WriteLine("The following {0} of {1} updates failed:");
            //            foreach ( var x in errored)
            //            {
            //                Console.WriteLine("objectid: {0}, Error code {1) {2}", x.objectId, x.error.code, x.error.description);
            //            }
            //        }
            //    }

            //}


            //var addResponse = addOp.Execute(client, "NDV/NDVEditing", 2, "addFeatures");

            //if (addResponse != null)
            //{
            //    if (addResponse.error != null)
            //        Console.WriteLine("Add error. Code: {0}. {1}", addResponse.error.code, addResponse.error.message);
            //    else
            //    {
            //        Console.WriteLine("Add operation success");

            //        var errored = addResponse.addResults.Where(x => x.success == false).ToList();
            //        if (errored.Count > 0)
            //        {
            //            Console.WriteLine("The following {0} of {1} adds failed:");
            //            foreach (var x in errored)
            //            {
            //                Console.WriteLine("objectid: {0}, Error code {1) {2}", x.objectId, x.error.code, x.error.description);
            //            }
            //        }

            //        var featuresToDelete = new List<VehicleF>();
            //        List<int> objectIdsToDelete = addResponse.addResults.Where(x => x.success == true).Select(q => q.objectId.Value).ToList();
            //        foreach (int oid in objectIdsToDelete)
            //        {
            //            featuresToDelete.Add(new VehicleF
            //            {
            //                attributes = new VehicleA { objectid = oid }
            //            });

            //        }

            //        deleteOp.features = featuresToDelete;
            //        var deleteResponse = deleteOp.Execute(client, "NDV/NDVEditing", 2, "deleteFeatures");
            //        if (deleteResponse != null)
            //        { }
            //    }

            //}

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
