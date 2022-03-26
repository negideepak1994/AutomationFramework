using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AutomationFramework.FrameworkComponents
{
    public class XMLOperations : GlobalVariable
    {
        //Environment.XML file contains all the CSV files path, if we want to read the file location we need to read it from environment.xml
        //file and need to define the same as COnstants
        public static Dictionary<string, string> EnvXMLCollection { get; set; }
        public static Dictionary<string, string> testXMLData = new Dictionary<string, string>();

        // Below function is used to Read XML File

        public static Dictionary<string, string> ReadInputXML(string filepath, string CommonTagName, string valueToFind = null)
        {
            string value = valueToFind;

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(filepath);
            XmlNodeList nodeList = xmlDoc.GetElementsByTagName(CommonTagName);
            foreach (XmlNode node in nodeList)
            {
                if (testXMLData != null)
                {
                    testXMLData.Clear();
                }
                for (int i = 0; i < node.ChildNodes.Count; i++)
                {
                    testXMLData.Add(node.ChildNodes.Item(i).Name, node.ChildNodes.Item(i).InnerText);
                }
                if (value != "")
                {
                   bool isValueFound = IsValuePresent(testXMLData, value);
                    if (isValueFound)
                    {
                        return testXMLData;
                    }
                }
                else
                {
                    return testXMLData;
                }
            }
            return null;
        }

        //the below method is to check whether the value is present in dictionary.
        // Used Internally

        public static bool IsValuePresent(Dictionary<string,string> collection, string valueToFind)
        {
            try
            {
                bool valuePresent = false;

                if (!collection.ContainsValue(valueToFind))
                {
                    return valuePresent;
                }
                else
                {
                    valuePresent = true;
                    return valuePresent;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        //below function is used to read data from environment.xml file and it stores the values in dictionary object "EnvXMLCollection" of XML Operation
        //class

        public static void GetEnvironmentXMLData()
        {
            try
            {
                EnvXMLCollection = new Dictionary<string, string>();
                EnvXMLCollection = ReadInputXML(AppConfigReader.EnvironmentXMLPath,null, "Environment");
                if (EnvXMLCollection == null)
                {
                    throw new Exception("Not able to read Environment XML");
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        //below method should be used to read value from dictionary collection of string type

        public static string GetInputValueByKey(Dictionary<string,string> collection , string key)
        {
            try
            {
                string value = "";
                if (!collection.TryGetValue(key, out value))
                {
                    //the key is not present in the dictionary
                    return value; // or whatever we want to do
                }
                value = collection[key];
                if (value == "#NULL")
                {
                    return null;
                }
                else
                {
                    return value;
                }
            }
            catch (Exception ex)
            {

                throw new Exception("Issue while finding the value for Key:" + key + ". Exception Details:"+ex.Message);
            }
        }
    }
}
