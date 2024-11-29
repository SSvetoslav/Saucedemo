using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Saucedemo.Pages
{
    internal abstract class WebPage
    {
        private IWebDriver Driver { get; init; }
        private WebDriverWait Wait { get; set; }
        private Actions Actions { get; set; }

        private const int TIMEOUT = 10;

        public WebPage(IWebDriver driver)
        {
            this.Driver = driver ?? throw new ArgumentNullException(nameof(driver));
            this.Wait = new WebDriverWait(driver, TimeSpan.FromSeconds(TIMEOUT));
            this.Actions = new Actions(Driver);
        }

        protected void RefreshPage()
        {
            Driver.Navigate().Refresh();
        }

        protected void OpenWebPage(string pageName)
        {
            Driver.Navigate().GoToUrl(pageName);
        }

        private IWebElement FindElement(By by) => Wait.Until(ExpectedConditions.ElementIsVisible(by));
        protected IList<IWebElement> FindElements(By by) => Driver.FindElements(by);
        protected IWebElement WaitsUntilVisible(By elementLocator) => Wait.Until(ExpectedConditions.ElementIsVisible(elementLocator));
        protected string GetText(By by) => FindElement(by).Text;
        protected string GetUrl() => Driver.Url;
        protected string GetAttribute(By by, string attribute) => FindElement(by).GetAttribute(attribute);
        protected string GetCSSValue(By by, string cssValue) => FindElement(by).GetCssValue(cssValue);

        protected void Click(By by)
        {
            FindElement(by).Click();
        }

        protected void Type(By by, string text)
        {
            var element = FindElement(by);
            element.Clear();
            element.SendKeys(text);
        }

        protected void ScrollToElement(By by)
        {
            new Actions(Driver)
               .ScrollToElement(FindElement(by))
               .Perform();
        }

        protected void ClickJS(By by)
        {
            ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].click();", Driver.FindElement(by));
        }

        protected void WaitUntilNotVisible(By by)
        {
            Wait.Until(ExpectedConditions.InvisibilityOfElementLocated(by));
        }

        protected void HooverElement(By by)
        {
            Actions.MoveToElement(FindElement(by))
                .MoveByOffset(1, 1)
                .Build()
                .Perform();
        }

        protected bool IsElementDispalyed(By by)
        {
            return FindElement(by).Displayed;
        }

        protected bool ElementPresent(By by, int timeout = TIMEOUT)
        {
            int i = 0;
            var present = false;

            while (present == false && i < timeout)
            {
                try
                {
                    Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);
                    IWebElement element = Driver.FindElement(by);
                    present = element.Displayed;
                    if (!present)
                    {
                        Console.WriteLine($"Element {by} not found/Displayed in iteration {i}. Repeating after 1 sec. Element enabled: {present}");
                        Thread.Sleep(1000);
                    }
                    else
                    {
                        Console.WriteLine($"Element {by} found/Displayed in iteration {i}. Continuing test. Element enabled: {present}");
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine($"Element {by} not found/Displayed in iteration {i}. Repeating after 1 sec. Element enabled: {present}");
                    Thread.Sleep(1000);
                }
                finally
                {
                    i++;
                }
            }
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            return present;
        }
    }
}
