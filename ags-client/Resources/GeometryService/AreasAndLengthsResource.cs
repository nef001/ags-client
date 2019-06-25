using System.Collections.Generic;

namespace ags_client.Resources.GeometryService
{
    public class AreasAndLengthsResource : BaseResponse
    {
        public List<double> areas { get; set; }
        public List<double> lengths { get; set; }
    }
}
