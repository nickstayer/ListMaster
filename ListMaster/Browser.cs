using System;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using System.Threading;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System.Collections.Generic;

namespace ListMaster
{
    class Browser
    {
        public IWebDriver Driver { get; set; }
        private readonly Settings _settings;
        private string _mainTab;
        public static List<int> RunningDriversPIDs = new List<int>();

        public Browser(Settings settings)
        {
            _settings = settings;
        }

        public void Init()
        {
            Start();
            GoTo();
        }

        [Obsolete]
        private void Start()
        {
            // *новый браузер*
            
            if (_settings.BrowserType == BrowserType.chrome)
            {
                var driverService = ChromeDriverService.CreateDefaultService();
                driverService.HideCommandPromptWindow = true;
                var options = new ChromeOptions();

                options.AddArguments("--incognito");
                options.AddArguments("--user-agent=Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/127.0.0.0 Safari/537.36");
                options.AddArguments("--disable-blink-features=AutomationControlled");
                // видимость
                //options.AddArguments("--headless");

                Driver = new ChromeDriver(driverService, options);
                RunningDriversPIDs.Add(driverService.ProcessId);
            }
            else if (_settings.BrowserType == BrowserType.iexplore)
            {
                var driverService = InternetExplorerDriverService.CreateDefaultService();
                driverService.HideCommandPromptWindow = true;
                Driver = new InternetExplorerDriver(driverService, new InternetExplorerOptions());
                RunningDriversPIDs.Add(driverService.ProcessId);
            }
            else if (_settings.BrowserType == BrowserType.firefox)
            {
                var options = new FirefoxOptions();
                options.AddArguments("incognito");
                // видимость
                options.AddArguments("headless");
                options.BrowserExecutableLocation = Consts.PATH_FIREFOX_EXE;
                var driverService = FirefoxDriverService.CreateDefaultService();
                driverService.HideCommandPromptWindow = true;
                Driver = new FirefoxDriver(driverService, options);
                RunningDriversPIDs.Add(driverService.ProcessId);
            }
            _mainTab = Driver.CurrentWindowHandle;
        }

        public void GoTo()
        {
            Driver.Navigate().GoToUrl(_settings.URL);
        }

        public void SwitchToMainFrame(string mainFrameName)
        {
            Driver.SwitchTo().Frame(mainFrameName);
        }

        public IWebElement FindAndClick(By locator)
        {
            WaitElement(locator);
            var element = Driver.FindElement(locator);
            element.Click();
            Thread.Sleep(TimeSpan.FromSeconds(Consts.WAIT_3_SEC));
            return element;
        }

        public IWebElement TryFindElement(By locator)
        {
            IWebElement element = null;
            try
            {
                element = Driver.FindElement(locator);
            }
            catch (Exception)
            {
                return element;
            }
            return element;
        }

        public bool ExistElementInPageSource(string locator)
        {
            return Driver.PageSource.Contains(locator);
        }

        public bool ExistElement(By locator)
        {
            try
            {
                Driver.FindElement(locator);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IWebElement GetElement(By locator)
        {
            IWebElement foo = null;
            try
            {
                WaitElement(locator);
                foo = Driver.FindElement(locator);
            }
            catch
            {
                foo = null;
            }
            return foo;
        }

        public void WaitElement(By locator)
        {
            IWebElement webElement = null;
            int counter = 0;
            while (webElement is null)
            {
                try
                {
                    webElement = Driver.FindElement(locator);
                }
                catch
                {
                    if (counter < Consts.WAIT_10_SEC)
                    {
                        Thread.Sleep(TimeSpan.FromSeconds(Consts.WAIT_3_SEC));
                        counter++;
                    }
                    else throw new Exception($"Превышено время ({counter} c.) ожидания элемента {locator}");
                }
            }
        }

        public void WaitWhileElementDisappear(By locator)
        {
            // для теста headless
            Thread.Sleep(TimeSpan.FromSeconds(Consts.WAIT_10_SEC));

            IWebElement webElement = null;
            int counter = 0;
            do
            {
                try
                {
                    webElement = Driver.FindElement(locator);
                }
                catch
                {
                    webElement = null;
                    if (counter < Consts.WAIT_30_SEC)
                    {
                        Thread.Sleep(TimeSpan.FromSeconds(Consts.WAIT_3_SEC));
                        counter++;
                    }
                    else throw new Exception($"Превышено время ({counter} c.) ожидания исчезновения элемента {locator}");
                }
            }
            while (webElement != null);
        }

        public void WaitFor(double seconds)
        {
            Thread.Sleep(TimeSpan.FromSeconds(seconds));
        }

        public bool CanDriveBrowser()
        {
            bool result = true;
            try
            {
                string windowHandle = Driver.CurrentWindowHandle;
            }
            catch
            {
                result = false;
            }
            return result;
        }

        public void SwitchToAnotherTab()
        {
            foreach (string tab in Driver.WindowHandles)
            {
                if (_mainTab != tab)
                {
                    Driver.SwitchTo().Window(tab);
                    break;
                }
            }
        }

        public void CloseAllAnotherTabs()
        {
            if(Driver.WindowHandles.Count == 1)
            {
                _mainTab = Driver.CurrentWindowHandle;
                Driver.SwitchTo().Window(_mainTab);
                return;
            }
            foreach (string tab in Driver.WindowHandles)
            {
                if (_mainTab != tab)
                {
                    Driver.SwitchTo().Window(tab);
                    Driver.Close();
                    Driver.SwitchTo().Window(_mainTab);
                }
            }
        }

        public void SwitchToMainTab()
        {
            foreach (string tab in Driver.WindowHandles)
            {
                if (_mainTab == tab)
                {
                    Driver.SwitchTo().Window(tab);
                    break;
                }
            }
        }

        public void WaitForTheNewTab()
        {
            var counter = 0;
            while (Driver.WindowHandles.Count != 2)
            {
                Thread.Sleep(1000);
                counter++;
                if (counter == Consts.WAIT_10_SEC)
                    throw new Exception(Consts.ERROR_LONG_TAB_OPEN);
            }
        }

        public void DeleteAllCookies()
        {
            Driver.Manage().Cookies.DeleteAllCookies();
        }

        public void PersistentClick(By locator)
        {
            IWebElement clickableElement = GetElement(locator);
            clickableElement?.Click();
            WaitFor(Consts.WAIT_3_SEC);
        }

        public bool IsElementClickableByIndex(By loc)
        {
            var webElement = GetElement(loc);
            string tabindex = webElement?.GetAttribute("tabindex");
            var result = tabindex != "-1" && !string.IsNullOrEmpty(tabindex);
            return result;
        }

        public void Quit()
        {
            Driver?.Quit();
            foreach (int pid in RunningDriversPIDs)
            {
                try
                {
                    Utils.KillChildBrowserProcesses(pid, _settings.BrowserType.ToString());
                    Utils.KillProcess(pid);
                }
                catch
                {
                    continue;
                }
            }
            RunningDriversPIDs.Clear();
        }

        public IWebElement GetParentElement(IWebElement webElement)
        {
            return webElement.FindElement(By.XPath("./parent::*"));
        }

        public void ExpandBlock(By expandButton, By hidedBlock)
        {
            while (AreKeepingRecordsExpanded(hidedBlock) == false)
            {
                FindAndClick(expandButton);
            }
        }

        private bool AreKeepingRecordsExpanded(By hidedBlock)
        {
            bool result = ExistElement(hidedBlock);
            return result;
        }
    }
}