using BookingProject.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace BookingProject.Tests
{
    [TestFixture]
    public class FilterTest
    {
        private IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            driver = new ChromeDriver(options);
        }

        [TestCase("Минск")]
        public void CheckFilter_WithCityAndDatesAndTwoAdultsOneChildOneNumber(string city)
        {
            FilterElement filter = new FilterElement(driver);
            filter.GoToPage();

            filter.TypeCityName(city).ChooseDates_7DaysFromTodayAndPlus2Days().ChoosePeople_TwoAdultsOneChildOneNumber().ClickCheckPricesButton();

            Assert.IsTrue(filter.IsFilterWorked(city), "Filter has not worked properly.");
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}