using AutomationFramework.FrameworkComponents;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.TestScripts.Application_Pages.UI
{
    public class Flipkart_HomePage_UI : ErrorHandler
    {

        #region HomePage Elements

        public static By cancel_button_popup = By.XPath("//div[@class='_2QfC02']//button[@class='_2KpZ6l _2doB4z']");
        public static By login_button = By.XPath("//div[@class='_28p97w']/div/div/a");

        #endregion
    }
}
