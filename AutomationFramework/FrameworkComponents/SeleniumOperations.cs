using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Chromium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.FrameworkComponents
{
    /// <summary>
    /// This Selenium Operations Page contains all the Selenium Related Operations, like dropdown,
    /// checkboxes,textbox
    /// </summary>

    public enum Locator
    {
        ID,
        Name,
        XPath,
        ClassName,
        LinkText,
        TagName,
        PartialLinkText,
        CssSelector
    }
    public enum DropdownSelectionType
    {
        Text,
        Value,
        Index
    }
    public class SeleniumOperations
    {
        #region Static Selenium Properties
        public static IWebDriver webDriver { get; set; }
        public static IWebElement webElement { get; set; }
        public static IReadOnlyCollection<IWebElement> webElements { get; set; }
        public static int timeoutInSeconds = 60;
        public static int implicitWaitInSeconds = 1;
        public static string globalObjectName { get; set; }
        public static string globalObjectValue { get; set; }
        public static int seleniumBrowserProcessID { get; set; }
        public static string currentHandle { get; set; }
        #endregion

        #region Static Selenium Variables
        public static Process process = new Process();
        public static By globalObjectName_POM { get; set; }   //This is used in writing the Xath
        #endregion

        #region Static Custom Selenium Operations

        /*This method is used to find the Element on browser and stores it in webElement property.
         */

        //Need to Add these Minimum Methods for Functioning of a Simple Loggin In Script
        //Switch To Alert
        public static void FindElement(By objectName, int timeoutInSeconds, bool verifyClickable = false, bool isFrame = false, string[] oldChars = null, string[] newChars = null, bool isMoveToWebElement = true)
        {
            Stopwatch globalWatch = new Stopwatch();

            try
            {
                globalObjectName_POM = objectName;
                globalObjectName = objectName.Criteria;
                webElement = null;

                if (objectName == null)
                {
                    throw new Exception("Object Name: " + globalObjectName.ToString() + "NOT found in object Repository");
                }
                if (oldChars != null && newChars != null)
                {
                    globalObjectValue = globalObjectName_POM.Criteria.ToString();
                    for (int i = 0; i < oldChars.Count(); i++)
                    {
                        if (!string.IsNullOrEmpty(oldChars[i]))
                        {
                            globalObjectValue = globalObjectValue.Replace(oldChars[i], newChars[i]);
                        }
                    }

                    string property = globalObjectName_POM.Mechanism;
                    if (property == Locator.ID.ToString().ToLower())
                    {
                        globalObjectName_POM = By.Id(globalObjectValue);
                    }
                    else if (property == Locator.Name.ToString().ToLower())
                    {
                        globalObjectName_POM = By.Name(globalObjectValue);
                    }
                    else if (property == Locator.XPath.ToString().ToLower())
                    {
                        globalObjectName_POM = By.XPath(globalObjectValue);
                    }
                    else if (property == Locator.ClassName.ToString().ToLower())
                    {
                        globalObjectName_POM = By.ClassName(globalObjectValue);
                    }
                    else if (property == Locator.TagName.ToString().ToLower())
                    {
                        globalObjectName_POM = By.TagName(globalObjectValue);
                    }
                    else if (property == Locator.LinkText.ToString().ToLower())
                    {
                        globalObjectName_POM = By.LinkText(globalObjectValue);
                    }
                    else if (property == Locator.PartialLinkText.ToString().ToLower())
                    {
                        globalObjectName_POM = By.PartialLinkText(globalObjectValue);
                    }
                    else if (property == Locator.CssSelector.ToString().ToLower())
                    {
                        globalObjectName_POM = By.CssSelector(globalObjectValue);
                    }
                }

                RetrieveWebElement(globalObjectName_POM, timeoutInSeconds, verifyClickable, isFrame, isMoveToWebElement);

                if (webElement == null && !isFrame)
                {
                    throw new Exception("Element:" + globalObjectName_POM + "NOT found at Url: " + SeleniumOperations.webDriver.Url);
                }
            }
            catch (Exception ex)
            {
                //To Add Logging Method
                throw new Exception("Element:" + globalObjectName_POM + "NOT found at URL: " + SeleniumOperations.webDriver.Url + "Exception Details: " + ex.Message);
            }
        }

        public static void RetrieveWebElement(By by, int timeoutInSeconds, bool verifyClickable, bool isFrame = false, bool isMoveToWebElement = true)
        {
            Exception exp = new Exception();
            bool isElementFound = false;

            if (timeoutInSeconds > 0)
            {
                Stopwatch elementSearch_TimeElapsed = new Stopwatch();
                elementSearch_TimeElapsed.Start();

                while (!isElementFound && timeoutInSeconds > Convert.ToInt32(elementSearch_TimeElapsed.Elapsed.TotalSeconds))
                {
                    WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(1));
                    try
                    {
                        if (isFrame)
                        {
                            webDriver = wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(by));
                        }
                        else if (verifyClickable)
                        {
                            webElement = wait.Until(ExpectedConditions.ElementToBeClickable(by));
                        }
                        else
                        {
                            wait.Until(ExpectedConditions.ElementExists(by));
                        }
                        if (isMoveToWebElement)
                        {
                            //Need to Add Move and Scroll to Element Methods in Selenium Operations Class 
                        }

                        isElementFound = true;
                        //Need to add WriteLog Method
                    }
                    catch (Exception ex)
                    {
                        exp = ex;
                    }
                }
                if (elementSearch_TimeElapsed.IsRunning) ;
                elementSearch_TimeElapsed.Stop();
            }
            else
            {
                webElement = webDriver.FindElement(by);
                //Add - MoveToWebElement method

                isElementFound = true;

            }
            if (!isElementFound)
                throw new Exception("Issue while getting element with property : " + by + ". Exception details: " + exp.Message + "Inner Exception: " + exp.InnerException);
        }

        public static void FindElements(By objectName, int timeoutInSeconds, bool verifyClickable = false, bool isFrame = false, string[] oldChars = null, string[] newChars = null, bool isMoveToWebElement = true)
        {
            Stopwatch globalWatch = new Stopwatch();

            try
            {
                globalObjectName_POM = objectName;
                globalObjectName = objectName.Criteria;
                webElement = null;

                if (objectName == null)
                {
                    throw new Exception("Object Name: " + globalObjectName.ToString() + "NOT found in object Repository");
                }
                if (oldChars != null && newChars != null)
                {
                    globalObjectValue = globalObjectName_POM.Criteria.ToString();
                    for (int i = 0; i < oldChars.Count(); i++)
                    {
                        if (!string.IsNullOrEmpty(oldChars[i]))
                        {
                            globalObjectValue = globalObjectValue.Replace(oldChars[i], newChars[i]);
                        }
                    }

                    string property = globalObjectName_POM.Mechanism;
                    if (property == Locator.ID.ToString().ToLower())
                    {
                        globalObjectName_POM = By.Id(globalObjectValue);
                    }
                    else if (property == Locator.Name.ToString().ToLower())
                    {
                        globalObjectName_POM = By.Name(globalObjectValue);
                    }
                    else if (property == Locator.XPath.ToString().ToLower())
                    {
                        globalObjectName_POM = By.XPath(globalObjectValue);
                    }
                    else if (property == Locator.ClassName.ToString().ToLower())
                    {
                        globalObjectName_POM = By.ClassName(globalObjectValue);
                    }
                    else if (property == Locator.TagName.ToString().ToLower())
                    {
                        globalObjectName_POM = By.TagName(globalObjectValue);
                    }
                    else if (property == Locator.LinkText.ToString().ToLower())
                    {
                        globalObjectName_POM = By.LinkText(globalObjectValue);
                    }
                    else if (property == Locator.PartialLinkText.ToString().ToLower())
                    {
                        globalObjectName_POM = By.PartialLinkText(globalObjectValue);
                    }
                    else if (property == Locator.CssSelector.ToString().ToLower())
                    {
                        globalObjectName_POM = By.CssSelector(globalObjectValue);
                    }
                }

                RetrieveWebElements(globalObjectName_POM, timeoutInSeconds, verifyClickable, isFrame, isMoveToWebElement);

                if (webElement == null && !isFrame)
                {
                    throw new Exception("Element:" + globalObjectName_POM + "NOT found at Url: " + SeleniumOperations.webDriver.Url);
                }
            }
            catch (Exception ex)
            {
                //To Add Logging Method
                throw new Exception("Element:" + globalObjectName_POM + "NOT found at URL: " + SeleniumOperations.webDriver.Url + "Exception Details: " + ex.Message);
            }
        }

        public static void RetrieveWebElements(By by, int timeoutInSeconds, bool isVerifyVisibility, bool isFrame = false, bool isMoveToWebElement = true)
        {
            Exception exp = new Exception();
            bool isElementFound = false;
            SeleniumOperations.webElements = null;

            if (timeoutInSeconds > 0)
            {
                Stopwatch elementSearch_TimeElapsed = new Stopwatch();
                elementSearch_TimeElapsed.Start();

                while (!isElementFound && timeoutInSeconds > Convert.ToInt32(elementSearch_TimeElapsed.Elapsed.TotalSeconds))
                {
                    WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(1));
                    try
                    {
                        if (isVerifyVisibility)
                        {
                            webElements = wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(by));
                        }
                        else
                        {
                            webElements = wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(by));
                        }
                        if (webElements != null && webElements.Count != 0)
                        {
                            isElementFound = true;

                        }

                        //Need to add WriteLog Method
                    }
                    catch (Exception ex)
                    {
                        exp = ex;
                    }
                }
                if (elementSearch_TimeElapsed.IsRunning) ;
                elementSearch_TimeElapsed.Stop();
            }
            else
            {
                webElements = webDriver.FindElements(by);
                //Add - MoveToWebElement method

                isElementFound = true;

            }
            if (!isElementFound)
                throw new Exception("Issue while getting elements with property : " + by + ". Exception details: " + exp.Message + "Inner Exception: " + exp.InnerException);
        }

        ///<Summary>
        ///This method is used to Open browser. Used Internally by BaseTest class Constructor
        /// </Summary>
        /// <param name="browserType"> Value Should be Chrome, Edge, or IE and is set in App.Config file</param>
        /// <param name="URL"> URL to navigate browser</param>
        /// <param name="prompBehaviour">Should be Used Only for IE. User can set, ignore, or dismiss the initial popups encountered while opening IE</param>
        /// <param name="timeOutInSeconds"></param>
        /// <returns>returns browser driver</returns>

        public static List<int> seleniumBrowserProcessIds = new List<int>();
        static string chromeDriverName = "chromedriver";
        static string chromeProcessName = "chrome";
        static string edgeChromiumDriver = "msedgedriver";
        static string edgeChromiumName = "msedge";

        public static IWebDriver OpenBrowser(string broswerType, string URL, bool isMaximised, UnhandledPromptBehavior promptBehavior, int timeoutInSeconds, bool isHeadlessBrowser = false)
        {
            try
            {
                //  IWebDriver webDriver;
                //  RemoteWebDriver driver = null;
                if (broswerType.ToUpper() == BrowserType.CHROME)
                {
                    // In Utilities can add method to Kill Process

                    IEnumerable<int> pidsBefore = Process.GetProcessesByName(chromeProcessName).Select(p => p.Id);
                    string loggedInUser = Environment.UserName;
                    var options = new ChromeOptions();
                    options.AddArgument("--noerrdialogs");
                    options.AddArgument("--disable-popup-blocking");

                    options.PageLoadStrategy = PageLoadStrategy.Normal;
                    options.UnhandledPromptBehavior = promptBehavior;
                    // main code to open broswer, is below and we will pass all these options(instructions) to our Chromedriver
                    webDriver = new ChromeDriver(@"..\..\Drivers", options);
                    Thread.Sleep(5000);

                    IEnumerable<int> pidsAfter = Process.GetProcessesByName("chrome").Select(p => p.Id);
                    seleniumBrowserProcessIds.Clear();
                    seleniumBrowserProcessIds = pidsAfter.Except(pidsBefore).ToList();
                    seleniumBrowserProcessID = pidsAfter.Except(pidsBefore).FirstOrDefault();

                }
                else if (broswerType.ToUpper() == BrowserType.EDGE)
                {
                    // In Utilities can ass method to kill process

                    string LoggedInuser = Environment.UserName;
                    var options = new EdgeOptions();
                    options.AddArgument("--disable-popup-blocking");
                    options.AddArgument("--disable-remote-debugging-port");

                    if (isHeadlessBrowser)
                        options.AddArgument("--headless");

                    //To Get  all DevTools Details
                    var perfLogPrefs = new ChromiumPerformanceLoggingPreferences();
                    perfLogPrefs.IsCollectingNetworkEvents = true;
                    perfLogPrefs.AddTracingCategories(new string[] { "devtools.network" });
                    options.PerformanceLoggingPreferences = perfLogPrefs;
                    options.SetLoggingPreference("performance", LogLevel.All);

                    options.UnhandledPromptBehavior = promptBehavior;
                    options.PageLoadStrategy = PageLoadStrategy.Normal;

                    IEnumerable<int> pidsBefore = Process.GetProcessesByName(chromeProcessName).Select(p => p.Id);

                    //main code for opening browser with these different options

                    webDriver = new EdgeDriver(@"..\..\Drivers", options);
                    webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                    webDriver.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromSeconds(10);
                    Thread.Sleep(5000);
                    IEnumerable<int> pidsAfter = Process.GetProcessesByName("chrome").Select(p => p.Id);
                    seleniumBrowserProcessIds.Clear();
                    seleniumBrowserProcessIds = pidsAfter.Except(pidsBefore).ToList();
                    seleniumBrowserProcessID = pidsAfter.Except(pidsBefore).FirstOrDefault();
                }
                else
                {
                    throw new Exception("Browser Type: " + broswerType + "is not supported");
                }

                if (isMaximised)
                {
                    webDriver.Manage().Window.Maximize();
                    webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                }
                return webDriver;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static void NavigateToUrl(string URL, string userName = "", string password = "")
        {
            string urlToNavigate = URL;
            string elapsedTime = string.Empty;

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
                webDriver.Navigate().GoToUrl(urlToNavigate);
                //here waitTillpageload method will come
            }
            catch (Exception ex)
            {

                throw new Exception("Selenium's GoToUrl method failed for URL navigation:" + urlToNavigate + "with exceptiom" + ex.Message);
            }
            if (stopwatch.IsRunning)
            {
                stopwatch.Stop();
                elapsedTime = stopwatch.Elapsed.ToString();
            }
        }

        public static void WaitTillPageLoad(int timeoutInSeconds)
        {
            if (timeoutInSeconds == 0)
            {
                return;
            }
            bool isPageLoadedCompletely = false;

            IJavaScriptExecutor js = (IJavaScriptExecutor)webDriver;

            //Below loop will rotate for "timeoutInSeconds" to check if the page is ready after every 1 seconds

            for (int i = 0; i < timeoutInSeconds; i++)
            {
                SeleniumOperations.Wait(1000);
                try
                {
                    //To check if the page is in ready state
                    if (js.ExecuteScript("return document.readyState").ToString().Equals("complete"))
                    {
                        isPageLoadedCompletely = true;
                        break;
                    }
                }
                catch //do nothing
                {
                    break;
                }
            }
        }

        public static void Wait(int timeoutInMilliSeconds)
        {
            Thread.Sleep(timeoutInMilliSeconds);
        }

        public static void ScrollToElementUsingJS(IWebElement element)
        {
            try
            {
                ((IJavaScriptExecutor)webDriver).ExecuteScript("arguments[0].scrollIntoView(true);", element);
            }
            catch (Exception ex)
            {

                throw new Exception("Element is not Visible in Scroll");
            }
        }

        public static void MoveToWebElemet()
        {
            try
            {
                ScrollToElementUsingJS(webElement);
            }
            catch (Exception ex)
            {

                throw new Exception("Move Mouse to webelement failed for object name: " + globalObjectName + "Exception Details: " + ex.Message);
            }
        }

        public static void MoveToWebElementAndClick()
        {
            try
            {
                Actions action = new Actions(webDriver);

                action.MoveToElement(webElement).Click().Build().Perform();
            }
            catch (Exception)
            {
                //do nothing
            }
        }

        public static string RefreshPage()
        {
            string currentUrl = string.Empty;
            try
            {
                currentUrl = webDriver.Url;
                webDriver.Navigate().Refresh();
            }
            catch (Exception)
            {

                throw;
            }
            return currentUrl;
        }

        public static void ReleaseWebDriver()
        {
            if (webDriver != null)
            {
                SeleniumOperations.webDriver.Quit();
                SeleniumOperations.webDriver.Dispose();
                SeleniumOperations.webDriver = null;
            }
        }

        public static void ValidateAttributeValue(string attribute, string expectedValue)
        {
            string actualAttributeValue = "";
            try
            {
                actualAttributeValue = SeleniumOperations.webElement.GetAttribute(attribute);
                Assert.AreEqual(expectedValue, actualAttributeValue);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        // This below function switches to the last open window
        // This function returns current window handle to be stored for future references.

        public static string SwitchToNewBrowserWindow(bool waitTillNewWindowLoad = true, List<string> oldWindowHandles = null)
        {
            try
            {
                int windowLoadTimeoutInSecoonds = 300;
                //pass current handle to another member to shift back later

                currentHandle = webDriver.CurrentWindowHandle;
                string newHandle = webDriver.WindowHandles.Last();

                if (oldWindowHandles != null)
                {
                    newHandle = webDriver.WindowHandles.Except(oldWindowHandles).FirstOrDefault();
                    if (newHandle == null)
                    {
                        newHandle = webDriver.WindowHandles.Last();
                    }
                }
                //wait until window is loaded
                WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(windowLoadTimeoutInSecoonds));
                //main code
                wait.Until(drv => drv.SwitchTo().Window(newHandle));

                if (waitTillNewWindowLoad)
                {
                    WaitTillPageLoad(timeoutInSeconds);
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error Occured while switching to new Window. Error Details: " + ex.Message);
            }
            return currentHandle;
        }

        public static bool verifyElementVisibility(By objectname, int timeoutInSeconds, bool isVerifyClickable = false, bool isFrame = false, string[] oldChars = null, string[] newChars = null, bool isMoveToWebElement = true)
        {
            try
            {
                FindElement(objectname, timeoutInSeconds, isVerifyClickable, isFrame, oldChars, newChars, isMoveToWebElement);
                if (SeleniumOperations.webElement.Displayed)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static string SwitchToAlert(bool acceptAlert, int timeoutInSeconds)
        {
            string text = null;
            try
            {
                WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(timeoutInSeconds));
                IAlert alert = wait.Until(ExpectedConditions.AlertIsPresent());

                if (alert != null)
                {
                    alert = SeleniumOperations.webDriver.SwitchTo().Alert();
                    text = alert.Text;

                    if (acceptAlert)
                        alert.Accept();
                    else
                    {
                        alert.Dismiss();
                    }
                }
            }
            catch (Exception ex)
            {

                throw new Exception("System is not able to switch to alert");
            }
            return text;

        }

        #endregion
    }
}
