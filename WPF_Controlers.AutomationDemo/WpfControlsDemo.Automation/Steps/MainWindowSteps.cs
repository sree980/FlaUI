using Reqnroll;
using WpfControlsDemo.Automation.Helpers;
using WpfControlsDemo.Automation.Helpers.Pages;

namespace WpfControlsDemo.Automation.Steps
{
    [Binding]
    public class MainWindowSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private MainWindowPage _page;
        private ApplicationManager _manager = new ApplicationManager();

        public MainWindowSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;

            if (_scenarioContext.TryGetValue<MainWindowPage>("MainWindowPage", out var p))
            {
                _page = p!;
            }
            else
            {
                _page = new MainWindowPage(_manager, autoStart: true);
                _scenarioContext.Set<ApplicationManager>(_manager, "AppManager");
                _scenarioContext.Set<MainWindowPage>(_page, "MainWindowPage");
            }

            if (_scenarioContext.TryGetValue<ApplicationManager>("AppManager", out var m))
            {
                _manager = m!;
            }
        }

        [Given("the application is started")]
        public void GivenTheApplicationIsStarted()
        {
            _page.Start();
        }

        [When("I click the simple button")]
        public void WhenIClickTheSimpleButton() => _page.Action_ClickSimpleButton();

        [Then("the page verifies the status is updated")]
        public void ThenPageVerifiesStatusIsUpdated() => _page.Verify_StatusContains("Button clicked");

        [When(@"I enter ""(.*)"" into the input textbox")]
        public void WhenIEnterTextIntoTheInputTextbox(string text) => _page.Action_EnterText(text);

        [Then(@"the input textbox should contain ""(.*)""")]
        public void ThenInputTextboxShouldContain(string expected) => _page.Verify_TextBoxContains(expected);

        [When(@"I select ""(.*)"" from the combo box")]
        public void WhenISelectFromComboBox(string option) => _page.Action_SelectComboOption(option);

        [Then(@"the page verifies combo box selection is ""(.*)""")]
        public void ThenPageVerifiesComboSelectionIs(string expected) => _page.Verify_ComboSelectionIs(expected);

        [When("I inspect the listbox")]
        public void WhenIInspectTheListbox() => _page.Action_SelectListBoxItem(1);

        [Then("the page verifies listbox has 3 items and selects the second item")]
        public void ThenVerifyListBox() => _page.Verify_ListBoxCountAndSelected(3,1);

        [When("I inspect the listview rows")]
        public void WhenIInspectListViewRows() { /* no-op action */ }

        [Then("the page verifies listview contains rows \"Alpha\",\"Bravo\",\"Charlie\"")]
        public void ThenVerifyListViewRows() => _page.Verify_ListViewContains("Alpha","Bravo","Charlie");

        [When("I inspect the datagrid")]
        public void WhenIInspectTheDataGrid() { /* no-op action */ }

        [Then("the page verifies datagrid has 3 rows and the Active state for row 1 is true")]
        public void ThenVerifyDataGrid() => _page.Verify_DataGridRowCountAndCheckbox(3,0,true);

        [When("I expand the root node")]
        public void WhenIExpandRootNode() => _page.Action_ExpandRootNode();

        [Then("the page verifies child nodes exist and Child 1A is present under Child 1")]
        public void ThenVerifyTreeNodes() => _page.Verify_TreeHasChildNode("Child 1","Child 1A");

        [When("I open the dialog")]
        public void WhenIOpenTheDialog() => _page.Action_OpenDialog();

        [Then("the page verifies the modal dialog appears and closes it")]
        public void ThenVerifyDialog() => _page.Verify_ModalDialogAppearsAndClose();

        [When("I click Menu->File->Open")]
        public void WhenIClickMenuFileOpen() => _page.Action_ClickMenuFileOpen();

        [Then("the page verifies an 'Open clicked' messagebox is shown")]
        public void ThenVerifyMenuOpenMsgBox() => _page.Verify_MenuFileOpenShowsMessageBoxAndClose();

        [When("I click Menu->Help->About")]
        public void WhenIClickMenuHelpAbout() => _page.Action_ClickMenuHelpAbout();

        [Then("the page verifies an 'About' messagebox is shown")]
        public void ThenVerifyMenuAboutMsgBox() => _page.Verify_MenuHelpAboutShowsMessageBoxAndClose();
    }
}
