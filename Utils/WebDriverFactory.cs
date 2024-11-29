using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System.Diagnostics;

namespace Saucedemo.Utils
{
    public static class WebDriverFactory
    {
        public static IWebDriver CreateWebDriver(Browser browser)
        {
            switch (browser)
            {
                case Browser.Chrome:
                    ChromeOptions chromeOptions = new();
                    if (!Debugger.IsAttached)
                    {
                        chromeOptions.AddArgument("--headless=old");
                    }
                    chromeOptions.AddUserProfilePreference("profile.password_manager_enabled", false);
                    chromeOptions.AddArgument("--disable-search-engine-choice-screen");
                    chromeOptions.AddArgument("window-size=1920, 1080");
                    return new ChromeDriver(chromeOptions);
                default:
                    throw new NotImplementedException("Unsupported browser: {browser}");
            }
        }
    }
}
