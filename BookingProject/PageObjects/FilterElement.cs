using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace BookingProject.PageObjects
{
    public class FilterElement
    {
        private readonly IWebDriver driver;

        private readonly By cityInputBy = By.Id("ss");
        private readonly By datesfieldBy = By.ClassName("xp__dates-inner");
        private readonly By datesListBy = By.CssSelector("#frm div.xp-calendar table>tbody>tr>td[data-date]");
        private readonly By peopleFieldBy = By.Id("xp__guests__toggle");
        private readonly By childrenAddButtonBy = By.CssSelector("#xp__guests__inputs-container>div>div>div:nth-of-type(2)>div>div:nth-of-type(2)>button:nth-of-type(2)");
        private readonly By checkPricesButtonBy = By.CssSelector("button[data-sb-id='main'");

        public FilterElement(IWebDriver _driver)
        {
            driver = _driver;
        }

        public void GoToPage()
        {
            driver.Navigate().GoToUrl(@"https://www.booking.com/");
        }

        public FilterElement TypeCityName(string city)
        {
            driver.FindElement(cityInputBy).SendKeys(city);
            return this;
        }

        public FilterElement ChooseDates_7DaysFromTodayAndPlus2Days()
        {
            var datesField = driver.FindElement(datesfieldBy);
            datesField.Click();

            string checkinDate = DateTime.Now.AddDays(7).ToString("yyyy-MM-dd");
            string checkoutDate = DateTime.Now.AddDays(9).ToString("yyyy-MM-dd");
            var datesList = driver.FindElements(datesListBy);

            foreach (var dateTd in datesList)
            {
                string dateString = dateTd.GetAttribute("data-date");

                if (dateString.Equals(checkinDate))
                {
                    dateTd.Click();
                    continue;
                }

                if (dateString.Equals(checkoutDate))
                {
                    dateTd.Click();
                    datesField.Click();
                    break;
                }
            }

            return this;
        }

        public FilterElement ChoosePeople_TwoAdultsOneChildOneNumber()
        {
            var peopleField = driver.FindElement(peopleFieldBy);
            peopleField.Click();

            driver.FindElement(childrenAddButtonBy).Click();

            peopleField.Click();

            return this;
        }

        public void ClickCheckPricesButton()
        {
            driver.FindElement(checkPricesButtonBy).Click();
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(e => e.Url.Contains("searchresults"));
        }

        public bool IsFilterWorked(string city)
        {
            string url = System.Web.HttpUtility.UrlDecode(driver.Url);
            DateTime checkinDate = DateTime.Now.AddDays(7);
            DateTime checkoutDate = DateTime.Now.AddDays(9);
            bool isIncorrectDates = !url.Contains($"checkin_year={checkinDate.Year}&checkin_month={checkinDate.Month}&checkin_monthday={checkinDate.Day}") ||
                            !url.Contains($"checkout_year={checkoutDate.Year}&checkout_month={checkoutDate.Month}&checkout_monthday={checkoutDate.Day}");

            if (!url.Contains($"ss={city}") || isIncorrectDates || !url.Contains($"group_adults={2}&group_children={1}&no_rooms={1}"))
            {
                return false;
            }

            return true;
        }
    }
}
