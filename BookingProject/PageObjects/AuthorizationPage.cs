using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace BookingProject.PageObjects
{
    public class AuthorizationPage
    {
        private readonly IWebDriver driver;

        private readonly By userNameInputBy = By.Id("username");
        private readonly By passwordInputBy = By.Id("password");
        private readonly By continueWithEmailButtonBy = By.CssSelector("form.nw-signin>div:nth-of-type(3)>button");
        private readonly By confirmButtonBy = By.CssSelector("form.nw-signin>button");

        public AuthorizationPage(IWebDriver _driver)
        {
            driver = _driver;
        }

        public void GoToPage()
        {
            driver.Navigate().GoToUrl(@"https://www.booking.com/");
        }

        public HomePage LogIn(string email, string password)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            wait.IgnoreExceptionTypes(new[] { typeof(NoSuchElementException) });

            wait.Until(dr => dr.FindElement(userNameInputBy)).SendKeys(email);
            driver.FindElement(continueWithEmailButtonBy).Click();

            var passwordInput = wait.Until(dr => dr.FindElement(passwordInputBy));
            passwordInput.SendKeys(password);
            driver.FindElement(confirmButtonBy).Click();

            return new HomePage(driver);
        }
    }
}
