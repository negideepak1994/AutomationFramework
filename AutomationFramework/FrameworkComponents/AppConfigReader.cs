using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutomationFramework.FrameworkComponents
{
    public class AppConfigReader
    {
        //private static string sendEmail;
        //private static string captureScreenshot;
        //private static string take_Common_Data_From_Local_File;
        //private static string common_Test_Data_Row;
        //private static string browserName;
        //private static string username;
        //private static string password;
        //public static string environmentXMLPath;
        //public static string url;
        //constructor of AppConfigReader class

        #region Read inputs from App.Config 
        public static string sendEmail { get; set; }
        public static string captureScreenshot { get; set; }
        public static string take_Common_Test_Data_From_Local_File { get; set; }
        public static string common_Test_Data_Row { get; set; }
        public static string browserName { get; set; }
        public static string username { get; set; }
        public static string password { get; set; }
        public static string environmentXMLPath { get; set; }
        #endregion
        public AppConfigReader(TestContext testContext)
        {
            sendEmail = testContext.Properties["SendEmail"].ToString();
            captureScreenshot = testContext.Properties["CaptureScreenshot"].ToString();
            take_Common_Test_Data_From_Local_File = testContext.Properties["Take_Common_Data_From_Local_File"].ToString();
            common_Test_Data_Row = testContext.Properties["Common_Test_Data_Row"].ToString();
            browserName = testContext.Properties["BrowserName"].ToString();
            username = testContext.Properties["Username"].ToString();
            password = testContext.Properties["Password"].ToString();
            environmentXMLPath = testContext.Properties["EnvironmentXMLPath"].ToString();
        }

    }
}
