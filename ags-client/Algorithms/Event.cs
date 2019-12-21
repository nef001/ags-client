using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ags_client.Types;
using ags_client.Types.Geometry;

namespace ags_client.Algorithms
{
    public enum EventType { SegmentStart, SegmentEnd };

    public class Event : IComparable<Event>
    {
        /// <summary>
        /// The type of the event
        /// </summary>
        public EventType Type { get; }

        /// <summary>
        /// The segment that this event pertains to
        /// </summary>
        public Segment Segment { get; }

        /// <summary>
        /// The point where the event occurs
        /// </summary>
        public Coordinate Point
        {
            get
            {
                switch (Type)
                {
                    case EventType.SegmentStart:
                        return Segment.FromPt;
                    case EventType.SegmentEnd:
                        return Segment.ToPt;
                    default:
                        throw new System.InvalidOperationException();
                }
            }
        }

        /// <summary>
        /// Creates a new event
        /// </summary>
        /// <param name="segment">the segement that this event pertains to</param>
        /// <param name="type">the type of the event</param>
        public Event(Segment segment, EventType type)
        {
            this.Segment = segment;
            this.Type = type;
        }

        public int CompareTo(Event other)
        {
            return Point.CompareTo(other.Point);
        }

        public override string ToString()
        {
            return $"Event: {Type} {Segment}";
        }
    }
}
