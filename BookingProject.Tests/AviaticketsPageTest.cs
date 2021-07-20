using BookingProject.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace BookingProject.Tests
{
    [TestFixture]
    public class AviaticketsPageTest
    {
        private IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            driver = new ChromeDriver(options);
        }

        [Test]
        public void GoToAviaticketPage()
        {
            HomePage homePage = new HomePage(driver);
            homePage.GoToPage();

            AviaticketsPage aviaticketsPage = homePage.GoToAviaticketsPage();

            Assert.IsTrue(aviaticketsPage.IsAviaticketsPage(), "It is not aviatickets page.");
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}