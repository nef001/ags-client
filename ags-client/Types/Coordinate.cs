

namespace ags_client.Types
{

    public class Coordinate // this is not a geometry point
    {
        public double x { get; set; }
        public double y { get; set; }

        public double[] ToArray()
        {
            return new double[] { x, y };
        }

        public string PointString()
        {
            /*
             <point> ::= <x> <y>
             */

            return $"{x} {y}";
        }

        public string PointText()
        {
            /*
             <point text> ::= <empty set> | <left paren> <point> <right paren>
             */
            if (double.IsNaN(x) || double.IsNaN(y) || double.IsInfinity(x) || double.IsInfinity(y))
                return "EMPTY";

            return $"({PointString()})";
        }

        

            

        public override string ToString()
        {
            return PointString();
        }
        
    }
}
