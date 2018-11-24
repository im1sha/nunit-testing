using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiverpoolMuseumTesting
{
    class Shop
    {

        private static IWebDriver driver;

        [SetUp]
        public void Initiaize()
        {
            driver = new ChromeDriver();
        }


        [Test]
        public void AddToBasket()
        {
            driver.Url = "http://www.liverpoolmuseums.org.uk/onlineshop/venues/mol.aspx";

            driver.Manage().Timeouts().ImplicitWait = new TimeSpan(0, 0, 20);

            string path = "/html/body/form[@id='form1']/div[@class='wrapper home']/div[@class='wrapper-inner is-shop']/div[@class='content page']/div[@class='container-fluid']/div[@class='row-fluid']/div[@class='span9']/div[@class='panel']/div[@id='main-content']/div[@id='categoryHolder']/div[@id='venueProductsPanel']/div[@class='span12']/div[@class='whats-on-features events-activities']/div[@id='itemDiv'][2]/div[@class='product-title article-snippet']/p[2]/a[@id='addToCart']";
            IWebElement button = driver.FindElement(By.XPath(path));
            button.Click();

            List<IWebElement> elements = null;
            try
            {
                elements = driver.FindElements(By.TagName("input")).Where(i => i.GetAttribute("value") == "1" &&
                i.GetAttribute("name") == "ctl00$ctl00$PageContent$cart$cartRptr$ctl01$quantityTB").ToList();
            }
            catch 
            {
            }

            Assert.AreEqual(elements.Count, 1);
            Assert.AreNotEqual(elements, null);
        }


        [Test]
        public void AddItemWithMultiselect()
        {
            driver.Url = "http://www.liverpoolmuseums.org.uk/onlineshop/gifts/liverpool-gifts/red-ceramic-liverbird.aspx";

            driver.Manage().Timeouts().ImplicitWait = new TimeSpan(0, 0, 10);

            string path = "/html/body/form[@id='form1']/div[@class='wrapper home']/div[@class='wrapper-inner is-shop']/div[@class='content page']/div[@class='container-fluid']/div[@class='row-fluid']/div[@class='span9']/div[@class='panel']/div[@id='main-content']/div[@id='productHolder']/div[@class='shop-item-detail']/div[@id='productVariations']/div[@id='variationsUpdatePnl']/input[@id='addToCartButton']";


            List<IWebElement> buttonList = null;
            try
            {
                buttonList = driver.FindElements(By.XPath(path)).ToList();
            }
            catch 
            {

            }

            Assert.AreEqual(buttonList[0].Enabled, false);
            Assert.AreEqual(buttonList.Count, 1);

            string pathToDroplist = "/html/body/form[@id='form1']/div[@class='wrapper home']/div[@class='wrapper-inner is-shop']/div[@class='content page']/div[@class='container-fluid']/div[@class='row-fluid']/div[@class='span9']/div[@class='panel']/div[@id='main-content']/div[@id='productHolder']/div[@class='shop-item-detail']/div[@id='productVariations']/div[@id='variationsUpdatePnl']/div[@id='colourVariationsPnl']/select[@id='colourVariationsDDL']";
            IWebElement element = driver.FindElement(By.XPath(pathToDroplist));
            SelectElement select = new SelectElement(element);
            select.SelectByText("red");

            var wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
            var clickableElement = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(path)));

            Assert.AreEqual(clickableElement.Enabled, true);
            Assert.AreEqual(buttonList.Count, 1);

        }


        [TearDown]
        public void Close()
        {
            driver.Quit();
        }

    }
}
