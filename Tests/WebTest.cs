using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using System.Text.RegularExpressions;
using Saucedemo.Utils;
using Saucedemo.Pages.LoginPage;
using Saucedemo.Pages.Inventory;
using Saucedemo.Pages.Cart;
using Saucedemo.Pages.Checkout;
using Saucedemo.Pages.Complete;

namespace Saucedemo.Tests
{
    internal abstract class WebTest
    {
        private Browser browser;
        public WebTest(Browser browser)
        {
            this.browser = browser;
        }

        protected IWebDriver Driver;
        protected LoginPage _loginPage;
        protected InventoryPage _inventoryPage;
        protected CartPage _cartPage;
        protected CheckoutPage _checkoutPage;
        protected CompletePage _completePage;

        [SetUp]
        public void SetUp()
        {
            try
            {
                Driver = WebDriverFactory.CreateWebDriver(browser);
                Driver.Manage().Window.Maximize();
                Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
            }
            catch (WebDriverException ex)
            {
                Driver?.Quit();
                Driver?.Dispose();
                Driver = WebDriverFactory.CreateWebDriver(browser);
                Driver.Manage().Window.Maximize();
                Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
                Console.WriteLine($"WebDriver execption: {ex.Message}\nStackTrace: {ex.StackTrace}");
            }
            catch (Exception ex)
            {
                Driver?.Quit();
                Driver?.Dispose();
                Console.WriteLine($"WebDriver is not started!.\nMessage: {ex.Message} \nStackTrace {ex.StackTrace}");
            }

            _loginPage = new LoginPage(Driver);
            _inventoryPage = new InventoryPage(Driver);
            _cartPage = new CartPage(Driver);
            _checkoutPage = new CheckoutPage(Driver);
            _completePage = new CompletePage(Driver);
        }

        [TearDown]
        public void TearDown()
        {
            if (Driver != null)
            {
                if (TestContext.CurrentContext.Result.Outcome.Status != TestStatus.Passed)
                {
                    string fileName = Regex.Replace(TestContext.CurrentContext.Test.Name, "[^a-zA-Z0-9_]+", "");
                    var path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
                    var ss = ((ITakesScreenshot)Driver).GetScreenshot();
                    ss.SaveAsFile(path + "\\" + fileName + DateTime.Now.ToString("dd-MM-yyyy") + ".png");

                    Driver.Quit();
                    Driver.Dispose();
                }
                else
                {
                    Driver.Quit();
                    Driver.Dispose();
                }
            }
        }
    }
}
