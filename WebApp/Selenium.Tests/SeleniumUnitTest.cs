using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Selenium.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Firefox;
    using OpenQA.Selenium.IE;
    using OpenQA.Selenium.Remote;
    using OpenQA.Selenium.PhantomJS;
    using System;

    [TestClass]
    public class SeleniumTests
    {
        //private string baseURL = "http://partsunlimited.azurewebsites.net/";
        private string baseURL = "http://pusk101.azurewebsites.net";
        private RemoteWebDriver driver;
        private string browser;
        public TestContext TestContext { get; set; }

        [TestMethod]
        [TestCategory("Selenium")]
        [Priority(5)]
        [Owner("BuildSet")] //Using Owner as Category trait is not supported by the DTA Task
        public void TireSearch_Any()
        {
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
            driver.Navigate().GoToUrl(this.baseURL);
            driver.FindElementById("search-box").Clear();
            //var element = driver.FindElementById("search-box");
            var element = driver.FindElementByName("q");
            element.SendKeys("tire");
            element.SendKeys(Keys.Return);

            System.Threading.Thread.Sleep(5000); // <= DON'T DO THIS, JUST FOR DEMO

            //do other Selenium things here!
        }

        /// <summary>
        /// Use TestCleanup to run code after each test has run
        /// </summary>
        [TestCleanup()]
        public void MyTestCleanup()
        {
            //driver.Quit();
        }

        [TestInitialize()]
        public void MyTestInitialize()
        {   //Set the browswer from a build
            browser = this.TestContext.Properties["browser"] != null ? this.TestContext.Properties["browser"].ToString() : "chrome";
            switch (browser)
            {
                case "firefox":
                    driver = new FirefoxDriver();
                    break;
                case "chrome":
                    driver = new ChromeDriver();
                    break;
                case "ie":
                    driver = new InternetExplorerDriver();
                    break;
                case "PhantomJS":
                    driver = new PhantomJSDriver();
                    break;
                default:
                    driver = new PhantomJSDriver();
                    break;
            }
            if (this.TestContext.Properties["Url"] != null) //Set URL from a build
            {
                this.baseURL = this.TestContext.Properties["Url"].ToString();
            }
            else
            {
                //this.baseURL = "http://partsunlimited.azurewebsites.net/";   //default URL just to get started with
                this.baseURL = "http://pusk101.azurewebsites.net/";   //default URL just to get started with
            }
        }
    }
}
