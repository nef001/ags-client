using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ags_client
{
    public class ResponseErrorException : Exception
    {
        public ResponseErrorException() : base()
        { }
        public ResponseErrorException(string msg) : base(msg)
        { }
    }
}
