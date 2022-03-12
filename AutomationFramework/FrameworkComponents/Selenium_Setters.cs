using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.FrameworkComponents
{
    public class Selenium_Setters
    {

        public static void Click()
        {
            bool isOperationSuccessful = false;
            int retry = 3;
            do
            {
                try
                {
                    WebDriverWait wait = new WebDriverWait(SeleniumOperations.webDriver, TimeSpan.FromSeconds((SeleniumOperations.timeoutInSeconds) / retry));
                    IWebElement clickableElement = wait.Until(ExpectedConditions.ElementToBeClickable(SeleniumOperations.webElement));
                    clickableElement.Click();
                    isOperationSuccessful = true;
                }
                catch (Exception)
                {
                    retry--;
                }
            } while (!isOperationSuccessful && retry > 0);
        }

        public static void EnterText(By objectName, string value, int timeoutInSeconds = 60, string[] oldChars = null, string[] newChars = null)
        {
            SeleniumOperations.FindElement(objectName, timeoutInSeconds, false, false, oldChars, newChars);
            SeleniumOperations.webElement.Clear();
            SeleniumOperations.webElement.SendKeys(value);
        }

        public static void Click_With_Left_Ctrl()
        {
            try
            {
                Actions action = new Actions(SeleniumOperations.webDriver);
                action.KeyDown(Keys.LeftControl).Click(SeleniumOperations.webElement).KeyUp(Keys.LeftControl).Build().Perform();
            }
            catch (Exception ex)
            {
                //do nothing
            }
        }


    }
}
