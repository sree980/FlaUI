# Functional Document - WpfControlsDemo

## Overview
WpfControlsDemo is a simple single-window WPF application showcasing a variety of standard WPF controls. Each control has been given an AutomationId to make it automation-friendly.

## Purpose
Provide a consistent testbed for UI automation learning and POC for FlaUI. Testers can use this app to practice locating and interacting with common controls including trees, grids, tabs, menus, and dialogs.

## Controls and Behaviour
1. **Button (BtnSimple)**
   - AutomationId: `BtnSimple`
   - When clicked, updates label `LblStatus` to `Status: Button clicked`.

2. **Label (LblStatus)**
   - AutomationId: `LblStatus`
   - Displays status messages.

3. **Hyperlink (HyperlinkText)**
   - AutomationId: `HyperlinkText`
   - Opens default browser to https://github.com/ when clicked.

4. **TextBox (TxtInput)**
   - AutomationId: `TxtInput`
   - Contains sample text by default.

5. **ComboBox (CmbOptions) and items (CmbItem1..3)**
   - AutomationId: `CmbOptions` and item ids `CmbItem1`, `CmbItem2`, `CmbItem3`.
   - Default selection: Option 1.

6. **CheckBox (ChkAgree)**
   - AutomationId: `ChkAgree`.

7. **RadioButtons (RbA, RbB)**
   - AutomationIds: `RbA`, `RbB`.
   - GroupName: `grp`.

8. **ListBox (LstBox) and items**
   - AutomationId: `LstBox`
   - Items: `LstBoxItem1`, `LstBoxItem2`, `LstBoxItem3`.

9. **ListView (ListViewSimple)**
   - AutomationId: `ListViewSimple`
   - Bound to in-memory collection (Alpha, Bravo, Charlie).

10. **DataGrid (DataGridSample)**
    - AutomationId: `DataGridSample`
    - Columns: Id, Description, Active. Populated with 3 rows.

11. **TreeView (TreeSample)**
    - AutomationId: `TreeSample`
    - Structure: Root -> Child1 -> Child1A; Root -> Child2

12. **TabControl (Tabs)**
    - AutomationId: `Tabs` with TabOne and TabTwo.

13. **Slider (SldValue)**
    - AutomationId: `SldValue` Value default: 40

14. **ProgressBar (PbProgress)**
    - AutomationId: `PbProgress` Value default: 40

15. **Window Dialog (BtnShowDialog)**
    - AutomationId: `BtnShowDialog`
    - Shows a modal dialog with text "This is a dialog".

16. **Menu / MenuItem (MainMenu, MenuFile, MenuHelp, etc.)**
    - AutomationIds provided for top-level and child items.
    - `MenuFileOpen` shows an Open clicked MessageBox.
    - `MenuFileExit` closes the app.
    - `MenuHelpAbout` shows About message box.

## Test Scenarios (examples)
- Verify clicking `BtnSimple` updates `LblStatus`.
- Verify `TxtInput` accepts input.
- Verify selecting different `CmbOptions` updates selection.
- Verify `LstBox` selection and count.
- Verify `ListViewSimple` contains 3 rows with expected names.
- Verify `DataGridSample` rows and checkbox state.
- Verify `TreeSample` node expansion and child presence.
- Verify modal dialog appears when `BtnShowDialog` clicked and can be closed.
- Verify menu actions trigger expected MessageBoxes or app exit.

## Automation Tips
- Use FlaUInspect to confirm AutomationIds.
- For DataGrid interaction, use cell selection or item patterns.
- Modal dialogs should be handled with Wait + FindTopLevelWindow.