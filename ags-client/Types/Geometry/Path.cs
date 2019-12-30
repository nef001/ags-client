using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

using ags_client.Algorithms;

namespace ags_client.Types.Geometry
{

    public class Path
    {
        [JsonIgnore]
        public List<Coordinate> Coordinates
        {
            get; set;
        }

        [JsonIgnore]
        public List<Segment> Segments
        {
            get
            {
                var result = new List<Segment>();
                if ((Coordinates == null) || (Coordinates.Count == 0))
                    return result;

                for (int i = 0; i < Coordinates.Count - 1; i++)
                    result.Add(new Segment(Coordinates[i], Coordinates[i + 1]));

                return result;
            }
        }

        public double[][] ToArray()
        {
            if (Coordinates == null)
                return null;

            if (Coordinates.Count == 0)
                return null;

            double[][] result = new double[Coordinates.Count][];
            for (var i = 0; i < Coordinates.Count; i++)
            {
                result[i] = Coordinates.ElementAt(i).ToArray();
            }
            return result;
        }

        public bool IsEmptyPath()
        {
            return nonEmptyCoordinates().Count == 0;
        }

        public bool IsEmptyRing()
        {
            return nonEmptyCoordinates().Count < 2;
        }

        public bool IsSimple()
        {
            if (IsEmptyPath())
                return true;
            var segments = Segments;
            if (segments.Count <= 1)
                return true;
            var sh = new ShamosHoey(segments);
            if (sh.HasIntersections())
                return false;
            return true;
        }

        public bool IsSimpleRing()
        {
            if (!IsClosedRing())
                return false;
            if (SignedArea() == 0)
                return false;
            return IsSimple();

        }

        public bool IsInteriorTo(Path other)
        {
            if (other == null)
                return false;
            if (!other.IsSimpleRing())
                return false;

            foreach (var p in Coordinates)
            {
                if (other.ContainsPoint(p) != 1)
                    return false;
            }
            return true;
        }


        /// <summary>
        /// A closed ring has at least 2 non null coordinates where the first equals the last
        /// </summary>
        /// <returns></returns>
        public bool IsClosedRing()
        {
            var nonEmpty = nonEmptyCoordinates();
            return (nonEmpty.Count > 1) && nonEmpty.First().Equals(nonEmpty.Last());
        }



        public string LineStringText(bool reversed)
        {
            /*
            < linestring text > ::= < empty set > | < left paren > 
                                    < point > 
                                    {< comma > < point >}* 
                                    < right paren >
            */

            List<Coordinate> coords = Coordinates;
            if (reversed) coords = reverseCoordinates();
            if (coords == null)
                return "EMPTY";
            coords.RemoveAll(x => x == null);
            if (coords.Count == 0)
                return "EMPTY";
            
            return $"({String.Join(",", coords.Select(x => x.PointString()).ToArray())})";
        }

        public string LineStringTaggedText(bool reversed)
        {
            /*
             <linestring tagged text> ::= linestring <linestring text>
             */

            return $"LINESTRING {LineStringText(reversed)}";
        }

        private List<Coordinate> reverseCoordinates()
        {
            if (Coordinates == null)
                return null;

            if (Coordinates.Count < 2)
                return Coordinates;

            return Coordinates.AsEnumerable().Reverse().ToList();
        }

        /// <summary>
        /// Determines ring direction of the non empty coordinates
        /// </summary>
        /// <remarks>
        /// The Esri convention is that clockwise is an exterior ring, and interior counter-clockwise. 
        /// OGC wkt convention is the opposite.
        /// </remarks>
        /// <returns>0 if the ring is empty or not a closed ring, >0 if the ring is anti-clockwise, <0 if the ring is clockwise.</returns>
        public double SignedArea()
        {
            if (IsClosedRing() == false)
                return 0;

            var nonEmptyCoords = nonEmptyCoordinates();

            double sum = 0;

            for (int i = 0; i < nonEmptyCoords.Count - 1; i++)
            {
                double x1 = nonEmptyCoords[i].x;
                double y1 = nonEmptyCoords[i].y;

                double x2, y2;

                if (i == nonEmptyCoords.Count - 2)
                {
                    x2 = nonEmptyCoords[0].x;
                    y2 = nonEmptyCoords[0].y;
                }
                else
                {
                    x2 = nonEmptyCoords[i + 1].x;
                    y2 = nonEmptyCoords[i + 1].y;
                }

                sum += ((x1 * y2) - (x2 * y1));
            }
            return sum;
        }


        

