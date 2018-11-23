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

            for (int i = 0; i < totalSlides; i++)
            {
                Assert.AreEqual(slidesClasses[0][i], slidesClasses[totalSlides][i]);
            }

        }


        [TearDown]
        public void Close()
        {
            driver.Quit();
        }

    }
}
