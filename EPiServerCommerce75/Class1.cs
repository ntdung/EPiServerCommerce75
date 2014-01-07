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
        public static IWebElement FindElement(this IWebDriver driver, By by, int timeoutInSecond)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSecond));
            IWebElement myDynamicElement = wait.Until<IWebElement>((d) =>
            {
                return d.FindElement(by);
            });
            return myDynamicElement;
        }
    }
}
