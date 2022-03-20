using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AutomationFramework.FrameworkComponents
{
    public class API_Handler : ErrorHandler
    {

        //Below Method is used to send the create the API Request
        // and returns the HttpWebResponse

        public static HttpWebResponse CreateRequest(string apiURL, HttpMethod requestMethodType, string requestInputData, RequestContentType requestContentType, string authToken, int reqTimeOut, Dictionary<string, string> headers_KVP = null)
        {
            HttpWebResponse response = null;

            string expectedResult = "API call to Endpoint:" + apiURL + "should be successful";
            string actualResult = "API call to Endpoint:" + apiURL + "is successful";

            ErrorHandler(() =>
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiURL);
                request.Timeout = reqTimeOut * 1000;

                #region Authentication code

                if (authToken != null)
                {
                    request.Headers.Add("Authorization", authToken);
                }
                #endregion

                #region Adding additional Headers
                if (headers_KVP != null)
                {
                    foreach (KeyValuePair<string, string> key in headers_KVP.Keys)
                    {
                        request.Headers.Add(key, headers_KVP[key]);
                    }
                }
                #endregion

                request.Method = requestMethodType.Value;

                if (requestContentType != null)
                {
                    request.ContentType = requestContentType.Value;
                }
                if (!string.IsNullOrEmpty(requestInputData))
                {
                    byte[] byteArray = Encoding.ASCII.GetBytes(requestInputData);
                    request.ContentLength = byteArray.Length;
                    using (Stream ds = request.GetRequestStream())
                    {
                        ds.Write(byteArray, 0, byteArray.Length);
                        ds.Close();
                    }
                }
                else
                {
                    request.ContentLength = 0;
                }

                Stopwatch stopwatchLocal = new Stopwatch();
                stopwatchLocal.Start();
                response = request.GetResponse() as HttpWebResponse;
                stopwatchLocal.Stop();

            }, actualResult, expectedResult);
            return response;
        }
    }
}
public class HttpMethod
{
    public string Value { get; set; }
    public HttpMethod(string value)
    {
        Value = value;
    }
    public static HttpMethod PUT { get { return new HttpMethod("PUT"); } }
    public static HttpMethod POST { get { return new HttpMethod("POST"); } }
    public static HttpMethod DELETE { get { return new HttpMethod("DELETE"); } }
    public static HttpMethod GET { get { return new HttpMethod("GET"); } }
}

public class RequestContentType
{
    public string Value { get; set; }
    public RequestContentType(string value)
    {
        Value = value;
    }

    public static RequestContentType Json { get { return new RequestContentType("application/json"); } }
    public static RequestContentType Form { get { return new RequestContentType("application/x-www-form-urlencoded"); } }
    public static RequestContentType JsonText { get { return new RequestContentType("application/json,text/plain, */*"); } }
}
