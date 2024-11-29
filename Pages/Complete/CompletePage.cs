using OpenQA.Selenium;

namespace Saucedemo.Pages.Complete
{
    internal class CompletePage : WebPage
    {
        // Locators
        private By TitleCompleteOrder = By.XPath("//h2[@class='complete-header'][contains(.,'Thank you for your order!')]");
        private By TitleCheckoutComplete = By.XPath("//span[@class='title'][contains(.,'Checkout: Complete!')]");

        // Methods
        public CompletePage(IWebDriver driver) : base(driver) {}

        // Asserts
        public void AssertThatCompleteOrderMessageIsDisplayed()
        {
            Assert.DoesNotThrow(() =>
            {
                Assert.That(IsElementDispalyed(TitleCompleteOrder), Is.EqualTo(true));
            }, $"Error: Successful message 'Thank you for your order!' is not displayed!");
        }

        public void AssertThatCheckoutCompleteTitleDisplayed() 
        {
            Assert.DoesNotThrow(() =>
            {
                Assert.That(IsElementDispalyed(TitleCheckoutComplete), Is.EqualTo(true));
            }, $"Error: Title 'Checkout: Complete!' is not displayed!");
        }
    }
}
