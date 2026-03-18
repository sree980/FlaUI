Feature: Main Window Controls
  As a tester
  I want to verify basic controls on the main window
  So that the desktop app works correctly

  @ui @smoke
  Scenario: Click button updates status
    Given the application is started
    When I click the simple button
    Then the page verifies the status is updated

  @ui
  Scenario: TextBox accepts input
    Given the application is started
    When I enter "hello from POC" into the input textbox
    Then the input textbox should contain "hello from POC"

  @ui
  Scenario: ComboBox selection updates selection
    Given the application is started
    When I select "Option 2" from the combo box
    Then the page verifies combo box selection is "Option 2"

  @ui
  Scenario: ListBox selection and count
    Given the application is started
    When I inspect the listbox
    Then the page verifies listbox has 3 items and selects the second item

  @ui
  Scenario: ListView contains expected 3 rows
    Given the application is started
    When I inspect the listview rows
    Then the page verifies listview contains rows "Alpha","Bravo","Charlie"

  @ui
  Scenario: DataGrid rows and checkbox state
    Given the application is started
    When I inspect the datagrid
    Then the page verifies datagrid has 3 rows and the Active state for row 1 is true

  @ui
  Scenario: TreeView node expansion and child presence
    Given the application is started
    When I expand the root node
    Then the page verifies child nodes exist and Child 1A is present under Child 1

  @ui
  Scenario: Modal dialog appears and can be closed
    Given the application is started
    When I open the dialog
    Then the page verifies the modal dialog appears and closes it

  @ui
  Scenario: Menu actions trigger expected messageboxes or exit
    Given the application is started
    When I click Menu->File->Open
    Then the page verifies an 'Open clicked' messagebox is shown
    When I click Menu->Help->About
    Then the page verifies an 'About' messagebox is shown
