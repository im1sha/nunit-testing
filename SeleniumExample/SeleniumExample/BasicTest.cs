using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumExample
{
    class BasicTest
    {
        IWebDriver driver;

        [SetUp]
        public void Initiaize()
        {
            driver = new ChromeDriver();
        }

        [Test]
        public void Test()
        {
            driver.Navigate().GoToUrl("http://toolsqa.wpengine.com/automation-practice-switch-windows/");
            IWebElement element = driver.FindElement(By.Id("colorVar"));
            DefaultWait<IWebElement> wait = new DefaultWait<IWebElement>(element);
            wait.Timeout = TimeSpan.FromMinutes(2);
            wait.PollingInterval = TimeSpan.FromMilliseconds(250);

            Func<IWebElement, bool> waiter = new Func<IWebElement, bool>((IWebElement ele) =>
            {
                String styleAttrib = element.GetAttribute("style");
                if (styleAttrib.Contains("red"))
                {
                    return true;
                }
                Console.WriteLine("Color is still " + styleAttrib);
                return false;
            });
            wait.Until(waiter);
        }

        [TearDown]
        public void Close()
        {
            driver.Close();
        }
    }
}
