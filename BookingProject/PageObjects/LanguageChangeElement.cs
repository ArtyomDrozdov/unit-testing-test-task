using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace BookingProject.PageObjects
{
    public class LanguageChangeElement
    {
        private readonly IWebDriver driver;

        private readonly By languageButtonBy = By.CssSelector("button[data-modal-id=\"language-selection\"]");
        private readonly By languageListBy = By.CssSelector("div.bui-modal__inner");
        private readonly By newLanguageLinksBy = By.CssSelector("ul>li.bui-grid__column.bui-grid__column-3>a[data-lang]");

        public LanguageChangeElement(IWebDriver _driver)
        {
            driver = _driver;
        }

        public void GoToPage()
        {
            driver.Navigate().GoToUrl(@"https://www.booking.com/");
        }

        public HomePage ChangeLanguage(string newLanguage)
        {
            IWebElement languageList = ClickLanguageButton();

            return ChooseNewLanguage(languageList, newLanguage);
        }

        private IWebElement ClickLanguageButton()
        {
            driver.FindElement(languageButtonBy).Click();

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            return wait.Until(e => e.FindElement(languageListBy));
        }

        private HomePage ChooseNewLanguage(IWebElement languageList, string newLanguage)
        {
            var languageLinks = languageList.FindElements(newLanguageLinksBy);
            foreach (var languageLink in languageLinks)
            {
                if (languageLink.GetAttribute("data-lang") == newLanguage)
                {
                    languageLink.Click();
                    break;
                }
            }

            return new HomePage(driver);
        }
    }
}
