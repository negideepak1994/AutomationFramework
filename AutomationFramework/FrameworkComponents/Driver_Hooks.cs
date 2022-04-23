using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.FrameworkComponents
{
    public class Driver_Hooks
    {

        public static void ClassInitialize(TestContext testContext)
        {
            new AppConfigReader(testContext);
            XMLOperations.GetEnvironmentXMLData();
        }
        public static void TestInitialize(bool isHeadlessBrowser = false, string browserToBeUsed = null)
        {
            if (SeleniumOperations.webDriver == null)
            {

                //reading URL, username & password from environment.xml
                string URL = XMLOperations.GetInputValueByKey(XMLOperations.EnvXMLCollection, "URL");
                string username = AppConfigReader.username;
                string password = AppConfigReader.password;

                if (browserToBeUsed != null)
                {
                     browserToBeUsed = AppConfigReader.browserName.ToLower();
                }

                SeleniumOperations.webDriver = SeleniumOperations.OpenBrowser(browserToBeUsed, URL, true, OpenQA.Selenium.UnhandledPromptBehavior.Dismiss, SeleniumOperations.timeoutInSeconds, isHeadlessBrowser);

                if (SeleniumOperations.webDriver != null)
                {
                    SeleniumOperations.webDriver.Quit();
                }
            }
        }

        public static void ClassCleanup()
        {
            SeleniumOperations.ReleaseWebDriver();
        }
    }
}
