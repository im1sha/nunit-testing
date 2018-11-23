using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;

namespace LiverpoolMuseumsTest
{
    public class FindUsTest
    {
        [Test]
        public void Test()
        {
            IWebDriver driver = new ChromeDriver
            {
                Url = "http://www.liverpoolmuseums.org.uk/"
            };


            string xpath = @"/html/body/form[@id='form1']/div[@class='footer']" +
               "/div[@class='container-fluid']/div[@class='row-fluid']" +
               "/div[@class='span4 social']/a[@class='sys_16'][1]/img/@src";

            IWebElement element = driver.FindElement(By.XPath(xpath));
            element.Click();


            //driver.Navigate().GoToUrl("http://toolsqa.wpengine.com/automation-practice-switch-windows/");
            //IWebElement element = driver.FindElement(By.Id("colorVar"));
            //DefaultWait<IWebElement> wait = new DefaultWait<IWebElement>(element);
            //wait.Timeout = TimeSpan.FromMinutes(2);
            //wait.PollingInterval = TimeSpan.FromMilliseconds(250);

            //Func<IWebElement, bool> waiter = new Func<IWebElement, bool>((IWebElement ele) =>
            //{
            //    String styleAttrib = element.GetAttribute("style");
            //    if (styleAttrib.Contains("red"))
            //    {
            //        return true;
            //    }
            //    Console.WriteLine("Color is still " + styleAttrib);
            //    return false;
            //});
            //wait.Until(waiter);



            //var homePage = new HomePage();
            //PageFactory.InitElements(driver, homePage);
            //homePage.MyAccount.Click();

            //var loginPage = new LoginPage();
            //PageFactory.InitElements(driver, loginPage);
            //loginPage.UserName.SendKeys("TestUser_1");
            //loginPage.Password.SendKeys("Test@123");
            //loginPage.Submit.Submit();


            //[FindsBy(How = How.Id, Using = "pwd")]
            //[CacheLookup]
            //public IWebElement Password { get; set; }


            driver.Quit();
        }
    }
}
