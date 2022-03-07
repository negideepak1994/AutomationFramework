using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.FrameworkComponents
{
    public class GlobalVariable
    {
        public static bool isTestCasePassed;
        public static string TestCaseType { get; set; }
    }
    public class TestCaseType
    {
        public const string API = "API";
        public const string UI = "UI";
    }
    public class BrowserType
    {
        public const string IE = "IE";
        public const string CHROME = "CHROME";
        public const string EDGE = "EDGE";
        public const string FIREFOX = "FIREFOX";

        public static bool IsChromeBrowserUsed()
        {
            if (AppConfigReader.BrowserName.ToUpper() == BrowserType.CHROME)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsEdgeBrowserUsed()
        {
            if (AppConfigReader.BrowserName.ToUpper() == BrowserType.EDGE)
            {
                return true;
            }
            else
            {
                return false ;
            }
        }

        public static bool IsFireFoxBrowserUsed()
        {
            if (AppConfigReader.BrowserName.ToUpper() == BrowserType.FIREFOX)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
