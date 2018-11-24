using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace LiverpoolMuseumTesting
{
    /// <summary>
    /// Tests kids content http://www.liverpoolmuseums.org.uk/kids/ and banners which redirect to it
    /// </summary>
    public class KidsSection
    {
        private static IWebDriver driver;

        [SetUp]
        public void Initiaize()
        {
            driver = new ChromeDriver();
        }

        /// <summary>
        /// Tries close banner
        /// </summary>
        [Test]
        public void CloseKidsSectionAdd()
        {

            driver.Url = "http://www.liverpoolmuseums.org.uk/";
         
            IWebElement kidsAdd = driver.FindElement(By.ClassName("kids-cta"));

            IWebElement hideButton = driver.FindElement(By.ClassName("kids-cta-hide-button"));
            hideButton.Click();

            try
            {
                kidsAdd = null;
                kidsAdd = driver.FindElement(By.ClassName("kids-cta is-shown")); // try to find banner
            }
            catch
            {
            }

            Assert.AreEqual(kidsAdd, null);
        }

        /// <summary>
        /// Checks redirecting when user clicks add
        /// </summary>
        [Test]
        public void GoToKidsPage()
        {
            driver.Url = "http://www.liverpoolmuseums.org.uk/";

            driver.Manage().Timeouts().ImplicitWait = new TimeSpan(0, 0, 30);

            // banner's xpath
            string xpathOfKidsBlock = @"/html/body/form[@id='form1']/div[@class='wrapper home']/div[@class='wrapper-inner']/div[@class='kids-cta is-shown']/a";
            IWebElement kidsBlock = driver.FindElement(By.XPath(xpathOfKidsBlock));
            kidsBlock.Click();

            Assert.AreEqual(driver.Url, @"http://www.liverpoolmuseums.org.uk/kids/" +
                @"?utm_source=NMLwebsitehomepage&utm_medium=website&utm_campaign=kids");
        }

        /// <summary>
        /// Tests thems changing
        /// </summary>
        [Test]
        public void ChangeTheme()
        {
            driver.Url = "http://www.liverpoolmuseums.org.uk/kids/";

            driver.Manage().Timeouts().ImplicitWait = new TimeSpan(0, 0, 30);

            string[] ids = {
                "persistBackgroundImage_spaceLink",
                "persistBackgroundImage_jungleLink",
                "persistBackgroundImage_egyptLink"
            };

            string[][] correctUrls = new string[][] {
                new string[] {
                    @"http://www.liverpoolmuseums.org.uk/kids/?theme=space",
                    @"http://www.liverpoolmuseums.org.uk/kids/?&theme=space"
                },
                new string[] {
                    @"http://www.liverpoolmuseums.org.uk/kids/?&theme=jungle",
                    @"http://www.liverpoolmuseums.org.uk/kids/?theme=jungle"
                },
                new string[] {
                    @"http://www.liverpoolmuseums.org.uk/kids/?&theme=egypt",
                    @"http://www.liverpoolmuseums.org.uk/kids/?theme=egypt"
                }
            };

            for (int i = 0; i < ids.Length; i++)
            {
                CheckOneTheme(driver, ids[i], correctUrls[i]);
            }                   
        }

        /// <summary>
        /// Compares passed URLs
        /// </summary>
        /// <param name="url">URL to compare with</param>
        /// <param name="urlsToCompare">Comparing URLs</param>
        /// <returns>True if urlsToCompare contain url</returns>
        private bool CompareUrls(string url, string [] urlsToCompare)
        {
            bool result = false;
            foreach (string u in urlsToCompare)
            {
                if (u == url)
                {
                    result = true;
                }
            }
            return result;
        }

        /// <summary>
        /// Checks if now correct theme is showing
        /// </summary>
        /// <param name="driver">Working driver</param>
        /// <param name="themeId">Id item's to click</param>
        /// <param name="urlsToCompare">Correct URLs</param>
        private void CheckOneTheme(IWebDriver driver, string themeId, string[] urlsToCompare)
        {
            IWebElement theme = driver.FindElement(By.Id(themeId));
            theme.Click();
            Assert.AreEqual(true, CompareUrls(driver.Url, urlsToCompare));
        }

        [TearDown]
        public void Close()
        {
            driver.Quit();
        }
    }
}
