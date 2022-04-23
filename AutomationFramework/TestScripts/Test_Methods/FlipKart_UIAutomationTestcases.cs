using AutomationFramework.FrameworkComponents;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AutomationFramework.TestScripts.Constants.Constants;

namespace AutomationFramework.TestScripts.Test_Methods
{
    [TestClass]
    public class FlipKart_UIAutomationTestcases : ErrorHandler
    {
        public TestContext testContext { get; set; }

        [ClassInitialize()]
        public static void ClassInitialize(TestContext testContext)
        {
            Driver_Hooks.ClassInitialize(testContext);
        }

        [TestInitialize]
        public void TestInitialize()
        {
            if (testContext.TestName.ToUpper().Contains("_UI"))
            {
                Driver_Hooks.TestInitialize();
            }
        }

        [ClassCleanup]
        public static void ClassCleanUp()
        {
            Driver_Hooks.ClassCleanup();
        }


        [TestMethod, TestCategory("Flipkart_HomePage_UI_Testing"), TestCategory("Verify the details on Home page")]
        public void TC01_Verify_that_home_page_is_displayed_after_login_or_Not_UI()
        {
             string exceptions = string.Empty;
             StringBuilder error = new StringBuilder();

             #region get site url and the user login details and navigate to application
             //string url = AppConfigReader.url = "https://www.flipkart.com";
             string userName = AppConfigReader.username;
             string password = AppConfigReader.password;
             string url = XMLOperations.GetInputValueByKey(XMLOperations.EnvXMLCollection, FlipkartApplication.FlipkartURL);
             SeleniumOperations.NavigateToUrl(url);
            #endregion

        }
    }
}
