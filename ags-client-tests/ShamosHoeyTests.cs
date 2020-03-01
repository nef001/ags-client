
using ags_client.Algorithms;
using ags_client.Types;
using ags_client.Types.Geometry;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace ags_client_tests
{
    [TestClass]
    public class ShamosHoeyTests
    {
        [TestMethod()]
        public void SegmentToStringTest()
        {
            var s1 = new Segment(new Coordinate { x = 0, y = 1 }, new Coordinate { x = 2, y = 3 });
            Assert.AreEqual("(0 1)->(2 3)", s1.ToString());
        }

        [TestMethod()]
        public void SegmentConstructorSwapsTest()
        {
            var s1 = new Segment(new Coordinate { x = 2, y = 3 }, new Coordinate { x = 0, y = 1 });
            Assert.AreEqual("(0 1)->(2 3)", s1.ToString());
        }

        [TestMethod()]
        public void SegmentConstructorSwapsTest2()
        {
            var s1 = new Segment(new Coordinate { x = 77, y = 0 }, new Coordinate { x = 87, y = 1 });
            Assert.AreEqual("(77 0)->(87 1)", s1.ToString());
        }

        [TestMethod()]
        public void SegmentConstructorSwapsEqualX()
        {
            var s1 = new Segment(new Coordinate { x = 2, y = 3 }, new Coordinate { x = 2, y = 2 });
            Assert.AreEqual("(2 2)->(2 3)", s1.ToString());
        }

        [TestMethod()]
        public void Segment_CompareToTest()
        {
            var s1 = new Segment(new Coordinate { x = 0, y = 0 }, new Coordinate { x = 2, y = 2 });
            var s2 = new Segment(new Coordinate { x = 0, y = 2 }, new Coordinate { x = 2, y = 4 });
            Assert.IsTrue(s1.CompareTo(s2) < 0);
            Assert.IsTrue(s1.CompareTo(s1) == 0);
            Assert.IsTrue(s2.CompareTo(s1) > 0);
            Assert.IsTrue(s2.CompareTo(s2) == 0);
        }

        [TestMethod]
        public void Event_CompareToTest()
        {
            var segment = new Segment(new Coordinate { x = 0, y = 1 }, new Coordinate { x = 2, y = 3 });
            var segmentStart = new Event(segment, EventType.SegmentStart);
            var segmentEnd = new Event(segment, EventType.SegmentEnd);
            Assert.IsTrue(segmentStart.CompareTo(segmentEnd) < 0);
            Assert.IsTrue(segmentStart.CompareTo(segmentStart) == 0);
            Assert.IsTrue(segmentEnd.CompareTo(segmentStart) > 0);
            Assert.IsTrue(segmentEnd.CompareTo(segmentEnd) == 0);
        }

        [TestMethod]
        public void ShamosHoey_HasIntersectionsTest()
        {
            int n = 6000;
            var segments = new List<Segment>();
            for (int i = 0; i < n / 2; i++)
            {
                segments.Add(
                    new Segment(
                        new Coordinate { x = i / (double)n, y = i },
                        new Coordinate { x = i / (double)n + 100, y = 100 + i }));

                segments.Add(
                    new Segment(
                        new Coordinate { x = i / (double)n, y = i + n },
                        new Coordinate { x = i / (double)n + 100, y = 100 + i + n }));
            }
            var shamosHoey = new ShamosHoey(segments);
            bool expected = false;
            bool actual = shamosHoey.HasIntersections();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ShamosHoey_SelfIntersectingPolygonTest()
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
                            new Coordinate { x = 4, y = 0 },
                            new Coordinate { x = 4, y = 2 },
                            new Coordinate { x = 1, y = 1 },
                        }
                    }
                }
            };

            var shamosHoey = new ShamosHoey(g.Rings[0].Segments);
            bool expected = true;
            bool actual = shamosHoey.HasIntersections();

            Assert.AreEqual(expected, actual);
        }
    }
}
