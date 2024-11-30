using NuGet.Frameworks;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saucedemo.Pages.LoginPage
{
    internal class LoginPage : WebPage
    {
        // Locators
        private readonly By InputUsernameField = By.Id("user-name");
        private readonly By InputPasswordField = By.Id("password");
        private readonly By InputLoginButton = By.Id("login-button");
        private readonly By TextErrorMessage = By.CssSelector(".error-message-container.error");
        private const string ErromMessageInvalidUsernameOrPass = "Epic sadface: Username and password do not match any user in this service";
        private const string ErromMessageEmptyUsername = "Epic sadface: Username is required";
        private const string ErromMessagEmptyPass = "Epic sadface: Password is required";

        private const string mainUrl = "https://www.saucedemo.com/";

        // Methods
        public LoginPage(IWebDriver driver) : base(driver) {}

        private void InputUsername(string username)
        {
            Type(InputUsernameField, username);
        }

        private void InputPassword(string password)
        {
            Type(InputPasswordField, password);
        }

        private void ClickLoginButton()
        {
              Click(InputLoginButton);
        }

        private string GetErrorMessage()
        {
            return GetText(TextErrorMessage);
        }

        public void NavigateToLoginPage()
        {
            OpenWebPage(mainUrl);
        }

        public void LogIn(string username, string password)
        {
            InputUsername(username);
            InputPassword(password);
            ClickLoginButton();
        }

        // Asserts
        public void AssertThatLoginPageIsOpenSuccessfuly()
        {
            Assert.DoesNotThrow(() =>
            {
                Assert.That(IsElementDispalyed(InputUsernameField), Is.EqualTo(true));
            }, "Error: Username field is not displayed!");
            Assert.DoesNotThrow(() =>
            {
                Assert.That(IsElementDispalyed(InputUsernameField), Is.EqualTo(true));
            }, "Error: Password field is not displayed!");
            Assert.DoesNotThrow(() =>
            {
                Assert.That(IsElementDispalyed(InputLoginButton), Is.EqualTo(true));
            }, "Error: Login button is not displayed!");
        }

        public void AssertThatErrorMessageWrongCredentialsIsDispalyed()
        {
            Assert.DoesNotThrow(() =>
            {
                Assert.That(GetAttribute(TextErrorMessage, "innerText"), Is.EqualTo(ErromMessageInvalidUsernameOrPass));
            }, $"Error: Error message '{ErromMessageInvalidUsernameOrPass}' is not displayed!");
           
        }

        public void AssertThatErrorMessageEmptyUsernameIsDispalyed()
        {
            Assert.DoesNotThrow(() =>
            {
                Assert.That(GetAttribute(TextErrorMessage, "innerText"), Is.EqualTo(ErromMessageEmptyUsername));
            }, $"Error: Error message '{ErromMessageEmptyUsername}' is not displayed!");

        }

        public void AssertThatErrorMessageEmptyPasswordIsDispalyed()
        {
            Assert.DoesNotThrow(() =>
            {
                Assert.That(GetAttribute(TextErrorMessage, "innerText"), Is.EqualTo(ErromMessagEmptyPass));
            }, $"Error: Error message '{ErromMessagEmptyPass}' is not displayed!");
        }
    }
}
