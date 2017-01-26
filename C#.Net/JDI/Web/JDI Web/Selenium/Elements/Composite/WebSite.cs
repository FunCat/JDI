﻿using System;
using Epam.JDI.Core.Base;
using JDI_Web.Selenium.Base;
using JDI_Web.Selenium.DriverFactory;
using JDI_Web.Settings;
using OpenQA.Selenium;

namespace JDI_Web.Selenium.Elements.Composite
{
    public class WebSite : Application
    {
        public IWebDriver WebDriver => WebSettings.WebDriverFactory.GetDriver(DriverName);
        public string Url => WebDriver.Url;
        public string BaseUrl => new Uri(WebDriver.Url).GetLeftPart(UriPartial.Authority);
        public string Title => WebDriver.Title;
        private static WebCascadeInit CascadeInit => new WebCascadeInit();

        public static void Init(Type site)
        {
            CascadeInit.InitStaticPages(site, WebSettings.WebDriverFactory.CurrentDriverName);
            CurrentSite = site;
        }

        public static T Init<T>(Type site, string driverName) where T : Application
        {
            return CascadeInit.InitPages<T>(site, driverName);
        }

        public static T Init<T>(Type site, DriverTypes driverType = DriverTypes.Chrome) where T : Application
        {
            return Init<T>(site, WebSettings.UseDriver(driverType));
        }

        public T Init<T>(string driverName) where T : Application
        {
            DriverName = driverName;
            return Init<T>(GetType(), driverName);
        }

        public T Init<T>(DriverTypes driverType = DriverTypes.Chrome) where T : Application
        {
            DriverName = WebSettings.UseDriver(driverType);
            return Init<T>(DriverName);
        }

        public T InitScope<T>(object scope) where T : Application
        {
            return Init<T>(GetType());
        }
        
        public void OpenUrl(string url)
        {
            WebDriver.Navigate().GoToUrl(url);
        }
        
        public void Refresh()
        {
            WebDriver.Navigate().Refresh();
        }

        public void Forward()
        {
            WebDriver.Navigate().Forward();
        }

        public void Back()
        {
            WebDriver.Navigate().Back();
        }
    }
}
