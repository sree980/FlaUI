# WpfControlsDemo

This is a minimal WPF (.NET 6) sample application demonstrating common controls and including AutomationId on each control for UI automation with FlaUI.

## Files included
- WpfControlsDemo.csproj
- App.xaml
- App.xaml.cs
- MainWindow.xaml
- MainWindow.xaml.cs
- Functional_Document.md

## How to open and run
1. Requirements:
   - Windows 10/11
   - .NET 6 SDK (or newer) installed
   - Visual Studio 2022 (recommended) with .NET desktop development workload OR use `dotnet` CLI

2. Open using Visual Studio:
   - Open `WpfControlsDemo.sln` in Visual Studio and build the solution (Build -> Build Solution).
   - Press F5 or Run to start the app.

3. Using dotnet CLI:
   - Navigate to project folder `WpfControlsDemo` and run:
     ```
     dotnet build
     dotnet run
     ```
   Note: `dotnet run` will run the app; ensure you are running on Windows and `UseWPF` project settings are supported.

## Notes for automation
- Each control has `AutomationProperties.AutomationId` set. Use FlaUInspect to confirm and use selectors like:
  ```csharp
  main.FindFirstDescendant(cf => cf.ByAutomationId("TxtInput")).AsTextBox();
  ```
- The `BtnShowDialog` opens a modal dialog (useful to practice modal handling).

## Troubleshooting
- If app doesn't start with `dotnet run`, open in Visual Studio and ensure target framework is supported.
- If controls not visible, resize the window.

