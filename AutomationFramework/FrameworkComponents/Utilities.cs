using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.FrameworkComponents
{
    public class Utilities : GlobalVariable
    {

        public static string AppendDateTime(string value)
        {
            return value + DateTime.Now.ToString();
        }

        public static string GetMethodNameFromStack(int stackLevel)
        {
            StackTrace st = new StackTrace();
            StackFrame sf = new StackFrame(stackLevel);

            return sf.GetMethod().Name;
        }

        // Below Method is used to Serialize input JSON request ---as String
        // ( string InputJSONRequest - which is a serialized input data string)
        public static string GetSerializedJson<T>(T deserializeJson)
        {
            string data = JsonConvert.SerializeObject(deserializeJson);
            return data;
        }

        // Below Method convert the HttpWebResponse into String by Parse this Response(JSON Response) as String to get the ResponseString
        public static string GetParsedResponseAsString(HttpWebResponse response)
        {
            StreamReader responseStream = new StreamReader(response.GetResponseStream());

            return responseStream.ReadToEnd().ToString();
        }

        // Below Method will desearialize the JSON  to the specefic .NET type
        // Basically we need to desearilize the string response to get the Response Class Object for validation and need to return that obj

        public static T GetDesearilizedResponseObject<T>(string response)
        {
           T output =  JsonConvert.DeserializeObject<T>(response);
            return output;
        }
    }
}
