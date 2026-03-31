using System;
using Reqnroll;
using WpfControlsDemo.Automation.Helpers;
using NUnit.Framework;
using WpfControlsDemo.Automation.Helpers.Pages;

namespace WpfControlsDemo.Automation.Hooks
{
    [Binding]
    public class TestHooks
    {
        private readonly ScenarioContext _scenarioContext;

        // Optional override for exe path; set to null to use MainWindowPage.DefaultExePath
        private const string ExePathOverride = null;

        public TestHooks(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            var manager = new ApplicationManager();
            if (!string.IsNullOrEmpty(ExePathOverride))
            {
                manager.StartApplication(ExePathOverride);
            }
            else
            {
                manager.StartApplication(MainWindowPage.DefaultExePath);
            }

            // store with correct Set<T>(T value, string key) order
            _scenarioContext.Set<ApplicationManager>(manager, "AppManager");

            var page = new MainWindowPage(manager, autoStart: false);
            _scenarioContext.Set<MainWindowPage>(page, "MainWindowPage");
        }

        [AfterScenario]
        public void AfterScenario()
        {
            try
            {
                if (_scenarioContext.TryGetValue<MainWindowPage>("MainWindowPage", out var page))
                {
                    if (TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Failed)
                    {
                        try
                        {
                            var path = page.CaptureScreenshot("Screenshots");
                            if (!string.IsNullOrEmpty(path)) TestContext.AddTestAttachment(path, "Screenshot on failure");
                        }
                        catch (Exception ex)
                        {
                            TestContext.WriteLine("Failed to capture screenshot: " + ex.Message);
                        }
                    }
                }

                if (_scenarioContext.TryGetValue<ApplicationManager>("AppManager", out var manager))
                {
                    try { manager.CloseApplication(); manager.Dispose(); } catch (Exception ex) { TestContext.WriteLine("Error closing app: " + ex.Message); }
                }
            }
            catch (Exception ex)
            {
                TestContext.WriteLine("Error in AfterScenario: " + ex.Message);
            }
            finally
            {
                if (_scenarioContext.ContainsKey("MainWindowPage")) _scenarioContext.Remove("MainWindowPage");
                if (_scenarioContext.ContainsKey("AppManager")) _scenarioContext.Remove("AppManager");
            }
        }
    }
}
