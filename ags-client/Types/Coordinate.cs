using System;

namespace ags_client.Types
{

    public class Coordinate : IComparable<Coordinate> // this is not a geometry point
    {
        public double x { get; set; }
        public double y { get; set; }

        public int CompareTo(Coordinate other)
        {
            int result = Math.Sign(x - other.x);
            if (result == 0)
            {
                result = Math.Sign(y - other.y);
            }
            return result;
        }

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

        public bool IsEmpty()
        {
            return (double.IsNaN(x) || double.IsNaN(y) || double.IsInfinity(x) || double.IsInfinity(y));
        }

        public override bool Equals(object obj)
        {
            // check for self comparison
            if (this == obj)
                return true;

            Coordinate other = obj as Coordinate;
            if (other == null)
                return false;

            return this.x.Equals(other.x) && this.y.Equals(other.y);
        }

        public override int GetHashCode()
        {
            int seed = 23;
            int result = hashdouble(seed, this.x);
            result = hashdouble(result, this.y);
            return result;
        }

        private int hashlong(int seed, long n)
        {
            return firstTerm(seed) + (int)(n ^ (n >> 32));
        }

        private int hashdouble(int seed, double d)
        {
            return hashlong(seed, BitConverter.DoubleToInt64Bits(d));
        }

        private int firstTerm(int seed)
        {
            int odd_prime = 37;
            return odd_prime * seed;
        }

    }
}
