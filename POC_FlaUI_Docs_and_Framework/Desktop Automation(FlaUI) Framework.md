# POC: Desktop Automation with FlaUI

**Author:** Automation Team  
**Date:** 2025-12-04

## Executive Summary
This POC evaluates adopting **FlaUI** for Windows desktop automation (Win32 & WPF) and integrating it into the existing unified QA Automation Framework (Option A). It demonstrates feasibility, implementation approach, limitations, CI requirements, and a recommended migration path so desktop tests run alongside Web/API/DB tests with unified reporting.

---

## Objective
- Verify that FlaUI can reliably automate our desktop applications (Win32, WPF) and integrate with the existing framework.
- Create a small, production-grade automation module (POC) with Page Object Model, BDD (Reqnroll), NUnit + Serilog, screenshot capture, and hooks.
- Demonstrate CI feasibility and provide recommended architecture for long-term adoption.

---

## Scope
**In-scope**
- Automate representative scenarios across WPF and Win32 apps (buttons, textboxes, combobox, listview/datagrid, treeview, menus, dialogs).
- Build Page Objects, Hooks, Steps, Helpers and integrate into the central framework.
- Logging, screenshots, and simple reporting integration.

**Out-of-scope**
- Distributed UI/grid testing, remote desktop automation, performance testing, mobile apps.

---

## Success Criteria
- Desktop tests run locally and on a self-hosted Windows agent.
- Reliable element identification (>=90% stability across 10 runs of the POC scenarios).
- Seamless reuse of logging, reporting and BDD structure from the existing framework.

---

## Tools & Versions (POC)
- FlaUI.Core 4.x (UIA3)
- FlaUI.UIA3 4.x
- Reqnroll.NUnit 3.x
- NUnit 3.13+
- Serilog (file sink)
- .NET 8.0-windows (test project)

---

## Architecture Overview
The POC follows the existing unified architecture (Option A). New desktop layer components are introduced and reuse existing services (logger, config, reports):

```
Features/                # BDD features (web + api + desktop)
  Web/
  API/
  Desktop/               # New - FlaUI features
Steps/
  Web/
  API/
  Desktop/               # New - Desktop steps
Pages/
  Web/
  API/
  Desktop/               # New - Desktop PageObjects
Hooks/
  GlobalHooks.cs         # unified before/after with tag filtering
Helpers/
  FlaUIHelpers/          # AppManager, WaitHelpers, Capture helpers
Reports/
  Unified Reports        # Extent/Custom PDF that includes desktop results
```

Key integration decisions:
- **Single Test Runner**: NUnit + Reqnroll runs all scenarios. Use tags to include/exclude desktop tests.
- **ScenarioContext** stores `ApplicationManager` and injected PageObjects for reuse.
- **Hooks** implement logic to launch desktop apps for `@desktop` tests and reuse existing logging and attach screenshots to the unified report.

---

## Integration Design (how FlaUI plugs into existing framework)
1. **ApplicationManager (Helper)** — responsible for launching/closing app (FlaUI Application + UIA3Automation), capturing screenshots and exposing the `Automation` object.
2. **DesktopBasePage** — inherits from the same BasePage used by Web/API but includes FlaUI helpers and a reference to the shared `ApplicationManager`.
3. **Desktop Pages (POM)** — Action_* and Verify_* methods. Do not assert in Steps; all assertions live in Page Verify_* methods to keep Step Definitions thin.
4. **Hooks** — GlobalHooks will check scenario tags (@desktop) and start/stop the application and store `ApplicationManager` and the primary PageObject in `ScenarioContext`.
5. **Steps** — obtain PageObject from `ScenarioContext` and call Page methods. Keep Steps minimal.
6. **Logging & Reporting** — reuse Serilog and add desktop metadata (app path, version, OS) to the existing report header. Attach screenshots on failure.

---

## Folder Structure (detailed)
```
/ProjectRoot
  /Features
    /Desktop
      MainWindow.feature
  /Pages
    /Desktop
      MainWindowPage.cs
      DesktopBasePage.cs
  /Steps
    /Desktop
      MainWindowSteps.cs
  /Hooks
    GlobalHooks.cs   # inspects tags and starts appropriate fixtures
  /Helpers
    /FlaUI
      ApplicationManager.cs
      WaitHelpers.cs
  /Reports
  /Config
    appsettings.json
```

---

## Sample Hook Behavior (pseudo)
```csharp
[BeforeScenario]
public void BeforeScenario(ScenarioContext ctx)
{
  if (ctx.ScenarioInfo.Tags.Contains("desktop"))
  {
    var manager = new ApplicationManager();
    manager.StartApplication(exePath);
    ctx.Set("AppManager", manager);
    ctx.Set("MainWindowPage", new MainWindowPage(manager, autoStart:false));
  }
}

[AfterScenario]
public void AfterScenario(ScenarioContext ctx)
{
  if (ctx.TryGetValue("AppManager", out ApplicationManager mgr))
  {
    if (TestContext.CurrentContext.Result.Status == Failed) attach screenshot
    mgr.CloseApplication(); mgr.Dispose();
  }
}
```

---

## POC Test Scenarios (Representative)
- Click button updates status (Button -> Label)
- TextBox accepts input and persists
- ComboBox item selection verifies selected text
- ListView contains rows (Alpha/Bravo/Charlie)
- DataGrid row count and checkbox state
- TreeView: expand/collapse and child presence
- Modal dialog opens and closes
- Menu actions show expected MessageBox

Each scenario uses Page action methods and Verify_* asserts in Page classes.

---

## CI/CD Feasibility & Agent Requirements
**Desktop UI tests require an interactive desktop session.**
- Use **self-hosted Windows agents** with:
  - Auto-logon enabled for the service account
  - Disabled screen-lock / sleep
  - Matching .NET SDK and runtimes installed
  - Visual Studio or required runtimes for WPF app
- Add a dedicated agent pool `ui-desktop-agents` and configure pipeline step to target `@desktop` tagged tests.

Sample Azure DevOps steps:
- Acquire agent
- Checkout
- Restore / Build (WPF app + tests)
- Start any required services
- Run `dotnet test --filter Category=Desktop` or run Reqnroll runner with tag filter

---

## Risk Assessment
- **Interactive agent availability** — High — Mitigate with dedicated self-hosted agents
- **Selector fragility after UI changes** — Medium — Mitigate with robust locator strategy (AutomationId first)
- **Virtualization/Remote session flakiness** — Medium — Use physical agents or VM with stable RDP

---

## Timeline & Effort Estimate (POC -> Pilot)
- POC (what we built): 1–2 weeks
- Pilot (10–20 critical flows): 2–3 weeks
- Production rollout (CI, infra, training): 3–6 weeks

---

## Final Recommendation
- Adopt FlaUI for WPF & Win32 automation and integrate it into the unified framework (Option A). Use the POC as baseline and proceed to a pilot for 10 critical flows, then production rollout.

---

## Annex: Links & Artifacts
- Sample code used in POC: `WpfControlsDemo.Automation` (repo)
- Inspect tool: FlaUInspect (choose UIA3 for WPF)
