using ags_client.Types.Geometry;
using C5;
using System.Collections.Generic;

namespace ags_client.Algorithms
{
    public class ShamosHoey
    {
        private IPriorityQueue<Event> events;
        private TreeSet<Segment> sweepline;

        public ShamosHoey(List<Segment> segments)
        {
            sweepline = new TreeSet<Segment>();
            events = new IntervalHeap<Event>();
            foreach (var segment in segments)
            {
                events.Add(new Event(segment, EventType.SegmentStart));
                events.Add(new Event(segment, EventType.SegmentEnd));
            }
        }

        public bool HasIntersections()
        {
            while (!events.IsEmpty)
            {
                var currentEvent = events.DeleteMin();

                switch (currentEvent.Type)
                {
                    case EventType.SegmentStart:
                        sweepline.Add(currentEvent.Segment);
                        int i = sweepline.IndexOf(currentEvent.Segment);
                        if (i > 0 && currentEvent.Segment.IntersectsWith(sweepline[0]))
                        {
                            return true;
                        }
                        if (i < sweepline.Count - 1 && currentEvent.Segment.IntersectsWith(sweepline[i + 1]))
                        {
                            return true;
                        }
                        break;
                    case EventType.SegmentEnd:
                        int j = sweepline.IndexOf(currentEvent.Segment);
                        if (j > 0 && j < sweepline.Count - 1 && sweepline[j - 1].IntersectsWith(sweepline[j + 1]))
                        {
                            return true;
                        }
                        sweepline.Remove(currentEvent.Segment);
                        break;
                }
            }
            return false;
        }

        /*
        static public int Main(string[] args)
        {
            int n = 6000;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            List<Segment> segments = new List<Segment>();
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
            if (shamosHoey.HasIntersections())
            {
                Console.WriteLine("Found intersections");
            }
            else
            {
                Console.WriteLine("Found no intersections");
            }
            stopwatch.Stop();
            Console.WriteLine("{0} segments took: {1}", n, stopwatch.ElapsedMilliseconds);
            Console.ReadKey();
            return 0;
        }
        */


    }
}
