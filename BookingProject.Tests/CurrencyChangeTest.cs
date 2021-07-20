using BookingProject.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace BookingProject.Tests
{
    [TestFixture]
    public class CurrencyChangeTest
    {
        private IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            driver = new ChromeDriver(options);
        }

        [TestCase("USD")]
        public void ChangeCurrency(string newCurrency)
        {
            CurrencyChangeElement element = new CurrencyChangeElement(driver);

            element.GoToPage();

            HomePage page = element.ChangeCurrency(newCurrency);

            Assert.IsTrue(page.IsNewCurrency(newCurrency), $"Currency is not changed to {newCurrency}");
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}