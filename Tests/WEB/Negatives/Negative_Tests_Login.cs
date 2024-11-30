using Saucedemo.Credentials;
using Saucedemo.Utils;

namespace Saucedemo.Tests.WEB.Negatives
{
    [TestFixture(Browser.Chrome)]
    [Parallelizable]
    [Category("NegativeTests")]
    internal class Negative_Tests_Login : WebTest
    {
        public Negative_Tests_Login(Browser browser) : base(browser) {}

        [TestCase(User.validUsername, User.invalidPassword)]
        [TestCase(User.invalidUsername, User.validPassword)]
        [TestCase(User.whiteSpacesUsername, User.validPassword)]
        [TestCase(User.validUsername, User.whiteSpacesPassword)]
        [TestCase(User.invalidUsername, User.invalidPassword)]
        [TestCase(User.whiteSpacesUsername, User.whiteSpacesPassword)]
        public void ErrorMessage_Unsuccessfylly_Login_With_Invalid_Credentials_Is_Dispalyed(string username, string password)
        {
            _loginPage.NavigateToLoginPage();
            _loginPage.AssertThatLoginPageIsOpenSuccessfuly();
            _loginPage.LogIn(username, password);
            _loginPage.AssertThatErrorMessageWrongCredentialsIsDispalyed();
        }

        [Test]
        public void ErrorMessage_For_Reduired_Username_Field_Is_Displayed()
        {
            _loginPage.NavigateToLoginPage();
            _loginPage.AssertThatLoginPageIsOpenSuccessfuly();
            _loginPage.LogIn(User.emptyUsername, User.validPassword);
            _loginPage.AssertThatErrorMessageEmptyUsernameIsDispalyed();

        }

        [Test]
        public void ErrorMessage_For_Reduired_Password_Field_Is_Displayed()
        {
            _loginPage.NavigateToLoginPage();
            _loginPage.AssertThatLoginPageIsOpenSuccessfuly();
            _loginPage.LogIn(User.validUsername, User.emptyUsername);
            _loginPage.AssertThatErrorMessageEmptyPasswordIsDispalyed();
        }
    }
}
