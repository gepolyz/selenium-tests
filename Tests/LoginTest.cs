
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumDemo.Pages;
using NUnit.Framework;
using System;
using NUnit.Framework.Legacy;
using OpenQA.Selenium.Support.UI;


namespace SeleniumDemo.Tests
{
    public class LoginTest
    {
        private IWebDriver driver;
        private LoginPage loginPage;

        [SetUp]
        public void Setup()
        {
            var options = new ChromeOptions();
            options.AddArgument("--headless"); //Run Chrome in headless mode
            options.AddArgument("--window-size=1920,1080");
            options.AddArgument("--disable-gpu");
            options.AddArgument("--no-sandbox");

            driver = new ChromeDriver(options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Navigate().GoToUrl("https://opensource-demo.orangehrmlive.com/web/index.php/auth/login");
            loginPage = new LoginPage(driver);
        }

        [Test]
        public void Login_WithValidCredentials_ShouldSucceed()
        {
            
            loginPage.EnterUserName("Admin");
            loginPage.EnterPassword("admin123");
            loginPage.ClickLogin();
            Assert.That(loginPage.WaitForDashboard(), Is.True);
        }

        [Test]
        [TestCase("ad", "123")]
        [TestCase("Admin", "ddd")]
        [TestCase("Jone", "admin123")]
        public void Login_WithInvalidCredentials_ShouldFail(string userName, string password)
        {
            loginPage.EnterUserName(userName);
            loginPage.EnterPassword(password);
            loginPage.ClickLogin();
            Assert.That(loginPage.GetErrorMessage().Contains("Invalid credentials"));
        } 

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}