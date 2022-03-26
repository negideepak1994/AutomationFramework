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
        private static string sendEmail;
        private static string captureScreenshot;
        private static string take_Common_Data_From_Local_File;
        private static string common_Test_Data_Row;
        private static string browserName;
        private static string username;
        private static string password;
        public static string environmentXMLPath;


        //constructor of AppConfigReader class

        public AppConfigReader(TestContext testContext)
        {
            sendEmail = testContext.Properties["SendEmail"].ToString();
            captureScreenshot = testContext.Properties["Capture"].ToString();
            take_Common_Data_From_Local_File = testContext.Properties["Take_Common_Data_From_Local_File"].ToString();
            common_Test_Data_Row = testContext.Properties["Common_Test_Data_Row"].ToString();
            browserName = testContext.Properties["BrowserName"].ToString();
            username = testContext.Properties["Username"].ToString();
            password = testContext.Properties["Password"].ToString();
            environmentXMLPath = testContext.Properties["EnvironmentXMLPath"].ToString();
        }

        #region Read inputs from App.Config 

        public static string SendEmail { get; set; }
        public static string CaptureScreenshot { get; set; }
        public static string Take_Common_Test_Data_From_Local_File { get; set; }
        public static string Common_Test_Data_Row { get; set; }
        public static string BrowserName { get; set; }
        public static string Username { get; set; }
        public static string Password { get; set; }
        public static string EnvironmentXMLPath { get; set; }
        #endregion
    }
}
