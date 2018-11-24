using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiverpoolMuseumTesting
{
    /// <summary>
    /// Tests registration page at http://www.liverpoolmuseums.org.uk/register/
    /// </summary>
    class SignUpForEmails
    {
        private static IWebDriver driver;

        [SetUp]
        public void Initiaize()
        {
            driver = new ChromeDriver();
        }

        /// <summary>
        /// Tests registration form when no data providing 
        /// </summary>
        [Test]
        public void Submit()
        {
            driver.Url = "http://www.liverpoolmuseums.org.uk/register/";

            driver.Manage().Timeouts().ImplicitWait = new TimeSpan(0, 0, 10);

            IWebElement errorsSection = null;
            try
            {
                // try to find error section before registration, it shouldn't exist
                errorsSection = driver.FindElement(By.XPath("/html/body/form[@id='form1']/div[@class='wrapper']/div[@class='wrapper-inner']/div[@class='content page']/div[@class='container-fluid']/div[@class='row-fluid']/div[@class='span9']/div[@class='panel']/div[@class='page-content content-container']/div[@id='regFormPnl']/div[@id='valSumm1']/ul"));
            }
            catch 
            {
            }
            Assert.AreEqual(true, errorsSection == null);

            // submit buttom
            IWebElement button = driver.FindElement(By.XPath("/html/body/form[@id='form1']/div[@class='wrapper']/div[@class='wrapper-inner']/div[@class='content page']/div[@class='container-fluid']/div[@class='row-fluid']/div[@class='span9']/div[@class='panel']/div[@class='page-content content-container']/div[@id='regFormPnl']/fieldset[6]/input[@id='submitRegBtn']"));
            button.Submit();

            // errors section should appear on page
            Assert.AreEqual(true, driver.FindElement(By.XPath("/html/body/form[@id='form1']/div[@class='wrapper']/div[@class='wrapper-inner']/div[@class='content page']/div[@class='container-fluid']/div[@class='row-fluid']/div[@class='span9']/div[@class='panel']/div[@class='page-content content-container']/div[@id='regFormPnl']/div[@id='valSumm1']/ul")) != null);
        }

        /// <summary>
        /// Tests try to registrate not existing mail 
        /// </summary>
        [Test]
        public void WrongMail()
        {
            driver.Url = "http://www.liverpoolmuseums.org.uk/register/";

            driver.Manage().Timeouts().ImplicitWait = new TimeSpan(0, 0, 10);

            // fill name, surname, mail sections
            IWebElement firstName = driver.FindElement(By.XPath("/html/body/form[@id='form1']/div[@class='wrapper']/div[@class='wrapper-inner']/div[@class='content page']/div[@class='container-fluid']/div[@class='row-fluid']/div[@class='span9']/div[@class='panel']/div[@class='page-content content-container']/div[@id='regFormPnl']/fieldset[1]/div[@class='fieldset']/div[1]/div[@class='formfield']/input[@id='firstNameTB']"));
            firstName.SendKeys("ivanov");
            IWebElement lastName = driver.FindElement(By.XPath("/html/body/form[@id='form1']/div[@class='wrapper']/div[@class='wrapper-inner']/div[@class='content page']/div[@class='container-fluid']/div[@class='row-fluid']/div[@class='span9']/div[@class='panel']/div[@class='page-content content-container']/div[@id='regFormPnl']/fieldset[1]/div[@class='fieldset']/div[2]/div[@class='formfield']/input[@id='surnameTB']"));
            lastName.SendKeys("ivanov");
            IWebElement mail = driver.FindElement(By.Id("emailTB"));
            mail.SendKeys("ivan@ivanov.com");

            // mark required checkboxes
            driver.FindElement(By.Id("confirmationCB")).Click();
            driver.FindElement(By.XPath("/html/body/form[@id='form1']/div[@class='wrapper']/div[@class='wrapper-inner']/div[@class='content page']/div[@class='container-fluid']/div[@class='row-fluid']/div[@class='span9']/div[@class='panel']/div[@class='page-content content-container']/div[@id='regFormPnl']/fieldset[3]/div[@class='fieldset']/div/div[@class='formfield']/span[@id='interestsChkBxLst']/label[3]")).Click();
            driver.FindElement(By.XPath("/html/body/form[@id='form1']/div[@class='wrapper']/div[@class='wrapper-inner']/div[@class='content page']/div[@class='container-fluid']/div[@class='row-fluid']/div[@class='span9']/div[@class='panel']/div[@class='page-content content-container']/div[@id='regFormPnl']/fieldset[2]/div[@class='fieldset']/div/div[4]/label[@id='ceiLabel']")).Click();

            IWebElement button = driver.FindElement(By.XPath("/html/body/form[@id='form1']/div[@class='wrapper']/div[@class='wrapper-inner']/div[@class='content page']/div[@class='container-fluid']/div[@class='row-fluid']/div[@class='span9']/div[@class='panel']/div[@class='page-content content-container']/div[@id='regFormPnl']/fieldset[6]/input[@id='submitRegBtn']"));
            button.Submit();

            List<IWebElement> textSuccess = null;
            try
            {
                // try to find text that confirms registration
                textSuccess = driver.FindElements(By.TagName("p")).Where(i => i.Text == "Thank you for registering your details with us.").ToList();
            }
            catch 
            {
   
            }

            Assert.AreEqual(true, textSuccess != null);
        }


        [TearDown]
        public void Close()
        {
            driver.Quit();
        }

    }
}
