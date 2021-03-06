﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace ags_client.Types.Geometry
{

    public class Polyline : IRestGeometry
    {
        public SpatialReference spatialReference { get; set; }

        [JsonProperty("paths")]
        private double[][][] _pathsArray;

        [JsonIgnore]
        public List<Path> Paths { get; set; }

        public double[][][] ToArray()
        {
            if (Paths == null)
                return null;

            Paths.RemoveAll(x => x == null);
            Paths.RemoveAll(x => x.Coordinates == null);
            Paths.RemoveAll(x => x.Coordinates.Count == 0);

            if (Paths.Count == 0)
                return null;


            double[][][] result = new double[Paths.Count][][];
            for (var i = 0; i < Paths.Count; i++)
            {
                result[i] = Paths.ElementAt(i).ToArray();
            }
            return result;
        }

        public string ToWkt()
        {
            if (Paths == null)
                return "LINESTRING EMPTY";
            if (Paths.Count == 0)
                return "LINESTRING EMPTY";
            if (Paths.Count == 1)
                return Paths[0].LineStringTaggedText(false);

            /*
            < multilinestring tagged text> ::= multilinestring < multilinestring text >
            */

            return $"MULTILINESTRING {multilinestringtext()}";
        }


        private string multilinestringtext()
        {
            /*
             < multilinestring text > ::=  < empty set > | 
                                            < left paren > < linestring text > {< comma > < linestring text >}* < right paren >
            */

            if (Paths == null)
                return "EMPTY";

            if (Paths.Count == 0)
                return "EMPTY";

            return $"({String.Join(",", Paths.Select(x => x.LineStringText(false)).ToArray())})";
        }



        [OnSerializing]
        internal void OnSerializing(StreamingContext context)
        {
            _pathsArray = ToArray();
        }

        [OnDeserialized]
        internal void OnDeserialized(StreamingContext context)
        {
            Paths = new List<Path>();
            foreach (var currentPathPointList in _pathsArray.Select(
                path => path.Select(
                    point => new Coordinate { x = point[0], y = point[1] }).ToList()))
            {
                Paths.Add(new Path { Coordinates = currentPathPointList });
            }

            if (Paths.Any(x => x == null)) { throw new InvalidOperationException("The collection may not contain a null path."); }
        }
    }
}
