using OpenQA.Selenium;

namespace Saucedemo.Pages.Cart
{
    internal class CartPage : WebPage
    {
        // Locators
        private By TitleOfAddedItem(string product) => By.XPath($"//div[@class='inventory_item_name'][contains(.,'{product}')]");
        private By AllAddedItems = By.XPath("//div[@class='cart_list']//div[@class='cart_item']");
        private By TextItemPriceCartPage = By.XPath("//div[contains(@class,'inventory_item_price')]");
        private By ButtonCheckout = By.Id("checkout");
        private By ButtonContinueShopping = By.Id("continue-shopping");

        // Methods
        public CartPage(IWebDriver driver) : base(driver) {}

        // Asserts
        internal void AssertThatPriceFromInventoryPageIsTheSameAtTheCartPage(string price)
        {
            var cartPagePrice = GetText(TextItemPriceCartPage).Replace("$", "");
            Assert.DoesNotThrow(() =>
            {
                Assert.That(cartPagePrice, Is.EqualTo(price));
            }, $"Error: Missmatch Prices:\n Inventory page price: '{price}'\nCart page price: {cartPagePrice}: is not displayed!");
        }

        internal void AssertThatAllPriceFromInventoryPageAreTheSameAtTheCartPage(List<string> prices)
        {
            var collection = AllAddedItemsToCart();
            for (int i = 0; i < collection.Count; i++)
            {
                var tempText = collection[i].FindElement(TextItemPriceCartPage).Text.Replace("$", "");
                Assert.DoesNotThrow(() =>
                {
                    Assert.That(tempText, Is.EqualTo(prices[i]));
                }, $"Error: Missmatch Prices:\n Inventory page price: '{prices[i]}'\nCart page price: {tempText}: is not displayed!");
            }
        }

        internal void ClickCheckoutButton()
        {
            Click(ButtonCheckout);
        }

        private IList<IWebElement> AllAddedItemsToCart()
        {
            return FindElements(AllAddedItems);
        }

    }
}
