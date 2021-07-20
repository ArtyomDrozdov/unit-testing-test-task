using BookingProject.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace BookingProject.Tests
{
    [TestFixture]
    public class AuthorizationTest
    {
        private IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--incognito");
            options.AddArgument("--start-maximized");
            options.AddArgument("--disable-popup-blocking");
            driver = new ChromeDriver(options);
        }

        [TestCase("bewevac308@nnacell.com", "Bewevac308")]
        public void CheckAccessToUserAccount(string email, string password)
        {
            HomePage homePage = new HomePage(driver);
            homePage.GoToPage();

            AuthorizationPage authPage = homePage.ClickEnterInAccountButton();
            homePage = authPage.LogIn(email, password);
            UserAccountPage accountPage = homePage.EnterUserAccount();

            Assert.IsTrue(accountPage.IsUserAccountPage(), $"It is not available user account for login={email}.");

            accountPage.LogOut();
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}