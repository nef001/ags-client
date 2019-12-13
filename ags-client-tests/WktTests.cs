using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using ags_client.Types;
using ags_client.Types.Geometry;

namespace ags_client_tests
{
    [TestClass]
    public class WktTests
    {
        [TestMethod]
        public void TestEmptyPointToWkt()
        {
            var g = new Point();
            string expected = "POINT (0 0)";
            string actual = g.ToWkt();
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void TestPointToWkt()
        {
            var g = new Point { x = 115.860458, y = -31.950527 };
            string expected = "POINT (115.860458 -31.950527)";
            string actual = g.ToWkt();
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void TestEmptyPolylineWithNullPaths()
        {
            var g = new Polyline();
            string expected = "LINESTRING EMPTY";
            string actual = g.ToWkt();
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void TestEmptyPolylineWith0Paths()
        {
            var g = new Polyline { Paths = new List<Path>() };
            string expected = "LINESTRING EMPTY";
            string actual = g.ToWkt();
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void TestEmptyPolylineWith1EmptyPath()
        {
            var g = new Polyline { Paths = new List<Path> { new Path() } };
            string expected = "LINESTRING EMPTY";
            string actual = g.ToWkt();
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void TestEmptyPolylineWith2EmptyPaths()
        {
            var g = new Polyline { Paths = new List<Path> { new Path(), new Path() } };
            string expected = "MULTILINESTRING (EMPTY,EMPTY)";
            string actual = g.ToWkt();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test1PathPolylineWithCoords()
        {
            var g = new Polyline 
            { Paths = new List<Path> { new Path { Coordinates = new List<Coordinate> { new Coordinate(), new Coordinate() } } } };
            string expected = "LINESTRING (0 0,0 0)";
            string actual = g.ToWkt();
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void Test1PathPolylinePoints()
        {
            var g = new Polyline
            { Paths = new List<Path> { new Path { Coordinates = new List<Coordinate> { new Point(), new Point() } } } };
            string expected = "LINESTRING (0 0,0 0)";
            string actual = g.ToWkt();
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void TestPolylineWith2Paths()
        {
            var g = new Polyline
            { Paths = new List<Path> 
            { 
                new Path { Coordinates = new List<Coordinate> { new Point(), new Point() } },
                new Path { Coordinates = new List<Coordinate> { new Point(), new Point() } },
            } };
            string expected = "MULTILINESTRING ((0 0,0 0),(0 0,0 0))";
            string actual = g.ToWkt();
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void TestPolylineWith2Paths1Empty()
        {
            var g = new Polyline
            {
                Paths = new List<Path>
                {
                    new Path { Coordinates = new List<Coordinate> { new Point(), new Point() } },
                    new Path () 
                }
            };
            string expected = "MULTILINESTRING ((0 0,0 0),EMPTY)";
            string actual = g.ToWkt();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestEmptyPolygon()
        {
            var g = new Polygon();
            string expected = "POLYGON EMPTY";
            string actual = g.ToWkt();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestSingleRingPolygon()
        {
            var g = new Polygon
            {
                Rings = new List<Path>
                {
                    new Path
                    {
                        Coordinates = new List<Coordinate>
                        {
                            new Point{ x=0, y=0 },
                            new Point{ x=0, y=0 },
                            new Point{ x=0, y=0 },
                            new Point{ x=0, y=0 }
                        }
                    }
                }
            };
            string expected = "POLYGON ((0 0,0 0,0 0,0 0))";
            string actual = g.ToWkt();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestSingleRingWithExteriorAndInteriorPolygon()
        {
            var g = new Polygon
            {
                Rings = new List<Path>
                {
                    new Path
                    {
                        Coordinates = new List<Coordinate>
                        {
                            new Coordinate { x = 1, y = 1 },
                            new Coordinate { x = 2, y = 5 },
                            new Coordinate { x = 4, y = 2 },
                            new Coordinate { x = 1, y = 1 },
                        }
                    },
                    new Path
                    {
                        Coordinates = new List<Coordinate>
                        {
                            new Coordinate { x = 2, y = 2 },
                            new Coordinate { x = 3, y = 2 },
                            new Coordinate { x = 3, y = 3 },
                            new Coordinate { x = 2, y = 2 },
                        }
                    }
                }
            };
            string expected = "POLYGON ((1 1,4 2,2 5,1 1),(2 2,3 3,3 2,2 2))";
            string actual = g.ToWkt();
            string actual2 = g.ToWkt();
            Assert.AreEqual(expected, actual);
            Assert.AreEqual(expected, actual2);
        }

    }
}
