

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

        
    }
}
