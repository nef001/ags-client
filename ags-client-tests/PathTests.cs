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
        public void TestPoint_EmptyToWkt()
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
            string ginitial = "(2 1,5 4,6 2,3 3,2 1)";
            string expected = "(2 1,6 2,5 4,3 3,2 1)";
            string actual = ordered.LineStringText(false);
            Assert.AreEqual(expected, actual);
        }
    }
}
