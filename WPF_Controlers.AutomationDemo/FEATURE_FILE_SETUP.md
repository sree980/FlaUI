# Fix for Greyed Out Feature File - Complete Setup Guide

## Issue
The `.feature` file appears greyed out in Visual Studio Solution Explorer and syntax highlighting is missing.

## Root Cause
You need the **Reqnroll Visual Studio Extension** for proper feature file recognition and "Go to Steps" functionality.

## Solution

### Step 1: Install Reqnroll Visual Studio Extension

**Option A: Via Visual Studio Extensions Menu (Recommended)**
1. In Visual Studio, go to **Extensions** → **Manage Extensions**
2. Search for: `Reqnroll` (or `SpecFlow` if using older version)
3. Click **Download**
4. Close Visual Studio and let the installer run
5. Reopen Visual Studio

**Option B: Download directly**
- Visit: https://marketplace.visualstudio.com/items?itemName=TechTalkSpecFlow.SpecFlowForVisualStudio
- Or search "Reqnroll" in the Extensions store

### Step 2: Verify Your Project Configuration

Your `reqnroll.json` is correctly configured:
```json
{
  "$schema": "https://schemas.reqnroll.net/reqnroll-config-latest.json",
  "language": {
    "feature": "en"
  },
  "bindingCulture": {
    "name": "en-us"
  }
}
```

### Step 3: Refresh Visual Studio

After installing the extension:
1. **Close and reopen Visual Studio** completely
2. Right-click on the project → **Unload Project**
3. Right-click again → **Reload Project**
4. The feature file should now display in **normal color** (not greyed out)

### Step 4: Verify "Go to Steps" Works

In your `MainWindow.feature` file:
1. **Right-click** on any step like: `Given the application is started`
2. Select **"Go to Definition"** or press **F12**
3. It should navigate to `MainWindowSteps.cs` → `GivenTheApplicationIsStarted()`

## Expected Result After Fix

✅ Feature file displays in **normal color**
✅ Step text has **syntax highlighting**
✅ "Go to Definition" / "Go to Implementation" works
✅ Test Explorer recognizes all scenarios as runnable tests
✅ Intellisense provides step suggestions

## Build Status
✓ Build Successful (all feature files properly code-generated)
✓ Step definitions found: 30+ steps in MainWindowSteps.cs
✓ Configuration file: reqnroll.json ✓

## If Still Greyed Out

Try these additional steps:
1. Delete `bin/` and `obj/` folders
2. Run: `dotnet clean && dotnet build`
3. Restart Visual Studio
4. Try unloading/reloading the project again

## Project Dependencies Verified
- ✓ Reqnroll.NUnit v3.2.1
- ✓ NUnit v4.4.0
- ✓ FlaUI.Core v5.0.0
- ✓ Target: .NET 8 (net8.0-windows)
