using System;

namespace ags_client.Types.Geometry
{
    public enum GeometryTypes
    {
        Point,
        Multipoint,
        Polyline,
        Polygon,
        Envelope
    }

    public static class GeometryHelper
    {
        public const string NO_GEOMETRY = null;
        public const string POINT_GEOMETRY = "esriGeometryPoint";
        public const string MULTIPOINT_GEOMETRY = "esriGeometryMultipoint";
        public const string POLYLINE_GEOMETRY = "esriGeometryPolyline";
        public const string POLYGON_GEOMETRY = "esriGeometryPolygon";
        public const string ENVELOPE_GEOMETRY = "esriGeometryEnvelope";

        public static string GetGeometryTypeName(this GeometryTypes geometryType)
        {
            switch (geometryType)
            {
                case GeometryTypes.Point: { return POINT_GEOMETRY; }
                case GeometryTypes.Multipoint: { return MULTIPOINT_GEOMETRY; }
                case GeometryTypes.Polyline: { return POLYLINE_GEOMETRY; }
                case GeometryTypes.Polygon: { return POLYGON_GEOMETRY; }
                case GeometryTypes.Envelope: { return ENVELOPE_GEOMETRY; }
                default: { return null; }
            }
        }

        //public static GeometryTypes GetGeometryType(this string geometryTypeName)
        //{
        //    switch (geometryTypeName)
        //    {
        //        case POINT_GEOMETRY: { return GeometryTypes.Point; }
        //        case MULTIPOINT_GEOMETRY: { return GeometryTypes.Multipoint; }
        //        case POLYLINE_GEOMETRY: { return GeometryTypes.Polyline; }
        //        case POLYGON_GEOMETRY: { return GeometryTypes.Polygon; }
        //        case ENVELOPE_GEOMETRY: { return GeometryTypes.Envelope; }
        //        default: { return GeometryTypes.Point; }
        //    }
        //}

        //public static Type GetRestGeometryType(string esriGeometryType)
        //{
        //    switch (esriGeometryType)
        //    {
        //        case "esriGeometryPoint": return typeof(Point);
        //        case "esriGeometryMultiPoint": return typeof(MultiPoint);
        //        case "esriGeometryPolyline": return typeof(Polyline);
        //        case "esriGeometryPolygon": return typeof(Polygon);
        //        case "esriGeometryEnvelope": return typeof(Envelope);
        //        default: throw new Exception("Unsupported geometryType: " + esriGeometryType);
        //    }

        //}
    }
}
