using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace BookingProject.PageObjects
{
    public class CurrencyChangeElement
    {
        private readonly IWebDriver driver;

        private readonly By currencyButtonBy = By.XPath("//header/nav[1]/div[2]/div[1]/button");
        private readonly By currentCurrencyBy = By.CssSelector("span.bui-button__text>span:nth-child(1)");
        private readonly By currencyListBy = By.CssSelector("div.bui-modal__inner");
        private readonly By newCurrencyLinksBy = By.CssSelector("ul>li.bui-grid__column.bui-grid__column-3>a");
        private readonly By newCurrencyNameBy = By.CssSelector("div.bui-traveller-header__currency");

        public CurrencyChangeElement(IWebDriver _driver)
        {
            driver = _driver;
        }

        public void GoToPage()
        {
            driver.Navigate().GoToUrl(@"https://www.booking.com/");
        }

        public HomePage ChangeCurrency(string newCurrency)
        {
            IWebElement currencyList = ClickCurrencyButton();

            return ChooseNewCurrency(currencyList, newCurrency);
        }

        private IWebElement ClickCurrencyButton()
        {
            driver.FindElement(currencyButtonBy).Click();

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            return wait.Until(e => e.FindElement(currencyListBy));
        }

        private HomePage ChooseNewCurrency(IWebElement currencyList, string newCurrency)
        {
            var currencyLinks = currencyList.FindElements(newCurrencyLinksBy);
            foreach (var currencyLink in currencyLinks)
            {
                var currencyName = currencyLink.FindElement(newCurrencyNameBy).Text;
                if (currencyName == newCurrency)
                {
                    var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
                    currencyLink.Click();
                    wait.Until(dr => dr.FindElement(currentCurrencyBy).Text == newCurrency);
                    break;
                }
            }

            return new HomePage(driver);
        }
    }
}
