using System;
using System.Collections.Generic;

namespace ags_client.Types
{
    public class ErrorDetail
    {
        public int code { get; set; }
        public string message { get; set; }
        public List<string> details { get; set; }

        public override string ToString()
        {
            string msg = String.Format("Code: {0}", code);
            if (!String.IsNullOrWhiteSpace(message))
            {
                msg = String.Format("{0} - {1}", msg, message);
                if (details != null)
                {
                    msg = msg + " " + String.Join(" ", details);
                }
            }
            return msg;
        }
    }
}
