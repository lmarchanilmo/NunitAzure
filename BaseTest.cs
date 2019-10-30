using System;
using System.IO;
using System.Reflection;

using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumTestsAzure
{
    [TestFixture]
    public class BaseTest
    {
        public IWebDriver driver;
        public TestContext TestContext { get; set; }

        [SetUp]
        public void setup()
        {
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("headless");
            driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), chromeOptions);
        }
            

        [TearDown]
        public void teardown()
        {
            try
            {
                if (TestContext.CurrentContext.Result.Outcome.Status != NUnit.Framework.Interfaces.TestStatus.Passed)
                {
                    Screenshot ss = ((ITakesScreenshot)driver).GetScreenshot();
                    string path = Directory.GetCurrentDirectory() + TestContext.CurrentContext.Test.MethodName + DateTime.Now.ToString().Trim().Replace("/","_").Replace(":","_").Replace(" ","_") + ".png";
                    ss.SaveAsFile(path);
                    TestContext.AddTestAttachment(path);


                }
            }
            finally
            {
                driver.Close();
                driver.Quit();
            }
            

        }
    }
}
