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
    /// <summary>
    /// Checks if order pages work correctly http://www.liverpoolmuseums.org.uk/onlineshop/
    /// </summary>
    class Shop
    {

        private static IWebDriver driver;

        [SetUp]
        public void Initiaize()
        {
            driver = new ChromeDriver();
        }

        /// <summary>
        /// Adds element to basket and finds it on page containing basket
        /// </summary>
        [Test]
        public void AddToBasket()
        {
            driver.Url = "http://www.liverpoolmuseums.org.uk/onlineshop/venues/mol.aspx";

            driver.Manage().Timeouts().ImplicitWait = new TimeSpan(0, 0, 20);

            string path = "/html/body/form[@id='form1']/div[@class='wrapper home']/div[@class='wrapper-inner is-shop']/div[@class='content page']/div[@class='container-fluid']/div[@class='row-fluid']/div[@class='span9']/div[@class='panel']/div[@id='main-content']/div[@id='categoryHolder']/div[@id='venueProductsPanel']/div[@class='span12']/div[@class='whats-on-features events-activities']/div[@id='itemDiv'][2]/div[@class='product-title article-snippet']/p[2]/a[@id='addToCart']";
            IWebElement button = driver.FindElement(By.XPath(path));
            button.Click(); // add item to basket

            List<IWebElement> elements = null;
            try
            {
                // try to find this element on order page
                elements = driver.FindElements(By.TagName("input")).Where(i => i.GetAttribute("value") == "1" &&
                i.GetAttribute("name") == "ctl00$ctl00$PageContent$cart$cartRptr$ctl01$quantityTB").ToList();
            }
            catch 
            {
            }

            Assert.AreEqual(elements.Count, 1);
        }

        /// <summary>
        /// Selects item that contains multiselect section
        /// </summary>
        [Test]
        public void AddItemWithMultiselect()
        {
            driver.Url = "http://www.liverpoolmuseums.org.uk/onlineshop/gifts/liverpool-gifts/red-ceramic-liverbird.aspx";

            driver.Manage().Timeouts().ImplicitWait = new TimeSpan(0, 0, 10);

            // XPath of item with multiselect opportunity
            string path = "/html/body/form[@id='form1']/div[@class='wrapper home']/div[@class='wrapper-inner is-shop']/div[@class='content page']/div[@class='container-fluid']/div[@class='row-fluid']/div[@class='span9']/div[@class='panel']/div[@id='main-content']/div[@id='productHolder']/div[@class='shop-item-detail']/div[@id='productVariations']/div[@id='variationsUpdatePnl']/input[@id='addToCartButton']";

            List<IWebElement> buttonList = null;
            try
            {
                buttonList = driver.FindElements(By.XPath(path)).ToList();
            }
            catch 
            {
            }

            Assert.AreEqual(buttonList[0].Enabled, false); // check if we may order item with no selection
            Assert.AreEqual(buttonList.Count, 1);

            string pathToDroplist = "/html/body/form[@id='form1']/div[@class='wrapper home']/div[@class='wrapper-inner is-shop']/div[@class='content page']/div[@class='container-fluid']/div[@class='row-fluid']/div[@class='span9']/div[@class='panel']/div[@id='main-content']/div[@id='productHolder']/div[@class='shop-item-detail']/div[@id='productVariations']/div[@id='variationsUpdatePnl']/div[@id='colourVariationsPnl']/select[@id='colourVariationsDDL']";
            IWebElement element = driver.FindElement(By.XPath(pathToDroplist));
            SelectElement select = new SelectElement(element);
            select.SelectByText("red");

            var wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));

            // item must be clickable after color selection 
            var clickableElement = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(path)));

            Assert.AreEqual(clickableElement.Enabled, true);
        }


        /// <summary>
        /// Checks if order data stores correctly. Page works incorrect
        /// </summary>
        [Test]
        public void CookiesFailed()
        {
            // page containing 1 selected item
            driver.Url = "http://www.liverpoolmuseums.org.uk/onlineshop/cart.aspx?item=Floral+Liverbird+Coaster&id=2820&pr=4&wt=50&cat=gifts/for-the-home/floral-liverbird-coaster&isp=0&ins=0&md=15";

            driver.Manage().Timeouts().ImplicitWait = new TimeSpan(0, 0, 10);

            List<IWebElement> elements = null;    

            elements = driver.FindElements(By.TagName("input")).Where(i => i.GetAttribute("value").Length > 0 &&
                i.GetAttribute("name") == "ctl00$ctl00$PageContent$cart$cartRptr$ctl01$quantityTB").ToList();
            int total = int.Parse(elements[0].GetAttribute("value")); // total selected before update
         
            driver.Navigate().Refresh();

            var wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
            var clickableElement = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/form[@id='form1']/div[@class='wrapper home']/div[@class='wrapper-inner is-shop']/div[@class='content page']/div[@class='container-fluid']/div[@class='row-fluid']/div[@class='span9']/div[@class='panel']/div[@class='content-container']/div[@class='page-content']/div[@class='callout']/div[@class='form']/div[@id='cartContents']/table[@class='cartTable']/tbody/tr[@class='itemRow'][1]/td[@class='col2']/input[@id='quantityTB']")));
            int totalNew = int.Parse(clickableElement.GetAttribute("value")); // selected after update

            // Test fails
            Assert.AreNotEqual(totalNew, total);
        }


        [TearDown]
        public void Close()
        {
            driver.Quit();
        }

    }
}
