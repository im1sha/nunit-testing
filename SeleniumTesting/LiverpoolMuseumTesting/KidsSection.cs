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
    public class KidsSection
    {
        private static IWebDriver driver;

        [SetUp]
        public void Initiaize()
        {
            driver = new ChromeDriver();
        }

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
                kidsAdd = driver.FindElement(By.ClassName("kids-cta is-shown"));
            }
            catch
            {
            }

            Assert.AreEqual(kidsAdd, null);
        }

        [Test]
        public void GoToKidsPage()
        {
            driver.Url = "http://www.liverpoolmuseums.org.uk/";

            driver.Manage().Timeouts().ImplicitWait = new TimeSpan(0, 0, 30);

            string xpathOfKidsBlock = @"/html/body/form[@id='form1']/div[@class='wrapper home']/div[@class='wrapper-inner']/div[@class='kids-cta is-shown']/a";
            IWebElement kidsBlock = driver.FindElement(By.XPath(xpathOfKidsBlock));
            kidsBlock.Click();

            Assert.AreEqual(driver.Url, @"http://www.liverpoolmuseums.org.uk/kids/" +
                @"?utm_source=NMLwebsitehomepage&utm_medium=website&utm_campaign=kids");
        }

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

            string[][] rightUrls = new string[][] {
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
                CheckOneTheme(driver, ids[i], rightUrls[i]);
            }
                    
        }

        private bool CompareUrls(string url, string [] urlsToCompare)
        {
            bool result = false;
            foreach (var u in urlsToCompare)
            {
                if (u == url)
                {
                    result = true;
                }
            }
            return result;
        }

        private void CheckOneTheme(IWebDriver driver, string themeId,  string[] urlsToCompare)
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
