using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ags_client.Types.Geometry
{
    public class Segment : IComparable<Segment>
    {
        public Coordinate FromPt { get; }
        public Coordinate ToPt { get; }

        public double X1 { get { return FromPt.x; } }
        public double Y1 { get { return FromPt.y; } }
        public double X2 { get { return ToPt.x; } }
        public double Y2 { get { return ToPt.y; } }

        public Segment(Coordinate p1, Coordinate p2)
        {
            if (p1.CompareTo(p2) > 0)
            {
                this.FromPt = p2;
                this.ToPt = p1;
            }
            else
            {
                this.FromPt = p1;
                this.ToPt = p2;
            }
        }

        public bool IntersectsWith(Segment other)
        {
            int o1 = getOrientation(FromPt, ToPt, other.FromPt);
            int o2 = getOrientation(FromPt, ToPt, other.ToPt);
            int o3 = getOrientation(other.FromPt, other.ToPt, FromPt);
            int o4 = getOrientation(other.FromPt, other.ToPt, ToPt);

            // General case
            if (o1 != o2 && o3 != o4)
                return true;

            // Special Cases
            // other.p1 is colinear with this segment and other.p1 lies on segment p1p2
            if (o1 == 0 && intersectsWithColinearPoint(other.FromPt)) return true;

            // other.p2 is colinear with this segment and other.p2 lies on segment p1p2
            if (o2 == 0 && intersectsWithColinearPoint(other.ToPt)) return true;

            // p1 is colinear with other segment and p1 lies on other segment
            if (o3 == 0 && other.intersectsWithColinearPoint(FromPt)) return true;

            // p2 is colinear with other segment and p2 lies on other segment
            if (o4 == 0 && other.intersectsWithColinearPoint(ToPt)) return true;

            return false; // Doesn't fall in any of the above cases
        }

        // Given colinear point q, checks if q lies on this line segment
        private bool intersectsWithColinearPoint(Coordinate q)
        {
            return q.x <= ToPt.x && q.x >= FromPt.x &&
                q.y <= Math.Max(FromPt.y, ToPt.y) && q.y >= Math.Min(FromPt.y, ToPt.y);
        }


        /// <summary>
        /// 0 = Colinear, 1 = Clockwise, 2 = Counterclockwise
        /// </summary>
        /// <param name="p"></param>
        /// <param name="q"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        private int getOrientation(Coordinate p, Coordinate q, Coordinate r)
        {
            // See 10th slides from following link for derivation of the formula
            // http://www.dcs.gla.ac.uk/~pat/52233/slides/Geometry1x1.pdf

            double val = (q.y - p.y) * (r.x - q.x) - (q.x - p.x) * (r.y - q.y);

            return Math.Sign(val);
        }

        public int CompareTo(Segment other)
        {
            if (this == other)
            {
                return 0;
            }
            if (X1 < other.X1)
            {
                return -other.CompareTo(this);
            }
            if (X1 == other.X1)
            {
                int result = FromPt.CompareTo(other.FromPt);
                if (result != 0)
                {
                    return result;
                }
                else
                {
                    // same start points, compare end points
                    switch (getOrientation(other.FromPt, other.ToPt, ToPt))
                    {
                        case 1: // Orientation.Clockwise:
                            return -1;
                        case -1: // Orientation.Counterclockwise:
                            return 1;
                        default:
                            return ToPt.CompareTo(other.ToPt);
                    }
                }
            }
            // TODO: check if other is entirely above or below us

            switch (getOrientation(other.FromPt, other.ToPt, FromPt))
            {
                case 1: // Orientation.Clockwise:
                    return -1;
                case -1:  // Orientation.Counterclockwise:
                    return 1;
                default:
                    //TODO: colinear line segments, starting in same point?!?
                    return 0;
            }
        }

        public override string ToString()
        {
            return $"({FromPt})->({ToPt})";
        }

    }
}
