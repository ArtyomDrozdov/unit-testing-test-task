using BookingProject.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace BookingProject.Tests
{
    [TestFixture]
    public class LanguageChangeTest
    {
        private IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            driver = new ChromeDriver(options);
        }

        [TestCase("en-us")]
        public void ChangeLanguage(string newLanguage)
        {
            LanguageChangeElement element = new LanguageChangeElement(driver);

            element.GoToPage();

            HomePage page = element.ChangeLanguage(newLanguage);

            Assert.IsTrue(page.IsNewLanguage(newLanguage), $"Language is not changed to {newLanguage}");
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}