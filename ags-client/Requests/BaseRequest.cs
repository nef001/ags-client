using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RestSharp;

namespace ags_client.Requests
{
    public abstract class BaseRequest
    {
        public abstract BaseResponse Execute(AgsClient client, string resourcePath);
    }
}
