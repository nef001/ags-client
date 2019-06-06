using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ags_client.Operations.GeometryOps
{
    public class AreasAndLengthsResponse : BaseResponse
    {
        public List<double> areas { get; set; }
        public List<double> lengths { get; set; }
    }
}
