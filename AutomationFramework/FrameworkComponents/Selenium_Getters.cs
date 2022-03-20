using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.FrameworkComponents
{
    public class Selenium_Getters
    {
        public static string currentPageURL { get; set; }  //This property gets updated by Selenium navigation event


        #region Below static methods to use to get the different value

        public static string GetText()
        {
            try
            {
                if (SeleniumOperations.webElement.TagName == "select")
                {
                    SelectElement selectedValue = new SelectElement(SeleniumOperations.webElement);
                    return selectedValue.SelectedOption.Text;
                }
                string value = SeleniumOperations.webElement.GetAttribute("value");
                if (string.IsNullOrEmpty(value))
                {
                    value = SeleniumOperations.webElement.Text;
                }
                return value.Trim();
            }
            catch (Exception ex)
            {

                throw new Exception("System received error while fetching text value of object: " + SeleniumOperations.globalObjectName);
            }
        }

        public static string GetAttribute(string attributeName)
        {
            string value = string.Empty;

            try
            {
                value = SeleniumOperations.webElement.GetAttribute(attributeName);
            }
            catch (Exception ex)
            {

                throw new Exception("System Received error while fetching value of attribute :" + attributeName + "for object:" + SeleniumOperations.globalObjectName);
            }
            return value;
        }

        public static List<string> GetTextList(bool isLowerCaseReq = false)
        {
            List<string> textList = new List<string>();
            foreach (IWebElement webElement in SeleniumOperations.webElements)
            {
                string text = webElement.Text;
                if (!string.IsNullOrEmpty(text))
                {
                    textList.Add(text.ToLower());
                }
                else
                {
                    textList.Add(text);
                }
            }
            return textList;
        }

        public static string GetPageTitle()
        {
            try
            {
                return SeleniumOperations.webDriver.Title;
            }
            catch (Exception ex)
            {
                throw new Exception("System received error while fetching page title. Exception Details: " + ex.Message);
            }
        }

        public static string GetPageURL()
        {
            try
            {
                return SeleniumOperations.webDriver.Url;
            }
            catch (Exception ex)
            {

                throw new Exception("System received error while fetching page URL. Exception Details: " + ex.Message);
            }
        }

        #endregion
    }
}
