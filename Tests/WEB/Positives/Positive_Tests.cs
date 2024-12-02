﻿using Saucedemo.Credentials;
using Saucedemo.Utils;
using Saucedemo.Utils.Products;

namespace Saucedemo.Tests.WEB.Positives
{
    [TestFixture(Browser.Chrome)]
    [Parallelizable]
    [Category("PositiveTests")]
    internal class Positive_Tests : WebTest
    {
        public Positive_Tests(Browser browser) : base(browser) {}
        private string priceinventoryPage;

        [Test]
        public void Successfully_Login_With_Valid_Credentials_And_InventoryPage_Is_Open()
        {
            _loginPage.NavigateToLoginPage();
            _loginPage.AssertThatLoginPageIsOpenSuccessfuly();
            _loginPage.LogIn(User.validUsername, User.validPassword);
            _inventoryPage.IsInventoryPageLoaded();
        }

        [Test]
        public void Successfully_Add_Items_To_The_Cart()
        {
            _loginPage.NavigateToLoginPage();
            _loginPage.AssertThatLoginPageIsOpenSuccessfuly();
            _loginPage.LogIn(User.validUsername, User.validPassword);
            _inventoryPage.IsInventoryPageLoaded();
            _inventoryPage.AddProductToCartByName(Product.SauceLabsBackpack);
            _inventoryPage.AssertThatRemoveButtonIsDisplayedForChoosenItem(Product.SauceLabsBackpack);
            _inventoryPage.GetPriceOfItem(ref priceinventoryPage, Product.SauceLabsBackpack);
            _inventoryPage.AssertThatOneItemIsAddedToShoppingCart();
            _inventoryPage.ProceedTocartPage();
            _cartPage.AssertThatPriceFromInventoryPageIsTheSameAtTheCartPage(priceinventoryPage);
        }

        [Test]
        public void Successfully_Remove_Item()
        {
            _loginPage.NavigateToLoginPage();
            _loginPage.AssertThatLoginPageIsOpenSuccessfuly();
            _loginPage.LogIn(User.validUsername, User.validPassword);
            _inventoryPage.IsInventoryPageLoaded();
            _inventoryPage.AddProductToCartByName(Product.SauceLabsBackpack);
            _inventoryPage.AssertThatRemoveButtonIsDisplayedForChoosenItem(Product.SauceLabsBackpack);
            _inventoryPage.ClickRemoveButton(Product.SauceLabsBackpack);
            _inventoryPage.AssertThatNoItemsAddedToShoppingCart();
        }

        [Test]
        public void Successfully_Purchase_Item()
        {
            _loginPage.NavigateToLoginPage();
            _loginPage.AssertThatLoginPageIsOpenSuccessfuly();
            _loginPage.LogIn(User.validUsername, User.validPassword);
            _inventoryPage.IsInventoryPageLoaded();
            _inventoryPage.AddProductToCartByName(Product.SauceLabsBackpack);
            _inventoryPage.AssertThatRemoveButtonIsDisplayedForChoosenItem(Product.SauceLabsBackpack);
            _inventoryPage.GetPriceOfItem(ref priceinventoryPage, Product.SauceLabsBackpack);
            _inventoryPage.AssertThatOneItemIsAddedToShoppingCart();
            _inventoryPage.ProceedTocartPage();
            _cartPage.AssertThatPriceFromInventoryPageIsTheSameAtTheCartPage(priceinventoryPage);
            _cartPage.ClickCheckoutButton();
            _checkoutPage.AssertThatCheckoutPadeIsLoaded();
            _checkoutPage.FillPersonalInformation(User.validFirstName, User.validLastName, User.validZipCode);
            _checkoutPage.ClickContinueButton();
            _checkoutPage.AssertThatTotalPriceIsCalculatedCorrect();
            _checkoutPage.ClickFinishButton();
            _completePage.AssertThatCheckoutCompleteTitleDisplayed();
            _completePage.AssertThatCompleteOrderMessageIsDisplayed();
        }

    }
}
