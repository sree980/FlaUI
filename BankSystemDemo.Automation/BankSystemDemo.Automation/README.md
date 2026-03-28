# FlaUI Bank System Automation Project

## 📋 Overview

This is a comprehensive **UI Automation Testing Framework** built for testing a Windows Forms-based Bank System application. The project demonstrates automated testing of desktop applications using **FlaUI**, a modern UI automation library for .NET applications that leverages Microsoft UI Automation (UIA).

The solution consists of two main projects:
1. **BankSystem** - A Windows Forms desktop application (Bank System application under test)
2. **BankSystemDemo.Automation** - An automation testing framework using Reqnroll (Cucumber-style BDD) for writing test scenarios

---

## 🏗️ Project Structure

### Solution Layout
```
WPF_Controlers.AutomationDemo/
├── BankSystem
│
└── BankSystemDemo.Automation/          # Automation Testing Framework
    ├── Features/                         # Gherkin feature files (BDD scenarios)
    │   └── BankingSystem.feature        # Test scenarios for bank system
    ├── Steps/                            # Step implementations (Glue code)
    │   └── BankSystemSteps.cs           # Step definitions for BankingSystem.feature
    ├── Helpers/                          # Utility classes
    │   ├── ApplicationManager.cs        # Manages app lifecycle and automation
    │   ├── WaitHelpers.cs               # Explicit wait mechanisms
    │   └── Pages/
    │       └── LaunchUrl.cs             # Page object model for app launch
    ├── Hooks/                            # Test lifecycle hooks
    │   └── TestHooks.cs                 # Before/After scenario setup/teardown
    ├── reqnroll.json                    # Reqnroll configuration
    ├── README.md                         # Framework documentation
    └── BankSystemDemo.Automation.csproj # Project file (.NET 8)
```

---

## 🎯 Key Projects


### 1. **BankSystemDemo.Automation** (.NET 8)
**Purpose**: UI Automation Testing Framework for testing the BankSystem application.

**Key Features**:
- **BDD Framework**: Uses Reqnroll (Cucumber-style) for writing scenarios in plain English
- **FlaUI Automation**: Leverages FlaUI library (v5.0.0) for UI element interaction
- **Cross-layer Testing**: Automates GUI interactions and validates business logic
- **Logging & Reporting**: Serilog integration for detailed test logs
- **Screenshot Capture**: Automatic screenshot capture on test failures

---

## 📦 Dependencies

### BankSystemDemo.Automation Project
- **Reqnroll.NUnit** (v3.2.1) - BDD framework for writing test scenarios
- **FlaUI.Core** (v5.0.0) - Core UI automation library
- **FlaUI.UIA3** (v5.0.0) - Microsoft UI Automation 3 provider
- **NUnit** (v4.4.0) - Unit testing framework
- **NUnit3TestAdapter** (v6.0.0-beta.3) - Test adapter for Visual Studio
- **Microsoft.NET.Test.Sdk** (v18.0.1) - Testing infrastructure
- **Serilog** (v4.3.1-dev) - Structured logging
- **Serilog.Sinks.File** (v7.0.1-dev) - File logging sink
- **System.Drawing.Common** (v10.0.0) - For screenshot capturing

### BankSystem Project
- .NET Framework 4.8
- Windows Forms (System.Windows.Forms)

---

## 🚀 Getting Started

### Prerequisites
- .NET 8 SDK (for automation framework)
- .NET Framework 4.8 (for bank system application)
- Visual Studio 2022 or later

### Setup Instructions

1. **Build the Solutions**
   ```bash
   # Build Bank System (SUT)
   dotnet build BankSystem/BankSystem.csproj
   
   # Build Automation Framework
   dotnet build BankSystemDemo.Automation/BankSystemDemo.Automation.csproj
   ```

2. **Update Application Path** (if needed)
   - Edit `BankSystemDemo.Automation/Helpers/Pages/LaunchUrl.cs`
   - Update `DefaultExePath` to match your BankSystem.exe location:
   ```csharp
   public const string DefaultExePath = @"C:\path\to\BankSystem\bin\Release\BankSystem.exe";
   ```

4. **Run Tests**
   ```bash
   dotnet test BankSystemDemo.Automation/BankSystemDemo.Automation.csproj
   ```

---

## 🧪 Test Scenarios

The automation framework includes the following test scenarios defined in `BankingSystem.feature`:

## 🧪 Test Scenarios

### Overview

The framework includes **9 automated test scenarios** covering critical functionality:

| Test Case | Scenario | Steps | Status |
|-----------|----------|-------|--------|
| **TC001** | Data Entering in TextBox | Launch → Register → Enter Data → Submit | ✅ Active |
| **TC002** | DropDown Selection | Launch → Register → Select Dropdown Values → Submit | ✅ Active |
| **TC003** | CheckBox Selection | Launch → Register → Check VIP → Submit | ✅ Active |
| **TC004** | Test for Dropdown Scenario | Launch → Register → Select Age Dropdown | ✅ Active |
| **TC005** | Contact Page Feedback | Launch → Contact Us → Enter Feedback → Submit | ✅ Active |
| **TC006** | Exchange Rate Button | Launch → Click Exchange Button | ✅ Active |
| **TC007** | About Us Button | Launch → Click About Button | ✅ Active |
| **TC008** | Capture Screenshot | Launch → Capture Screenshot | ✅ Active |
| **TC009** | Exit Application | Launch → Exit App | ✅ Active |

