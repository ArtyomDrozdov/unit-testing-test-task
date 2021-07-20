using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;

namespace BookingProject.PageObjects
{
    public class HomePage
    {
        private readonly IWebDriver driver;

        private readonly By aviaticketsTab = By.CssSelector("ul.bui-tab__nav>li:nth-child(2)>a");
        private readonly By enterInAccountButtonBy = By.CssSelector("nav.bui-header__bar>div:nth-of-type(2)>div:nth-of-type(6)>a");
        private readonly By accountButtonBy = By.CssSelector("#b2indexPage>header>nav.bui-header__bar>div:nth-child(2)>div:nth-child(6)>div>a");
        private readonly By manageAccountButtonBy = By.CssSelector("nav.bui-header__bar ul.bui-dropdown-menu__items>li:nth-child(1)>a");

        public HomePage(IWebDriver _driver)
        {
            driver = _driver;
        }

        public void GoToPage()
        {
            driver.Navigate().GoToUrl("https://www.booking.com/");
        }

        public AviaticketsPage GoToAviaticketsPage()
        {
            driver.FindElement(aviaticketsTab).Click();
            return new AviaticketsPage(driver);
        }

        public AuthorizationPage ClickEnterInAccountButton()
        {
            driver.FindElement(enterInAccountButtonBy).Click();
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(dr => dr.Url.StartsWith("https://account.booking.com/"));

            return new AuthorizationPage(driver);
        }

        public UserAccountPage EnterUserAccount()
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            Actions actions = new Actions(driver);
            GoToPage();

            actions.MoveToElement(driver.FindElement(By.TagName("body"))).SendKeys(Keys.Escape).Perform();
            wait.Until(dr => dr.Url.StartsWith("https://www.booking.com/"));

            wait.Until(dr => dr.FindElement(accountButtonBy)).Click();
            driver.FindElement(manageAccountButtonBy).Click();
            wait.Until(dr => dr.Url.StartsWith("https://account.booking.com/mysettings"));

            return new UserAccountPage(driver);
        }

        public bool IsNewCurrency(string currency) => driver.Url.Contains($"selected_currency={currency}");

        public bool IsNewLanguage(string language) => driver.Url.Contains($"lang={language}");
    }
}
