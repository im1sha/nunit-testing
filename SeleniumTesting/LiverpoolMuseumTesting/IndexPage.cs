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
    class IndexPage
    {
        private static IWebDriver driver;

        [SetUp]
        public void Initiaize()
        {
            driver = new ChromeDriver();
        }

        [Test]
        public void CheckSlider()
        {
            driver.Url = "http://www.liverpoolmuseums.org.uk/";

            driver.Manage().Timeouts().ImplicitWait = new TimeSpan(0, 0, 30);

            int totalSlides = driver.FindElements(By.ClassName("owl-page")).Count();

            IWebElement nextButton = driver.FindElement(By.ClassName("owl-next"));

            List<List<string>> slidesClasses =
                new List<List<string>>(totalSlides + 1);

            for (int i = 0; i < totalSlides + 1; i++)
            {
                var items = driver.FindElements(By.ClassName("owl-page"));
                Assert.AreEqual(totalSlides, items.Count);
                slidesClasses.Add(new List<string>(totalSlides));
                for (int j = 0; j < items.Count; j++)
                {
                    slidesClasses[i].Add(items[j].GetAttribute("className"));
                }
                nextButton.Click();
            }

            string[] activePageClass = { "owl-page active", "active owl-page" };

            int activePosition = 0;
            for (int i = 0; i < totalSlides; i++)
            {
                if (slidesClasses[0][i] == activePageClass[0] || slidesClasses[0][i] == activePageClass[1])
                {
                    activePosition = i;
                    break;
                }
            }

            for (int i = 1; i < totalSlides + 1; i++)
            {
                activePosition = (activePosition + 1) % (totalSlides);
                bool result = (slidesClasses[i][activePosition] == activePageClass[0]) || 
                    (slidesClasses[i][activePosition] == activePageClass[1]);
                Assert.AreEqual(true, result);
            }
        }

        //[Test]
        //public void TestFindUs() {
        //    driver.Url = "http://www.liverpoolmuseums.org.uk/";

        //    driver.Manage().Timeouts().ImplicitWait = new TimeSpan(0, 0, 30);


        //}


        [TearDown]
        public void Close()
        {
            driver.Quit();
        }

    }
}