        public Path NonEmptyCoordinates()
        {
            return new Path { Coordinates = nonEmptyCoordinates() };
        }

        private List<Coordinate> nonEmptyCoordinates()
        {
            if ((Coordinates == null) || (Coordinates.Count == 0))
                return new List<Coordinate>();
            return
                Coordinates.Where(c => c.IsEmpty() == false).ToList();
        }

        

        /// <summary>
        /// Determinas if the supplied coordinate is inside (1), outside (0) or on the boundary (-1)
        /// of the path.
        /// </summary>
        /// <param name="p">The coordinate to test</param>
        /// <returns>1 = inside, -1 = on the boundary, 0 = outside</returns>
        public int ContainsPoint(Coordinate p)
        {
            /*
             * Algorithm 1 from https://www.mdpi.com/2073-8994/10/10/477
             * 
             * Optimal Reliable Point-in-Polygon Test and Differential Coding Boolean Operations on Polygons
             * Jianqiang Hao, Jianzhi Sun, Yi Chen, Qiang Cai and Li Tan 
             * Symmetry 2018, 10(10), 477; https://doi.org/10.3390/sym10100477 
             * 
             * © 2018 by the authors. 
             * Licensee MDPI, Basel, Switzerland. 
             * 
             * This article is an open access article distributed under the 
             * terms and conditions of the Creative Commons Attribution
             * (CC BY) license (http://creativecommons.org/licenses/by/4.0/).
             */


            int k = 0;
            
            for (int i = 0; i < Coordinates.Count - 1; i++)
            {
                var c1 = Coordinates[i];
                var c2 = Coordinates[i + 1];
                double v1 = c1.y - p.y;
                double v2 = c2.y - p.y;
                if ((v1 < 0 && v2 < 0) || (v1 > 0 && v2 > 0))   // Case 11 or 26 
                {
                    continue;
                }
                double u1 = c1.x - p.x;
                double u2 = c2.x - p.x;
                double f = (u1 * v2) - (u2 * v1);
                if ((v2 > 0) && (v1 <= 0)) // Case 3, 9, 16, 21, 13, or 24 
                {
                    if (f > 0) // Case 3 or 9 
                    {
                        k++; // Handle Case 3 or 9 
                    }
                    else if (f == 0) // Case 16 or 21.The rest are Case 13 or 24 
                    {
                        return -1; // Handle Case 16 or 21 
                    }
                }
                else if ((v1 > 0) && (v2 <= 0)) // Case 4, 10, 19, 20, 12, or 25 
                {
                    if (f < 0) //Case 4 or 10
                    {
                        k++; // Handle Case 4 or 10
                    }
                    else if (f == 0) // Case 19 or 20.The rest are Case 12 or 25  
                    {
                        return -1; // Handle Case 19 or 20 
                    }
                }
                else if ((v2 == 0) && (v1< 0)) // Case 7, 14, or 17 
                {
                    if (f == 0)
                    {
                        return -1; // Case 17.The rest are Case 7 or 14 
                    }
                }
                else if ((v1 == 0) && (v2 < 0)) // Case 8, 15, or 18
                {
                    if (f == 0)
                    {
                        return -1; // Case 18.The rest are Case 8 or 15 
                    }
                }
                else if ((v1 == 0) && (v2 == 0)) // Case 1, 2, 5, 6, 22, or 23
                {
                    if ((u2 <= 0) && (u1 >= 0)) // Case 1
                    {
                        return -1; // Handle Case 1 
                    }
                    else if ((u1 <= 0) && (u2 >= 0)) // Case 2.The rest are Case 5, 6, 22, or 23 
                    {
                        return -1; // Handle Case 2 
                    }
                }
            }
            if (k % 2 == 0)
                return 0;
            else
                return 1;
        }

    }
}
