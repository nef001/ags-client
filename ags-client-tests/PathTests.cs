using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using ags_client.Types;
using ags_client.Types.Geometry;

namespace ags_client_tests
{
    [TestClass]
    public class PathTests
    {
        [TestMethod]
        public void TestPath_OrientPath()
        {
            var g = new Path
            {
                Coordinates = new List<Coordinate>
                {
                    new Coordinate{x = 2, y = 1 },
                    new Coordinate{x = 5, y = 4 },
                    new Coordinate{x = 6, y = 2 },
                    new Coordinate{x = 3, y = 3 },
                    new Coordinate{x = 2, y = 1 }
                }
            };
            Path ordered = new Path { Coordinates = g.OrientPath() };
            string expected = "(2 1,6 2,5 4,3 3,2 1)";
            string actual = ordered.LineStringText(false);
            Assert.AreEqual(expected, actual);
            Assert.IsTrue(ordered.SignedArea() > 0);
        }

        [TestMethod]
        public void TestPath_OrientPath2()
        {
            var g = new Path
            {
                Coordinates = new List<Coordinate>
                {
                    new Coordinate{x = 5, y = 4 },
                    new Coordinate{x = 6, y = 2 },
                    new Coordinate{x = 3, y = 3 },
                    new Coordinate{x = 2, y = 1 },
                    new Coordinate{x = 2, y = 1 },
                }
            };
            Path ordered = new Path { Coordinates = g.OrientPath() };
            string expected = "(2 1,6 2,5 4,3 3,2 1)";
            string actual = ordered.LineStringText(false);
            Assert.AreEqual(expected, actual);
            Assert.IsTrue(ordered.SignedArea() > 0);
        }

        [TestMethod]
        public void TestPath_OrientPath3()
        {
            var g = new Path
            {
                Coordinates = new List<Coordinate>
                {
                    new Coordinate{x = 2, y = 1 },
                    new Coordinate{x = 6, y = 2 },
                    new Coordinate{x = 5, y = 4 },
                    new Coordinate{x = 3, y = 3 },
                    new Coordinate{x = 2, y = 1 },
                }
            };
            Path ordered = new Path { Coordinates = g.OrientPath() };
            string expected = "(2 1,6 2,5 4,3 3,2 1)";
            string actual = ordered.LineStringText(false);
            Assert.AreEqual(expected, actual);
            Assert.IsTrue(ordered.SignedArea() > 0);
        }

        [TestMethod]
        public void TestPath_OrientPath4()
        {
            var g = new Path
            {
                Coordinates = new List<Coordinate>
                {
                    new Coordinate{x = 0, y = 0 },
                    new Coordinate{x = 0, y = 0 },
                    new Coordinate{x = 0, y = 0 },
                    null,
                    new Coordinate{x = 2, y = 1 },
                }
            };
            Path ordered = new Path { Coordinates = g.OrientPath() };
            string expected = "(0 0,2 1,0 0)";
            string actual = ordered.LineStringText(false);
            Assert.AreEqual(expected, actual);
            Assert.IsTrue(ordered.SignedArea() == 0);
        }
    }
}
