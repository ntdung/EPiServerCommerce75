using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace EPiServerCommerce75
{
    class Program
    {
        private static IWebDriver driver = null;
        private static string baseURL = "http://netfocus:17000";

        public static bool ElementMouseOver(IWebElement targetElement)
        {
            var builder = new OpenQA.Selenium.Interactions.Actions(driver);
            try
            {  
                builder.MoveToElement(targetElement).Build().Perform();
                Thread.Sleep(2000);//2 sec is just to for this blog.
                //I use custome method to wait element being appeared after mouse hover event.
                //You can use other variable wait time but make sure your give some pause
                //otherwise mouse hover will happen for fraction of seconds and then disappear.

            }
            catch (Exception e)
            {
                //loggerInfo.Instance.Message(e.Message);
                return false;
            }
            return true;
        }

        static void TestExpanCollaspOnTop()
        {
            var divExpand = driver.FindElement(By.XPath("//div[@class='epi-navigation-expandcollapseContainer']"));
            var link = divExpand.FindElement(By.XPath(".//a"));

            Actions actions2 = new Actions(driver);
            actions2.MoveToElement(link);
            actions2.Click().Perform();

            Thread.Sleep(3000);
            var adminLink = driver.FindElement(By.XPath("//a[@class='epi-navigation-global_cms_admin ']"));
            actions2.MoveToElement(adminLink);
            actions2.Click().Perform();

            driver.SwitchTo().Frame("FullRegion_AdminMenu");

            driver.FindElement(By.LinkText("Content Type")).Click();

            driver.FindElement(By.LinkText("Create New Page Type")).Click();
            //var contentTypeTab = driver.FindElement(By.LinkText("Content Type"));
            //actions2.MoveToElement(contentTypeTab);
            //actions2.Click().Perform();

        }

        private static void Logout()
        {
            driver.FindElement(By.CssSelector("nav.account-bar")).Click();
            driver.FindElement(By.Id("LoginSelectorID_LoginView_logout")).Click();
        }

        private static void Login()
        {
            driver.FindElement(By.CssSelector("nav.account-bar")).Click();
            driver.FindElement(By.CssSelector("nav.login-selector > a")).Click();
            driver.FindElement(By.Id("MainContent_LoginID_EmailAddress_ExistingId")).Clear();
            driver.FindElement(By.Id("MainContent_LoginID_EmailAddress_ExistingId")).SendKeys("admin");
            driver.FindElement(By.Id("MainContent_LoginID_Password_ExistingId")).Clear();
            driver.FindElement(By.Id("MainContent_LoginID_Password_ExistingId")).SendKeys("store");
            driver.FindElement(By.Name("RememberMe")).Click();
            driver.FindElement(By.Id("MainContent_LoginID_SignInId")).Click();
        }

        private static void GoToCmsEdit()
        {
            driver.FindElement(By.Id("epi-quickNavigator-clickHandler")).Click();
            driver.FindElement(By.LinkText("CMS Edit")).Click();
            Thread.Sleep(5000);
        }

        private static void PlaceOrder()
        {
            // Click on Shopping menu item
            driver.FindElement(By.XPath("//a[@href='/en/Shopping-Overview/']")).Click();

            // Click on Available Catalogs \ Departments \ Fashion
            driver.FindElement(By.XPath("//a[@href='/en/departmental-catalog/Departments/Fashion/']")).Click();

            // Tops
            var link = driver.FindElement(By.XPath("//div[@class='caption']//a[@href='/en/departmental-catalog/Departments/Fashion/Tops/']"));
            Console.WriteLine(link.Text);
            link.Click();

            var link1 = driver.FindElement(By.XPath("//div[@class='caption']//a[@href='/en/departmental-catalog/Departments/Fashion/Tops/Tops-Tunics/']"));
            Console.WriteLine(link1.Text);
            link1.Click();

            //btn-info
            var link2 = driver.FindElement(By.XPath("//div[@class='btn-group']//a[@href='/en/departmental-catalog/Departments/Fashion/Tops/Tops-Tunics/Tops-Tunics-CowlNeck/']/following-sibling::a[1]"));
            link2.Click();

            var link3 =
                driver.FindElement(By.XPath("//div[contains(@class, 'btn-group')]//a[@href='/en/departmental-catalog/Departments/Fashion/Tops/Tops-Tunics/Tops-Tunics-CowlNeck/']/following::div//a[contains(@id, 'btnAddToCart')]"));
                
            link3.Click();

            driver.FindElement(By.Id("MainContent_CartSimpleModuleID_ContinueButton")).Click();

            driver.FindElement(By.XPath("//div[@class='btn-group']//a[@href='/en/departmental-catalog/Departments/Fashion/Tops/Tops-Tunics/Tops-Tunics-LongSleeve/']/following-sibling::a[1]")).Click();

            driver.FindElement(By.XPath("//div[contains(@class, 'btn-group')]//a[@href='/en/departmental-catalog/Departments/Fashion/Tops/Tops-Tunics/Tops-Tunics-LongSleeve/']/following::div//a[contains(@id, 'btnAddToCart')]")).Click();
            ClickProceedToCheckout();
            ClickSingleShipment();
            EnterNewBillAddress();
            //
        }

        private static void ClickProceedToCheckout()
        {
            driver.FindElement(By.Id("MainContent_CartSimpleModuleID_goToCheckout")).Click();
        }

        private static void ClickSingleShipment()
        {
            driver.FindElement(By.XPath("//a[@href='/en/Checkout/Single-Shipment-Checkout/']")).Click();
        }

        private static void EnterNewBillAddress()
        {
            driver.FindElement(By.XPath("//strong[contains(text(), 'Bill To:')]//parent::*//parent::*//a[@class='btn btn-info dropdown-toggle']")).Click();

            driver.FindElement(By.XPath("//strong[contains(text(), 'Bill To:')]//parent::*//parent::*//a[text()='Creating a New Address']")).Click();
            
            Thread.Sleep(2000);
            
            driver.FindElement(By.Id("MainContent_SingleShipmentCheckoutID_Addresses_Name")).SendKeys("NNPT");

            driver.FindElement(By.Id("MainContent_SingleShipmentCheckoutID_Addresses_FirstName")).SendKeys("Dũng");
            driver.FindElement(By.Id("MainContent_SingleShipmentCheckoutID_Addresses_LastName")).SendKeys("Nguyễn");
            driver.FindElement(By.Id("MainContent_SingleShipmentCheckoutID_Addresses_Email")).SendKeys("ntdung171@hotmail.com");
            driver.FindElement(By.Id("MainContent_SingleShipmentCheckoutID_Addresses_Phone")).SendKeys("84986676268");
            driver.FindElement(By.Id("MainContent_SingleShipmentCheckoutID_Addresses_CompanyName")).SendKeys("TVSoft Co.,Ltd");
            driver.FindElement(By.Id("MainContent_SingleShipmentCheckoutID_Addresses_StreetAddress")).SendKeys("18/422 Trương Định");
            driver.FindElement(By.Id("MainContent_SingleShipmentCheckoutID_Addresses_Apartment")).SendKeys("Apt");
            driver.FindElement(By.Id("MainContent_SingleShipmentCheckoutID_Addresses_City")).SendKeys("Hà Nội");
            driver.FindElement(By.Id("MainContent_SingleShipmentCheckoutID_Addresses_State")).SendKeys("N/A");
            driver.FindElement(By.Id("MainContent_SingleShipmentCheckoutID_Addresses_Zip")).SendKeys("10000");
           // driver.FindElement(By.Id("MainContent_SingleShipmentCheckoutID_Addresses_Country")).SendKeys("");
            var country =
                new SelectElement(driver.FindElement(By.Id("MainContent_SingleShipmentCheckoutID_Addresses_Country")));
            country.SelectByText("Viet Nam");
            driver.FindElement(By.Id("MainContent_SingleShipmentCheckoutID_Addresses_SaveAddressButton")).Click();

            Thread.Sleep(2000);

            SelectExistingAddressForShipTo();
            Thread.Sleep(2000);
            // Verify Sub Total value
            var subTotal = driver.FindElement(By.XPath("//h5[contains(text(),'Sub Total For Your Items')]//parent::*//following-sibling::div//strong"));
            Console.WriteLine("Sub total: " + subTotal.Text);

            //var subTotal1 = driver.FindElement(By.XPath("//h5[contains(text(),'Additional Order Level Discounts')]//parent::*//following-sibling::div//strong"));
            //Console.WriteLine("Additional Order: " + subTotal1.Text);

            var subTotal2 = driver.FindElement(By.XPath("//h5[contains(text(),'Sub Total For Your Cart/Order')]//parent::*//following-sibling::div//h5"));
            Console.WriteLine("Sub Total For Your Cart/Order: " + subTotal2.Text);

            var subTotal3 = driver.FindElement(By.XPath("//div[contains(text(),'Estimated Shipping Costs')]//following-sibling::div//strong"));
            Console.WriteLine("Estimated Shipping: " + subTotal3.Text);

            var subTotal4 = driver.FindElement(By.XPath("//div[contains(text(),'Estimated Tax to Be Collected')]//following-sibling::div//strong"));
            Console.WriteLine("Estimated Tax: " + subTotal4.Text);
            
            /* Click Place Order */
            driver.FindElement(By.Id("MainContent_SingleShipmentCheckoutID_CheckoutButton")).Click();
        }

        private static void SelectExistingAddressForShipTo()
        {
            driver.FindElement(By.XPath("//strong[contains(text(), 'Ship To:')]//parent::*//parent::*//a[text()='Use Address Book']")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("//div[@id='select-existing-address' and contains(@style, 'block')]//a[text()='NNPT']")).Click();
        }
        static void Main(string[] args)
        {
            driver = new ChromeDriver();
            //Size currentWinSize = driver.Manage().Window.Size;
            driver.Manage().Window.Maximize();


            driver.Navigate().GoToUrl(baseURL + "/");
            
            Login();
            PlaceOrder();
            
            
            //var ul = driver.FindElement(By.Id("epi-quickNavigator-menu"));
            //Console.WriteLine(ul.GetAttribute("style"));
            //var links = ul.FindElements(By.TagName("a"));
            //var li = ul.FindElements(By.TagName("li"));
            //foreach (var link in links)
            //{
            //    Console.WriteLine(link.Text);
            //}

            //var dashBoardLink = links.First(element => element.Text == "Dashboard");
            //Console.WriteLine(dashBoardLink.Location);

            //var cmsEditLink = links.First(element => element.Text == "CMS Edit");
            //Console.WriteLine(cmsEditLink.Location);
            
            
            //TestExpanCollaspOnTop();
            return;
            
            var span = driver.FindElement(By.Id("uniqName_26_46"));

            Console.WriteLine(span != null);
            var sParent = span.FindElement(By.XPath("./parent::*"));
            if (sParent != null)
            {
                Console.WriteLine("hjeheheheh");
                Console.WriteLine(sParent.GetAttribute("data-dojo-attach-event"));
                Actions actions = new Actions(driver);
                actions.MoveToElement(sParent);//.Build().Perform();
                actions.Click().Perform();
                var pin = driver.FindElement(By.Id("dijit_form_ToggleButton_6"));
                var pPin = pin.FindElement(By.XPath("./parent::*"));
                if (pPin != null)
                {
                    Console.WriteLine(pPin.GetAttribute("data-dojo-attach-event"));
                    actions.MoveToElement(pPin);//.Build().Perform();
                    actions.Click().Perform();
                }
            }
            var treeDiv = driver.FindElement(By.Id("navigation"));
            if (treeDiv != null)
            {
                Actions actions1 = new Actions(driver);
                Console.WriteLine("left tree can be recorgnied");
                var homePageNode = treeDiv.FindElement(By.XPath("//span[contains(@title, '2526')]"));
                actions1.MoveToElement(homePageNode.FindElement(By.XPath("./parent::*")));
                actions1.Click().Perform();

                //Create content
                var createButton = driver.FindElement(By.XPath("//span[contains(@title, 'Create content')]"));
                actions1.MoveToElement(createButton.FindElement(By.XPath("./parent::*")));
                actions1.Click().Perform();

                var newPageRow = driver.FindElement(By.Id("uniqName_26_47"));
                actions1.MoveToElement(newPageRow);
                actions1.Click().Perform();
                driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
                //data-dojo-attach-point="namePanel"
                var namePanelDiv = driver.FindElement(By.XPath("//div[contains(@data-dojo-attach-point, 'namePanel')]//div[contains(@class, 'dijitReset dijitInputField dijitInputContainer')]"));

                if (namePanelDiv != null)
                {
                    //dijitReset dijitInputField dijitInputContainer
                    var newPageInput = namePanelDiv.FindElement(By.TagName("input"));
                    Console.WriteLine(newPageInput.GetAttribute("id"));

                    actions1.MoveToElement(newPageInput);
                    actions1.Click().Perform();

                    actions1.SendKeys(Keys.Backspace);
                    actions1.SendKeys(Keys.Backspace);
                    actions1.SendKeys(Keys.Backspace);
                    actions1.SendKeys(Keys.Backspace);
                    actions1.SendKeys(Keys.Backspace);
                    actions1.SendKeys(Keys.Backspace);
                    actions1.SendKeys(Keys.Backspace);
                    actions1.SendKeys(Keys.Backspace);

                    actions1.SendKeys("TestPage");
                    var homePage = driver.FindElement(By.XPath("//h3[contains(text(), 'Home Page')]")).FindElement(By.XPath("./parent::*"));

                    actions1.MoveToElement(homePage);
                    actions1.Click().Perform();

                    Thread.Sleep(5000);

                    var tiny = driver.FindElement(By.ClassName("epi-overlay-bracket"));
                    actions1.MoveToElement(tiny);
                    actions1.Click().Perform();

                    Thread.Sleep(3000);
                    var winHandleBefore = driver.CurrentWindowHandle;
                    driver.SwitchTo().Frame("uniqName_110_0_editorFrame_ifr");

                    var p = driver.FindElement(By.Id("tinymce")).FindElement(By.TagName("p"));
                    actions1.MoveToElement(p);
                    actions1.SendKeys("skdfj sjlfdkjsl djfklj sldfkj").Perform();
                    Thread.Sleep(3000);

                    driver.SwitchTo().Window(winHandleBefore);

                    //dijit dijitReset dijitInline epi-mediumButton epi-modeButton dijitToggleButton
                    var toggleButton =
                        driver.FindElement(
                            By.XPath(
                                "//span[contains(@class, 'dijit dijitReset dijitInline epi-mediumButton epi-modeButton dijitToggleButton')]")).FindElement(By.TagName("span"));
                    actions1.MoveToElement(toggleButton);
                    actions1.Click().Perform();

                    Thread.Sleep(3000);
                    var form = driver.FindElement(By.XPath("//form[@class='epi-formArea']"));
                    var div =
                        form.FindElement(By.TagName("div"));

                    Console.WriteLine(div.GetAttribute("class"));
                    var div1 = div.FindElement(By.XPath("//div[@class='dijitTabPaneWrapper dijitTabContainerTop-container']"));
                    Console.WriteLine(div1.GetAttribute("aria-labelledby"));

                    var ul = div1.FindElement(By.XPath(".//ul"));
                    var lis = ul.FindElements(By.XPath(".//li"));
                    Console.WriteLine("li count: " + lis.Count.ToString());


                    
                    var label = lis[0].FindElement(By.XPath(".//label"));
                    
                    Console.WriteLine(lis[1].FindElement(By.XPath(".//label[contains(.,'StringField')]")).GetAttribute("for"));
                    Console.WriteLine("for: " + label.GetAttribute("for"));
                    
                    var labelChild = label.FindElements(By.XPath("*"));
                    Console.WriteLine("label child count: " + labelChild.Count.ToString());



                    //var label = driver.FindElement(By.XPath("//label[contains(text(), 'PayPalPaymentPage')]"));
                    //if (label != null)
                    //{
                    //    Console.WriteLine("PayPalPaymentPage label found");
                    //}
                }
                //span[contains(@title, 'Home Page')]
                // //div[contains(@class, 'v-table-body')]
            }
           
            // driver.Quit();
            //driver.FindElement(By.XPath("//span[@id='uniqName_26_46']/span")).Click();
            //driver.FindElement(By.XPath("//span[@id='uniqName_26_46']/span")).Click();
            //uniqName_26_46
            //IWebElement elem = driver.FindElement(By.XPath(".//*[@id='epi-quickNavigator-menu']/a[text()='CMS Edit']"));


            //Actions actions = new Actions(driver);

            //actions.MoveToElement(elem).Build().Perform();

            //actions.MoveToElement(cmsEditLink).Build().Perform();
            //cmsEditLink.Click();
            //actions.Click().Perform();
            //Console.ReadKey();

        }
    }
}
