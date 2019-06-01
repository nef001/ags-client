using System;
using System.IO;
using System.Net;
using System.Text;

namespace ags_client.Utility
{
    public class HttpUtil
    {
        private const string REQUEST_METHOD = "POST";
        private const string REQUEST_CONTENT_TYPE = "application/x-www-form-urlencoded; charset=UTF-8"; //"application/x-www-form-urlencoded";

        public const string REQ_PARAM_FORMAT_JSON = "f=pjson";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestUrl"></param>
        /// <param name="requestData"></param>
        /// <returns></returns>
        public static string GetResponse(string requestUrl, string requestData)
        {
            string result = null;

            var request = (HttpWebRequest)WebRequest.Create(new Uri(requestUrl));
            request.Method = REQUEST_METHOD;
            request.ContentType = REQUEST_CONTENT_TYPE;

            var encoding = new UTF8Encoding();
            var jsonData = encoding.GetBytes(requestData);
            request.ContentLength = jsonData.Length;
            var postStream = request.GetRequestStream();
            postStream.Write(jsonData, 0, jsonData.Length);
            postStream.Close();

            var httpWebResponse = (HttpWebResponse)request.GetResponse();
            var httpWebResponseStream = httpWebResponse.GetResponseStream();

            if (httpWebResponseStream != null)
            {
                using (var responseResult = new StreamReader(httpWebResponseStream))
                {
                    result = responseResult.ReadToEnd();
                }
            }
            return result;
        }

        
    }
}
