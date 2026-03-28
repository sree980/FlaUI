using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystemDemo.Automation.Helpers.Pages
{
    public class LaunchUrl
    {
        private readonly ApplicationManager _appManager;
        private readonly bool _ownsManager;

        // Default exe path - update if needed
        public const string DefaultExePath = @"C:\Automation\FlaUI\WPF_Controlers.AutomationDemo\BankSystem\bin\Release\BankSystem.exe";

        public LaunchUrl()
        {
            _appManager = new ApplicationManager();
            _ownsManager = true;
        }
        // ctor for injected ApplicationManager; autoStart will start if manager has no running app
        public LaunchUrl(ApplicationManager appManager, bool autoStart = false)
        {
            _appManager = appManager ?? throw new ArgumentNullException(nameof(appManager));
            _ownsManager = false;
            if (autoStart && _appManager.App == null)
            {
                _appManager.StartApplication(DefaultExePath);
                Thread.Sleep(300);
            }
        }

        public void Start(string? exePath = null)
        {
            if (_appManager.App != null) return;
            var path = exePath ?? DefaultExePath;
            _appManager.StartApplication(path);
            Thread.Sleep(300);
        }
        public string? CaptureScreenshot(string folder = "Screenshots") => _appManager.CaptureMainWindowScreenshot(folder);

        public void Stop()
        {
            _appManager.CloseApplication();
        }
    }
}
