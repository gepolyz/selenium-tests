
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumDemo.Pages;
using NUnit.Framework;



namespace SeleniumDemo.Tests
{
    public class LoginTest
    {
        private IWebDriver driver = null!;
        private LoginPage loginPage = null!;

        [SetUp]
        public void Setup()
        {
            var options = new ChromeOptions();
            
            options.AddArgument("--headless"); //Run Chrome in headless mode
            options.AddArgument("--window-size=1920,1080");
            options.AddArgument("--disable-gpu");
            options.AddArgument("--no-sandbox");
            
            ChromeDriverService service;

            // Checks in what Operating System are waorking
            if (OperatingSystem.IsWindows())
            {
                // Let Selenium find chromedriver.exe in PATH (you must install it yourself)
                service = ChromeDriverService.CreateDefaultService();
            }
            else if (OperatingSystem.IsLinux())
            {
                // Use manually installed path on GitHub Actions
                service = ChromeDriverService.CreateDefaultService("/usr/local/bin");
            }
            else
            {
                throw new PlatformNotSupportedException("Unsupported OS for ChromeDriver setup");
            }

            driver = new ChromeDriver(service, options);

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
        public void Logout_AfterLogin_ShouldReturnToLoginPage()
        {
            loginPage.EnterUserName("Admin");
            loginPage.EnterPassword("admin123");
            loginPage.ClickLogin();
            loginPage.WaitForDashboard();
            loginPage.ClickUserDropDown();

            loginPage.ClickLogout();
            Assert.That(loginPage.WaitForLoginPage(), Is.True);   
        }

        [Test]
        public void Login_With_Blank_Credentials()
        {
            loginPage.EnterUserName("");
            loginPage.EnterPassword("");
            loginPage.ClickLogin();
            Assert.That(loginPage.GetAlertForContent().Contains("Required"));
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