### Detailed Test Case Descriptions

#### TC001 - User Registration with Valid Data

**Objective:** Verify user can register with complete valid information

**Steps:**
1. Launch BankSystem application
2. Click "Registration" button
3. Enter user details:
   - First Name: Srikanth
   - Last Name: Kalamanda
   - Phone: 9876543210
   - Email: jhabcdefhjk@gmail.com
   - Password: 12345
   - Card Number: 456378963215
4. Check VIP checkbox
5. Click OK button
6. Verify successful registration

**Expected Result:** User registration completes without errors

---

#### TC002 - Dropdown Selection

**Objective:** Verify dropdown/combobox functionality

**Steps:**
1. Launch BankSystem application
2. Click "Registration" button
3. Select Age: 4
4. Select Country: India
5. Check VIP checkbox
6. Click OK button

**Expected Result:** Dropdown values are properly selected and saved

---

#### TC003 - CheckBox Interaction

**Objective:** Verify checkbox state changes

**Steps:**
1. Launch BankSystem application
2. Click "Registration" button
3. Check VIP checkbox
4. Click OK button

**Expected Result:** Checkbox state is properly toggled and registered

---

#### TC004-TC009

**Similar structure with variations:**
- TC004: Age dropdown dropdown selection
- TC005: Contact form feedback submission
- TC006: Exchange rate button functionality
- TC007: About Us button navigation
- TC008: Screenshot capture functionality
- TC009: Application exit and cleanup


---

## 🔧 Core Components

### **ApplicationManager** (`Helpers/ApplicationManager.cs`)
Manages application lifecycle and automation setup:
- Launches the target application (BankSystem.exe)
- Initializes UIA3 Automation provider
- Handles screenshot capture on demand
- Manages application closure and resource cleanup

```csharp
public class ApplicationManager : IDisposable
{
    public Application? App { get; private set; }
    public UIA3Automation? Automation { get; private set; }
    
    public void StartApplication(string exePath, string? args = null)
    public void CloseApplication()
    public string? CaptureMainWindowScreenshot(string folder = "Screenshots")
}
```

### **LaunchUrl** (`Helpers/Pages/LaunchUrl.cs`)
Page Object Model for application launch and management:
- Encapsulates application startup logic
- Provides screenshot capture interface
- Manages application lifecycle

```csharp
public class LaunchUrl
{
    public const string DefaultExePath = @"...BankSystem.exe";
    public void Start(string? exePath = null)
    public void Stop()
    public string? CaptureScreenshot(string folder = "Screenshots")
}
```

### **TestHooks** (`Hooks/TestHooks.cs`)
Reqnroll hooks for test setup and teardown:

**BeforeScenario**:
- Launches the BankSystem application
- Initializes ApplicationManager
- Sets up ScenarioContext with application instances

**AfterScenario**:
- Captures screenshots on test failure
- Cleans up and closes the application
- Removes context variables
- Handles exceptions gracefully

### **BankSystemSteps** (`Steps/BankSystemSteps.cs`)
Step implementations for feature file scenarios:
- Launches application
- Clicks UI elements (buttons, checkboxes, dropdowns)
- Enters user data into forms
- Validates UI state changes

### **WaitHelpers** (`Helpers/WaitHelpers.cs`)
Explicit wait mechanisms for reliable test execution:
- Retry logic with timeout handling
- Null-reference protection
- TimeoutException on element not found

---

## 📊 Test Execution Flow

```
┌─────────────────────────────────────────────────┐
│         Test Scenario Starts                    │
└────────────────┬────────────────────────────────┘
                 │
                 ▼
┌─────────────────────────────────────────────────┐
│    [BeforeScenario Hook]                        │
│    - Launch BankSystem.exe                      │
│    - Initialize ApplicationManager              │
│    - Initialize UIA3Automation                  │
│    - Store in ScenarioContext                   │
└────────────────┬────────────────────────────────┘
                 │
                 ▼
┌─────────────────────────────────────────────────┐
│    Execute Step Definitions                     │
│    - Find UI elements using FlaUI               │
│    - Perform actions (click, type, etc.)        │
│    - Validate results                           │
└────────────────┬────────────────────────────────┘
                 │
          ┌──────┴──────┐
          │ Pass / Fail  │
          └──────┬───────┘
                 │
                 ▼
┌─────────────────────────────────────────────────┐
│    [AfterScenario Hook]                         │
│    - If FAILED: Capture screenshot              │
│    - Close application                          │
│    - Dispose resources                          │
│    - Clean up ScenarioContext                   │
└────────────────┬────────────────────────────────┘
                 │
                 ▼
┌─────────────────────────────────────────────────┐
│         Test Scenario Ends                      │
└─────────────────────────────────────────────────┘
```

