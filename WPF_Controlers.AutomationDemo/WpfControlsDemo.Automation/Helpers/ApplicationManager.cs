using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using FlaUI.Core;
using FlaUI.Core.Capturing;
using FlaUI.UIA3;

namespace WpfControlsDemo.Automation.Helpers
{
    public class ApplicationManager : IDisposable
    {
        public Application? App { get; private set; }
        public UIA3Automation? Automation { get; private set; }

        public void StartApplication(string exePath, string? args = null)
        {
            if (App != null) return;
            if (!File.Exists(exePath)) throw new FileNotFoundException("EXE not found", exePath);
            App = string.IsNullOrEmpty(args) ? Application.Launch(exePath) : Application.Launch(exePath, args);
            Automation = new UIA3Automation();
        }

        public void CloseApplication()
        {
            try { App?.Close(); } catch { try { App?.Kill(); } catch { } }
            finally { Automation?.Dispose(); Automation = null; App = null; }
        }

        public string? CaptureMainWindowScreenshot(string folder = "Screenshots", string? fileName = null)
        {
            if (App == null || Automation == null) return null;
            var win = App.GetMainWindow(Automation);
            if (win == null) return null;
            Directory.CreateDirectory(folder);
            fileName ??= $"{Guid.NewGuid():N}.png";
            var full = Path.Combine(folder, fileName);
            using var cap = Capture.Element(win);
            if (cap == null) return null;
            using var bmp = cap.Bitmap;
            if (bmp == null) return null;
            bmp.Save(full, ImageFormat.Png);
            return full;
        }

        public void Dispose() { CloseApplication(); }
    }
}
