
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;


namespace SeleniumDemo.Pages
{
    public class LoginPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
        }

        //Locators
        private IWebElement UserNameInput => _driver.FindElement(By.Name("username"));
        private IWebElement PasswordInput => _driver.FindElement(By.Name("password"));
        private IWebElement LoginButton => _driver.FindElement(By.TagName("button"));
        private By ErrorMessage => By.CssSelector(".oxd-alert-content-text");

        //Actions
        public void EnterUserName(string username) => UserNameInput.SendKeys(username);
        public void EnterPassword(string password) => PasswordInput.SendKeys(password);
        public void ClickLogin() => LoginButton.Click();

        //Waits
        public bool WaitForDashboard()
        {
            return _wait.Until(ExpectedConditions.UrlContains("dashboard"));
        }
        public string GetErrorMessage()
        {
            _wait.Until(ExpectedConditions.ElementIsVisible(ErrorMessage));
            return _driver.FindElement(ErrorMessage).Text;
        }
    }
}