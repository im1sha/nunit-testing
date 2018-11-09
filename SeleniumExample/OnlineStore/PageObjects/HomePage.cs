using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.PageObjects
{
    class HomePage
    {
        private IWebDriver driver;

        [FindsBy(How = How.Id, Using = "account")]
        public IWebElement MyAccount { get; set; }
    }
}
