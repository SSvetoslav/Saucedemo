using OpenQA.Selenium;

namespace Saucedemo.Pages.Checkout
{
    internal class CheckoutPage : WebPage
    {
        // Locators
        private const string label = "Checkout: Your Information";
        private By InputFirstNameField = By.Id("first-name");
        private By InputLastNameField = By.Id("last-name");
        private By InputPostalCodeField = By.Id("postal-code");
        private By ButtonContinue = By.CssSelector(".cart_button");
        private By ButtonCancel = By.Id("cancel");
        private By LabelCheckoutPage = By.XPath($"//span[@class='title'][contains(.,'{label}')]");
        private By LabelItemName = By.XPath("//div[@class='inventory_item_name']");

        private By TextItemPrice = By.XPath("//div[@class='inventory_item_price']");

        private By TextItemPriceBeforeTax = By.XPath("//div[contains(@class,'summary_subtotal_label')]");
        private By TextTax = By.XPath("//div[contains(@class,'summary_tax_label')]");
        private By TextTotalPrice = By.XPath("//div[contains(@class,'summary_total_label')]");
        private By ButtonFinish = By.Id("finish");

        // Methods
        public CheckoutPage(IWebDriver driver) : base(driver) {}

        internal void FillPersonalInformation(string firstName, string lastName, string zipCode)
        {
            InputFirstName(firstName);
            InputLastName(lastName);
            InputZipCode(zipCode);
        }

        internal void ClickFinishButton()
        {
            Click(ButtonFinish);
        }

        internal void ClickContinueButton()
        {
            Click(ButtonContinue);
        }

        internal void ClickCancelButton() 
        {
            Click(ButtonCancel);
        }

        private double GetItemPrice()
        {
            return double.Parse(GetText(TextItemPriceBeforeTax).Replace("Item total: $", ""));
        }

        private double GetTax()
        {
            return double.Parse(GetText(TextTax).Replace("Tax: $", ""));
        }

        private double GetTotalPrice()
        {
            return double.Parse(GetText(TextTotalPrice).Replace("Total: $", ""));
        }

        private void InputFirstName(string firstName)
        {
            Type(InputFirstNameField, firstName);
        }
        private void InputLastName(string lastName)
        {
            Type(InputLastNameField, lastName);
        }

        private void InputZipCode(string zipCode)
        {
            Type(InputPostalCodeField, zipCode);
        }

        private bool IsFirstNameFieldDisplayed()
        {
            return IsElementDispalyed(InputFirstNameField);
        }

        private bool IsLastNameFieldDisplayed()
        {
            return IsElementDispalyed(InputLastNameField);
        }

        private bool IsZipCodeFieldDisplayed()
        {
            return IsElementDispalyed(InputPostalCodeField);
        }

        private bool IsLebelCheckoutPageDispalyed()
        {
            return IsElementDispalyed(LabelCheckoutPage);
        }

        // Asserts
        internal void AssertThatCheckoutPadeIsLoaded()
        {
            Assert.DoesNotThrow(() =>
            {
                Assert.That(IsFirstNameFieldDisplayed(), Is.EqualTo(true));
            }, "Error: First name field is not displayed!");
            Assert.DoesNotThrow(() =>
            {
                Assert.That(IsLastNameFieldDisplayed(), Is.EqualTo(true));
            }, "Error: Last name field is not displayed!");
            Assert.DoesNotThrow(() =>
            {
                Assert.That(IsZipCodeFieldDisplayed(), Is.EqualTo(true));
            }, "Error: Zip code field is not displayed!");
            Assert.DoesNotThrow(() =>
            {
                Assert.That(IsLebelCheckoutPageDispalyed(), Is.EqualTo(true));
            }, $"Error: Label '{label}' is not displayed!");
        }

        internal void AssertThatTotalPriceIsCalculatedCorrect()
        {
            var totalPrice = GetItemPrice() + GetTax();
            Assert.DoesNotThrow(() =>
            {
                Assert.That(totalPrice, Is.EqualTo(GetTotalPrice()));
            }, $"Error: Product price: {GetItemPrice()}\nTax value: {GetTax()}\nTotal price: {GetTotalPrice()}");
        }

    }
}
