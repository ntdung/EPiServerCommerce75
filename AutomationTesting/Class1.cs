using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace EPiServerCommerce75
{
    public static class Class1
    {
        public static IWebElement FindElement(this IWebDriver driver, By by, int timeoutInSeconds)
        {
            var myDynamicElement  = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds)).Until(ExpectedConditions.ElementExists(by));
            return myDynamicElement;
        }

        public static IWebElement FindVisibleElement(this IWebDriver driver, By by, int timeoutInSeconds)
        {
            var myDynamicElement = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds)).Until(ExpectedConditions.ElementIsVisible(by));
            return myDynamicElement;
        }
    }
}
