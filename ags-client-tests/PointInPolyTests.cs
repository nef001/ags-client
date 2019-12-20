using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using ags_client.Types;
using ags_client.Types.Geometry;

namespace ags_client_tests
{
    [TestClass]
    public class PointInPolyTests
    {
        [TestMethod]
        public void TestMethod1()
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
            var c1 = new Coordinate { x = 2, y = 3 };
            bool expected = true;
            bool actual = g.PointInPoly(c1);
            Assert.AreEqual(expected, actual);

            var c2 = new Coordinate { x = 2.5, y = 2.25 };
            expected = false;
            actual = g.PointInPoly(c2);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestMethod2()
        {
            var g = new Polygon
            {
                Rings = new List<Path>
                {
                    new Path
                    {
                        Coordinates = new List<Coordinate>
                        {
                            new Coordinate { x = 0, y = 4 },
                            new Coordinate { x = -4, y = 0 },
                            new Coordinate { x = -3, y = 5 },
                            new Coordinate { x = -7, y = 5 },
                            new Coordinate { x = -3, y = 10 },
                            new Coordinate { x = -6, y = 11 },
                            new Coordinate { x = -2, y = 14 },
                            new Coordinate { x = 0, y = 11 },
                            new Coordinate { x = 3, y = 14 },
                            new Coordinate { x = 5, y = 9.5 },
                            new Coordinate { x = -1, y = 7 },
                            new Coordinate { x = 5, y = 7 },
                            new Coordinate { x = 6, y = 1 },
                            new Coordinate { x = 0, y = 4 },
                        }
                    },
                    new Path
                    {
                        Coordinates = new List<Coordinate>
                        {
                            new Coordinate { x = 0, y = 9 },
                            new Coordinate { x = 1, y = 9 },
                            new Coordinate { x = 3, y = 10 },
                            new Coordinate { x = 2, y = 12 },
                            new Coordinate { x = 0, y = 9 },
                        }
                    },
                    new Path
                    {
                        Coordinates = new List<Coordinate>
                        {
                            new Coordinate { x = -3, y = 2 },
                            new Coordinate { x = -1, y = 4 },
                            new Coordinate { x = -3, y = 4 },
                            new Coordinate { x = -3, y = 2 },
                        }
                    },
                    new Path
                    {
                        Coordinates = new List<Coordinate>
                        {
                            new Coordinate { x = 3, y = 8 },
                            new Coordinate { x = 0, y = 14 },
                            new Coordinate { x = 4, y = 15 },
                            new Coordinate { x = 3, y = 8 },
                        }
                    },
                }
            };
            bool expected = false;
            bool actual = g.PointInPoly(new Coordinate { x = 0, y = 0 } );
            Assert.AreEqual(expected, actual);

            expected = true;
            actual = g.PointInPoly(new Coordinate { x = 3, y = 5 });
            Assert.AreEqual(expected, actual);

            expected = false;
            actual = g.PointInPoly(new Coordinate { x = -2.5, y = 3 });
            Assert.AreEqual(expected, actual);

            expected = false;
            actual = g.PointInPoly(new Coordinate { x = 1, y = 9.5 });
            Assert.AreEqual(expected, actual);

            expected = true;
            actual = g.PointInPoly(new Coordinate { x = 2, y = 10.5 });
            Assert.AreEqual(expected, actual);

            expected = true;
            actual = g.PointInPoly(new Coordinate { x = 0, y = 9 });
            Assert.AreEqual(expected, actual);

            expected = true;
            actual = g.PointInPoly(new Coordinate { x = -2, y = 14 });
            Assert.AreEqual(expected, actual);

            expected = true;
            actual = g.PointInPoly(new Coordinate { x = -4.5, y = 10.5 });
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestMethod3()
        {
            var g = new Polygon
            {
                Rings = new List<Path>
                {
                    new Path
                    {
                        //interior
                        Coordinates = new List<Coordinate>
                        {
                            new Coordinate { x = 1, y = 1 },
                            new Coordinate { x = 4, y = 2 },
                            new Coordinate { x = 2, y = 5 },
                            new Coordinate { x = 1, y = 1 },
                        }
                    },
                    new Path
                    {
                        //interior
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
            var c = new Coordinate { x = 2, y = 3 };
            bool expected = false;
            bool actual = g.PointInPoly(c);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestMethod4()
        {
            var g = new Polygon
            {
                Rings = new List<Path>
                {
                    new Path
                    {
                        //interior
                        Coordinates = new List<Coordinate>
                        {
                            new Coordinate { x = 1, y = 1 },
                            new Coordinate { x = 4, y = 2 },
                            new Coordinate { x = 2, y = 5 },
                            new Coordinate { x = 1, y = 1 },
                        }
                    },
                    new Path
                    {
                        //interior
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

            var c = new Coordinate { x = 2.5, y = 2.25 };
            bool expected = false;
            bool actual = g.PointInPoly(c);
            Assert.AreEqual(expected, actual);
        }
    }
}
