using OpenQA.Selenium;
using Saucedemo.Utils.Products;

namespace Saucedemo.Pages.Inventory
{
    internal class InventoryPage : WebPage
    {

        // Locators
        private By InventoryContainer = By.Id("inventory_container");
        private By LinkShoppingCart = By.CssSelector(".shopping_cart_link");
        private By TextShoppingCartItemsCount = By.CssSelector(".shopping_cart_link > span");
        private By ProductsTitle = By.ClassName("title");
        private By InventoryItems = By.CssSelector(".inventory_item");

        // Methods
        private By ItemAddToCartButton(string product) => By.XPath($"//div[contains(.,'{product}')]" +
                "/ancestor::div[@class='inventory_item']//button[contains(@class, 'btn_inventory')]");

        private By TextPriceOfTheItem(string product) => By.XPath($"//div[contains(.,'{product}')]" + 
            "/ancestor::div[@class='inventory_item']//button[contains(@class, 'btn_inventory')]/parent::*//div[@class='inventory_item_price']");

        internal InventoryPage(IWebDriver driver) : base(driver) { }

        internal void AddProductToCartByIndex(int itemIndex)
        {
            var itemAddToCartButton = By.CssSelector($".inventory_item:nth-child({itemIndex + 1}) .btn_inventory");
            Click(itemAddToCartButton);
        }

        internal void AddProductToCartByName(string productName)
        {
            var element =  ItemAddToCartButton(productName);
            Click(element);
        }

        internal void ClickCartLink()
        {
            Click(LinkShoppingCart);
        }

        internal bool IsInventoryDisplayed()
        {
            return FindElements(InventoryItems).Any();
        }

        internal bool IsPageLoaded()
        {
            return GetText(ProductsTitle) == "Products" && GetUrl().Contains("inventory.html");
        }

        internal void IsInventoryPageLoaded()
        {
            Assert.DoesNotThrow(() =>
            {
                Assert.That(IsElementDispalyed(InventoryContainer), Is.EqualTo(true));
            }, "Error: Inventory page is not displayed!");

            Assert.DoesNotThrow(() => 
            {
                Assert.That(IsPageLoaded(), Is.EqualTo(true));
            }, "Error: Inventory page is not loaded correctrly!");
        }

        internal void GetPriceOfItem(ref string price, string product)
        {
            var element = TextPriceOfTheItem(product);
            price = GetAttribute(element, "textContent").Replace("$", "");   
        }

        internal void ProceedTocartPage()
        {
            Click(LinkShoppingCart);
        }

        internal void ClickRemoveButton(string product)
        {
            Click(ItemAddToCartButton(product));
        }

        // Asserts
        internal void AssertThatRemoveButtonIsDisplayedForChoosenItem(string product)
        {
            var element = ItemAddToCartButton(product);
            Assert.That(GetText(element), Is.EqualTo("Remove"));

        }

        internal void AssertThatOneItemIsAddedToShoppingCart()
        {
            Assert.DoesNotThrow(() =>
            {
                Assert.That(GetText(TextShoppingCartItemsCount), Is.EqualTo("1"));
            }, $"Error: Inccorect count '{GetText(TextShoppingCartItemsCount)}' is not displayed!");
        }

        internal void AssertThatNoItemsAddedToShoppingCart()
        {
            //WaitUntilNotVisible(TextShoppingCartItemsCount);
            Assert.DoesNotThrow(() =>
            {
                Assert.That(ElementPresent(TextShoppingCartItemsCount), Is.EqualTo(false));
            }, $"Error: Count of items is displayed!");
        }
    }
}
