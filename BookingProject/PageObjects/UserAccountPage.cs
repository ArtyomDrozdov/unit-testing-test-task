using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace BookingProject.PageObjects
{
    public class UserAccountPage
    {
        private readonly IWebDriver driver;

        private readonly By userAccountButtonBy = By.CssSelector("nav.bui-header__bar>div:nth-of-type(2)>div:nth-of-type(4)>div>button");
        private readonly By signOutButtonBy = By.CssSelector("nav.bui-header__bar ul>li:nth-child(7)>button");

        public UserAccountPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void LogOut()
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(dr => dr.FindElement(userAccountButtonBy)).Click();
            driver.FindElement(signOutButtonBy).Click();
        }

        public bool IsUserAccountPage()
        {
            return driver.Url.StartsWith("https://account.booking.com/mysettings");
        }
    }
}
