﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ags_client;
using ags_client.Types;
using ags_client.Types.Geometry;
using ags_client.JsonConverters;
using ags_client.Utility;
using ags_client.Requests;
using ags_client.Requests.MapService;
using ags_client.Requests.GeometryService;
using ags_client.Requests.FeatureService;

using Newtonsoft.Json;

namespace ags_client_test_console
{
    class Program
    {

        static async Task Main(string[] args)
        {

            double? x = null;
            object o = null;
            var c0 = new Coordinate { x = 5, y = 7 };
            Console.WriteLine($"value = {c0}");

            var c1 = new Coordinate { x = 1, y = -2 };
            var path0 = new Path { Coordinates = new List<Coordinate> { c0, c1, null } };
            var emptyPath = new Path();
            var emptyPath2 = new Path { Coordinates = new List<Coordinate>() };
            Console.WriteLine($"path0 = {path0}");

           
            var emptypolyline = new Polyline();

            Console.WriteLine($"emptypolyline = {emptypolyline}");

            var emptypolyline2 = new Polyline { Paths = new List<Path>() };
            Console.WriteLine($"emptypolyline2 = {emptypolyline2}");

            var polylinebasic = new Polyline { Paths = new List<Path> { path0 } };
            Console.WriteLine($"polylinebasic = {polylinebasic}");

            var polyline1 = new Polyline { Paths = new List<Path> { path0, emptyPath, emptyPath2 } };
            Console.WriteLine($"polyline1 = {polyline1}");

            var polyline2 = new Polyline { Paths = new List<Path> { path0, emptyPath, path0 } };
            Console.WriteLine($"polyline2 = {polyline2}");

            var p0 = new Point { x = 5, y = 7 };
            var p1 = new Point { x = 1, y = -2 };

            Console.WriteLine($"Point p0 = {p0}");

            var multipoint = new MultiPoint { Coordinates = new List<Coordinate> { p0, p1 } };
            Console.WriteLine($"multipoint = {multipoint}");

            

            //"POLYGON((-122.358 47.653, -122.348 47.649, -122.348 47.658, -122.358 47.658, -122.358 47.653))",

            var polygon = new Polygon
            {
                Rings = new List<Path>
                {
                    new Path
                    {
                        Coordinates = new List<Coordinate>
                        {
                            new Coordinate { x = -122.358, y = 47.653 },
                            new Coordinate { x = -122.348, y = 47.649 },
                            new Coordinate { x = -122.348, y = 47.658 },
                            new Coordinate { x = -122.358, y = 47.658 },
                            new Coordinate { x = -122.358, y = 47.653 },
                        }
                    },
                }
            };
            Console.WriteLine($"polygon = {polygon}");

            //"MULTILINESTRING((-122.358 47.653, -122.348 47.649, -122.348 47.658), (-122.357 47.654, -122.357 47.657, -122.349 47.657, -122.349 47.650))",

            var polyline = new Polyline
            {
                Paths = new List<Path>
                {
                    new Path(),
                    new Path
                    {
                        Coordinates = new List<Coordinate>
                        {
                            new Coordinate { x = -122.358, y = 47.653 },
                            new Coordinate { x = -122.348, y = 47.649 },
                            new Coordinate { x = -122.348, y = 47.658 },
                            
                        }
                    },
                    new Path
                    {
                        Coordinates = new List<Coordinate>
                        {
                            new Coordinate { x = -122.357, y = 47.654 },
                            new Coordinate { x = -122.357, y = 47.657 },
                            new Coordinate { x = -122.349, y = 47.657 },
                            new Coordinate { x = -122.349, y = 47.650 },
                        }
                    }
                }
            };

            Console.WriteLine($"polyline = {polyline}");

            var polygon2 = new Polygon
            {
                Rings = new List<Path>
                {
                    new Path
                    {
                        Coordinates = new List<Coordinate>
                        {
                            new Coordinate { x = 40, y = 40 },
                            new Coordinate { x = 45, y = 30 },
                            new Coordinate { x = 20, y = 45 },
                            new Coordinate { x = 40, y = 40 },
                            
                        }
                    },
                    new Path
                    {
                        Coordinates = new List<Coordinate>
                        {
                            new Coordinate { x = 20, y = 35 },
                            new Coordinate { x = 45, y = 20 },
                            new Coordinate { x = 30, y = 5 },
                            new Coordinate { x = 10, y = 10 },
                            new Coordinate { x = 10, y = 30 },
                            new Coordinate { x = 20, y = 35 },
                        }
                    },
                    new Path
                    {
                        Coordinates = new List<Coordinate>
                        {
                            new Coordinate { x = 30, y = 20 },
                            new Coordinate { x = 20, y = 25 },
                            new Coordinate { x = 20, y = 15 },
                            new Coordinate { x = 30, y = 20 },
                        }
                    }
                }
            };
            Console.WriteLine($"polygon2 = {polygon2}");


            Ssl.EnableTrustedHosts();

            var client = new AgsClient("agatstgis1.int.atco.com.au", "arcgis", 6443, true, "agm_test", "agm_test123");

            var serverInfo = new ServerInfoRequest().Execute(client);

            var cat1 = new CatalogRequest(null).Execute(client);

            var cat2 = new CatalogRequest("Utilities").Execute(client);

            var gs = new GeometryServiceRequest("Geometry").Execute(client, cat2);

            var gs2 = new GeometryServiceRequest("Geometry").Execute(client, "rest/services/Utilities/Geometry/GeometryServer");

            var cat3 = new CatalogRequest("NDV").Execute(client);

            var fs1 = new FeatureServiceRequest("NDVEditing").Execute(client, cat3);

            var flayer = new FeatureServiceLayerRequest<VehicleA>(2).Execute(client, fs1);

            var flayerquery = new FeatureServiceLayerQueryRequest<VehicleF, Point, VehicleA>()
            {
                where = "objectid <= 10",
                outFields = "*"
            };
            var r = flayerquery.Execute(client, flayer);

            //var feat = new FeatureServiceLayerFeatureRequest<VehicleF, Point, VehicleA>(10).Execute(client, flayer);

            //var geom = r.features[0].geometry;

            //var projectReq = new ProjectRequest<Point>()
            //{
            //    geometries = new Geometries<Point>
            //    {
            //        geometries = r.features.Select(x => x.geometry).ToList(),
            //        geometryType = GeometryHelper.GetGeometryTypeName(GeometryTypes.Point)
            //    },
            //    inSR = new SpatialReference { wkid = 28350 },
            //    outSR = new SpatialReference { wkid = 4326 }
            //};

            //var z = await projectReq.ExecuteAsync(client, gs);

            //if (z != null)
            //{

            //}

            //int All_Vehicles_layerId = cat.GetServiceLayerId(client, "NDV/NDVEditing/MapServer", "All Vehicles");

            //var query = new LayerQueryOp<VehicleF, Point, VehicleA>
            //{
            //    where = "objectid <= 10",
            //    outFields = new List<string> { "*" }
            //};

            //var response = query.Execute(client, "NDV/NDVEditing", "MapServer", 2);

            //var geom1 = r.features[0].geometry;
            //var geom2 = r.features[1].geometry;

            //var projectOp = new ProjectOp<Point>
            //{
            //    geometries = new Geometries<Point> {
            //        geometries = r.features.Select(x => x.geometry).ToList(),
            //        geometryType = GeometryHelper.GetGeometryTypeName(GeometryTypes.Point)
            //    },
            //    inSR = new SpatialReference { wkid = 28350 },
            //    outSR = new SpatialReference { wkid = 4326 }
            //};

            //var projectResponse = projectOp.Execute(client, "Utilities/Geometry");
            //if (projectResponse != null)
            //{ }

            //var bufferOp = new BufferOp<Point>
            //{
            //    geometries = new Geometries<Point>
            //    {
            //        geometries = r.features.Select(x => x.geometry).ToList(),
            //        geometryType = GeometryHelper.GetGeometryTypeName(GeometryTypes.Point)
            //    },
            //    inSR = new SpatialReference { wkid = 28350 },
            //    distances = new List<double> { 10.1, 20.67 }, //each input geom gets buffered by each distance
            //    unit = 9001, //esriSRUnit_Meter
            //    unionResults = false,
            //    geodesic = false
            //};

            //var bufferResponse = bufferOp.Execute(client, "Utilities/Geometry");
            //if (bufferResponse != null)
            //{ }


            //var areasAndLengthsOp = new AreasAndLengthsOp
            //{
            //    polygons = bufferResponse.geometries,
            //    sr = new SpatialReference { wkid = 28350 },
            //    calculationType = "planar"
            //};
            //var areasAndLengthsResponse = areasAndLengthsOp.Execute(client, "Utilities/Geometry");
            //if (areasAndLengthsResponse != null)
            //{ }

            //var distanceOp = new DistanceOp<Point, Point>
            //{
            //    geometry1 = new Geometry<Point>
            //    {
            //        geometryType = GeometryHelper.GetGeometryTypeName(GeometryTypes.Point),
            //        geometry = r.features[0].geometry
            //    },
            //    geometry2 = new Geometry<Point>
            //    {
            //        geometryType = GeometryHelper.GetGeometryTypeName(GeometryTypes.Point),
            //        geometry = r.features[1].geometry
            //    },
            //    sr = new SpatialReference { wkid = 28350 },
            //    distanceUnit = 9001, //esriSRUnit_Meter
            //    geodesic = false
            //};

            //var distanceResponse = distanceOp.Execute(client, "Utilities/Geometry");
            //if (distanceResponse != null)
            //{ }

            //var convexHullOp = new ConvexHullOp<Point>
            //{
            //    geometries = new Geometries<Point>
            //    {
            //        geometryType = GeometryHelper.GetGeometryTypeName(GeometryTypes.Point),
            //        geometries = new List<Point> { r.features[0].geometry, r.features[1].geometry, r.features[2].geometry }
            //    },
            //    sr = new SpatialReference { wkid = 28350 }
            //};
            //var convexHullResponse = convexHullOp.Execute(client, "Utilities/Geometry");
            //if (convexHullResponse != null)
            //{ }

            //var q = new LayerQueryOp<CommonF<Polygon>, Polygon, CommonAttributes>
            //{
            //    where = "pin in (1322607,11652291, 1322609)",
            //    outFields = new List<string> { "*" }
            //};

            //var qresponse = q.Execute(client, "Misc/MirnDiscovery", "MapServer", 1); //WA_CAD_POLY

            //var diffOp = new DifferenceOp<Polygon, Polygon>
            //{
            //    geometries = new Geometries<Polygon>
            //    {
            //        geometries = qresponse.features.Select(x => x.geometry).ToList(),
            //        geometryType = GeometryHelper.GetGeometryTypeName(GeometryTypes.Polygon),
            //    },
            //    geometry = new Geometry<Polygon>
            //    {
            //        geometryType = GeometryHelper.GetGeometryTypeName(GeometryTypes.Polygon),
            //        geometry = qresponse.features[0].geometry
            //    },
            //    sr = new SpatialReference { wkid = 28350 }
            //};

            //var diffResponse = diffOp.Execute(client, "Utilities/Geometry");
            //if (diffResponse != null)
            //{ }

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
        public int? objectid { get; set; }
        public int? node_id { get; set; }

        [JsonConverter(typeof(DateTimeUnixConverter))]
        public DateTime? date_time { get; set; }
        public string description { get; set; }
        public string type_descr { get; set; }
        
    }
}