---

## 🎮 UI Automation with FlaUI

FlaUI provides access to UI elements using Microsoft UI Automation:

```csharp
// Find element by name
var element = _mainWindow.FindFirstDescendant(_cf.ByName("ElementName"));

// Find element by automation ID
var element = _mainWindow.FindFirstDescendant(_cf.ByAutomationId("AutomationId"));

// Interact with elements
element.AsButton().Click();
element.AsTextBox().Enter("text");
element.AsCheckBox().Check();
element.AsComboBox().Select("option");
```

---

## 📝 Gherkin Scenario Format

Tests are written in Gherkin language (human-readable):

```gherkin
@Automation @Regression
Scenario: TC001 User registers with valid credentials
    Given the BankSystem application is launched
    When I click the Registration button
    When I enter the following user details:
        | FieldId    | Value                 |
        | Firstname  | Srikanth              |
        | Email      | test@example.com      |
    Then I click the Ok button
```

---

## 🛠️ Troubleshooting

### Application Not Found
- **Issue**: BankSystem.exe not found at the specified path
- **Solution**: Update `DefaultExePath` in `LaunchUrl.cs` to match your build output location

### UI Element Not Found
- **Issue**: FlaUI cannot locate expected UI elements
- **Solution**: 
  - Verify element names/automation IDs using Inspect.exe (Windows SDK)
  - Increase wait timeouts in test steps
  - Check if application UI has changed

### Test Timeout
- **Issue**: Tests exceed timeout duration
- **Solution**: 
  - Increase `MediumWait` and `ShortWait` constants in steps
  - Verify application performance
  - Check system resource availability

### Screenshot Capture Fails
- **Issue**: Screenshots not captured on failure
- **Solution**: 
  - Verify "Screenshots" folder exists or can be created
  - Check file write permissions
  - Review test logs for detailed error messages

---

## 📋 Configuration Files

### `reqnroll.json`
Reqnroll (Cucumber) configuration for feature file parsing and test execution.

### `.csproj` Files
- **BankSystem.csproj**: .NET Framework 4.8 Windows Forms project
- **BankSystemDemo.Automation.csproj**: .NET 8 test automation project

---

## 🔍 Logging and Reporting

### Serilog Integration
Structured logging is configured in the automation framework:
- File-based logs in `Logs/` directory
- Configurable log levels (Debug, Info, Warning, Error)
- Structured JSON output for easy parsing

### NUnit Test Results
Test results are generated in standard NUnit format:
- Test counters (passed, failed, skipped)
- Execution duration
- Stack traces for failures
- Screenshot attachments on failure

---

## 🚦 Best Practices

1. **Page Object Model**: Encapsulate UI element locators in page classes
2. **Explicit Waits**: Use `WaitHelpers.RetryWhileNull()` instead of `Thread.Sleep()`
3. **Data-Driven Testing**: Use DataTable for parameterized test data
4. **Meaningful Step Names**: Write steps that read like plain English
5. **Error Handling**: Wrap steps in try-catch with descriptive messages
6. **Screenshot on Failure**: Automatically captured in `AfterScenario` hook
7. **Scenario Cleanup**: Use `AfterScenario` hook to clean up resources

---

## 📚 Resources

- **FlaUI GitHub**: https://github.com/FlaUI/FlaUI
- **FlaUI Documentation**: https://flauiui.readthedocs.io/
- **Reqnroll Documentation**: https://reqnroll.net/
- **NUnit Documentation**: https://docs.nunit.org/
- **UI Automation Provider**: https://learn.microsoft.com/en-us/windows/win32/winauto/uiauto-providers

---

## 👨‍💻 Contributing

This is a demonstration project showcasing UI automation testing practices. Contributions and improvements are welcome!

### Future Enhancements
- [ ] Add page object models for all application screens
- [ ] Implement cross-browser testing (if web app migration occurs)
- [ ] Add performance testing scenarios
- [ ] Create data-driven test suites from external sources
- [ ] Implement parallel test execution
- [ ] Add visual regression testing

---

## 📄 License

This project is part of the FlaUI demonstration repository. Please refer to the main repository for license information.

---

## 📧 Support & Contact

For questions or issues related to this automation framework:
- Repository: https://github.com/sree980/FlaUI
- Issue Tracker: https://github.com/sree980/FlaUI/issues

---

## 🎓 Learning Outcomes

By studying this project, you will learn:
- ✅ Modern UI automation testing with FlaUI
- ✅ BDD test development with Reqnroll/Cucumber
- ✅ Page Object Model design pattern
- ✅ Windows Forms application testing
- ✅ Test lifecycle hooks and setup/teardown patterns
- ✅ Exception handling in test automation
- ✅ Screenshot capture and logging in tests
- ✅ Cross-framework testing (.NET Framework + .NET 8)

---

Thank You!!
