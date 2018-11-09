using NUnit.Framework;
using OnlineStore.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.TestCases
{
    class LoginTest
    {
        [Test]
        public void Test()
        {
            IWebDriver driver = new ChromeDriver
            {
                Url = "http://www.store.demoqa.com"
            };


            var homePage = new HomePage();
            PageFactory.InitElements(driver, homePage);
            homePage.MyAccount.Click();

            var loginPage = new LoginPage();
            PageFactory.InitElements(driver, loginPage);
            loginPage.UserName.SendKeys("TestUser_1");
            loginPage.Password.SendKeys("Test@123");
            loginPage.Submit.Submit();

            driver.Quit();
        }
    }
}
