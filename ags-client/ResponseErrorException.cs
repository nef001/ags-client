using System;

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
