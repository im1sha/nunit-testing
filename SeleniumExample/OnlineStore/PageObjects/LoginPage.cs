using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.PageObjects
{
    public class LoginPage
    {
        private IWebDriver driver;

        [FindsBy(How = How.Id, Using = "log")]
        [CacheLookup]
        public IWebElement UserName { get; set; }

        [FindsBy(How = How.Id, Using = "pwd")]
        [CacheLookup]
        public IWebElement Password { get; set; }

        [FindsBy(How = How.Id, Using = "login")]
        [CacheLookup]
        public IWebElement Submit { get; set; }
    }
}
