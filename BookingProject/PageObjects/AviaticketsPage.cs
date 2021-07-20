using OpenQA.Selenium;

namespace BookingProject.PageObjects
{
    public class AviaticketsPage
    {
        private readonly IWebDriver driver;

        public AviaticketsPage(IWebDriver _driver)
        {
            driver = _driver;
        }

        public bool IsAviaticketsPage()
        {
            string url = driver.Url;
            return url.StartsWith("https://booking.kayak.com/");
        }
    }
}
