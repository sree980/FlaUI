# Updated QA Automation Framework Guide — Desktop Automation (FlaUI) Integration

This document updates the existing framework file to include desktop automation using FlaUI, following the unified Option A architecture.

## 1. Overview
We extend the existing framework to support Desktop automation (WPF) while reusing common components (logging, reports, config, CI). The Desktop layer follows the same POM + BDD + Hooks pattern your team already uses.

## 2. New/Updated Components
### ApplicationManager (Helpers/FlaUI)
- Responsibilities:
  - Launch/Close application using `FlaUI.Core.Application.Launch`.
  - Create and manage `UIA3Automation` instance.
  - Capture screenshots (Capture.Element(...).Bitmap)
  - Expose `Automation` and `App` for pages to use.

### DesktopBasePage (Pages/Desktop)
- Inherits from existing BasePage where applicable.
- Holds a reference to `ApplicationManager` and provides helpers to get `MainWindow` and common find methods.
- Exposes `Start()` and `Stop()` helper delegating to `ApplicationManager`.

### Desktop Pages (Pages/Desktop/<PageName>Page.cs)
- Follow the existing POM style: `Action_*` and `Verify_*` methods.
- Keep interactions inside Pages; Steps should call these methods only.

### Steps/Bindings (Steps/Desktop)
- Thin wrappers that fetch Page objects from `ScenarioContext` and call the Page methods.
- Avoid logic or assertions in steps.

### Hooks (Hooks/GlobalHooks.cs)
- Single hooks file that handles both web and desktop tests using tags.
- Example tags: `@web`, `@api`, `@desktop`.
- Launch/Close application for `@desktop` scenarios and attach screenshots on failure.

## 3. Locator Strategy & Best Practices
1. Prefer `AutomationId`.
2. Use `ControlType` (Text, Button, ComboBox) and patterns (SelectionItem, Value, ExpandCollapse) when needed.
3. For templated controls (ListView, DataGrid), prefer `AsListView()` or find `ControlType.DataItem` then child `Text` nodes.
4. Avoid absolute coordinates and window title-only selectors.
5. Use `Retry` helpers (WaitHelpers) around dynamic controls.

## 4. Example Page Template (C#)
```csharp
public class DesktopBasePage
{
  protected readonly ApplicationManager AppManager;
  public DesktopBasePage(ApplicationManager manager) => AppManager = manager;
  protected Window MainWindow => AppManager.App.GetMainWindow(AppManager.Automation);
}

public class MainWindowPage : DesktopBasePage
{
  public MainWindowPage(ApplicationManager m) : base(m) {}
  public void Action_ClickButton() => MainWindow.FindFirstDescendant(cf=>cf.ByAutomationId("BtnSimple")).AsButton().Invoke();
  public void Verify_Label(string expected) => NUnit.Framework.Assert.AreEqual(expected, MainWindow.FindFirstDescendant(cf=>cf.ByAutomationId("LblStatus")).AsLabel().Text);
}
```

## 5. Hooks Implementation (pattern)
- Single `GlobalHooks` inspects scenario tags and performs environment setup accordingly.
- For `@desktop`:
  - Create `ApplicationManager`, call `StartApplication(path)`
  - Store in `ScenarioContext` with key `AppManager`
  - Instantiate primary Page(s) and store `MainWindowPage` in `ScenarioContext`
- After scenario:
  - On failure: capture screenshot via Page.CaptureScreenshot and attach using `TestContext.AddTestAttachment`.
  - Close and dispose `ApplicationManager`.

## 6. Reporting & Attachments
- Reuse existing reporting pipeline. Add desktop-specific metadata (exe path, OS, screen resolution).
- Attach screenshots and logs to the same report output.
- Keep test results unified (single HTML/Extent + PDF if required).

## 7. CI/CD & Infra
- **Self-hosted Windows agents required**
- Agent requirements:
  - Auto-logon user
  - .NET SDK (matching target)
  - Visual Studio or runtime
  - Screen resolution and DPIs standardized
- Pipeline strategy:
  - Tag-based test execution: run only `@desktop` on UI agents
  - Use artifact staging for screenshots/logs

## 8. Execution Commands
- Run all tests: `dotnet test`
- Run only desktop tests: `dotnet test --filter TestCategory=Desktop` OR use Reqnroll tag filter if available

## 9. Best Practices
- Keep Page methods deterministic and idempotent.
- Centralize wait/retry logic in `WaitHelpers`.
- Capture logs and screenshots at step granularity when helpful.
- Avoid parallel desktop runs on the same machine.

## 10. Migration Checklist
1. Add Helpers/FlaUI to repo
2. Add Pages/Desktop folder and DesktopBasePage
3. Implement minimal Hooks changes for tag-based launch
4. Add 10 pilot scenarios and run locally
5. Configure and validate self-hosted Windows agent
6. Integrate reporting and run pipeline

## 11. Appendix: Useful Snippets
- ApplicationManager screenshot snippet, Start/Close patterns, and common Wait helpers (see code sample in POC document).
