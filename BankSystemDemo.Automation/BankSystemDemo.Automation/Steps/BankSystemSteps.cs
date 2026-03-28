using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Conditions;
using FlaUI.Core.Patterns;
using FlaUI.UIA3;
using Reqnroll;
using BankSystemDemo.Automation.Helpers;
using FlaUI.Core.Capturing;

namespace BankSystemDemo.Automation.Steps
{
    [Binding]
    public class BankSystemSteps
    {

        private Application? _application;
        private ApplicationManager? _applicationManager;
        private UIA3Automation? _automation;
        private AutomationElement? _mainWindow;
        private ConditionFactory? _cf;
        private const int ShortWait = 500;
        private const int MediumWait = 1000;

        [Given("the BankSystem application is launched")]
        public void GivenTheBankSystemApplicationIsLaunched()
        {
            try
            {
                Console.WriteLine("Launching BankSystem application...");

                _applicationManager = ScenarioContext.Current.Get<ApplicationManager?>("AppManager");
                _application = _applicationManager!.App;
                Thread.Sleep(MediumWait);

                _automation = new UIA3Automation();
                _mainWindow = _application!.GetMainWindow(_automation);

                if (_mainWindow == null)
                {
                    throw new Exception("Failed to get main window.");
                }

                _cf = new ConditionFactory(new UIA3PropertyLibrary());
                Console.WriteLine("✓ BankSystem application launched successfully.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error launching BankSystem application: {ex.Message}", ex);
            }
        }

        [When("I click the Registration button")]
        public void WhenIClickTheRegistrationButton()
        {
            try
            {
                Console.WriteLine("Clicking Registration button...");
                var registrationButton = _mainWindow!.FindFirstDescendant(_cf.ByName("Registration"));

                if (registrationButton == null)
                {
                    throw new Exception("Registration button not found.");
                }

                registrationButton.AsButton().Click();
                Thread.Sleep(ShortWait);
                Console.WriteLine("✓ Registration button clicked.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error clicking Registration button: {ex.Message}", ex);
            }
        }

        [When("I enter the following user details:")]
        public void WhenIEnterTheFollowingUserDetails(DataTable dataTable)
        {
            try
            {
                Console.WriteLine("Entering user details...");
                var userDetails = new Dictionary<string, string>();

                foreach (var row in dataTable.Rows)
                {
                    userDetails[row["FieldId"]] = row["Value"];
                }

                // First Name
                if (userDetails.ContainsKey("Firstname"))
                {
                    _mainWindow!.FindFirstDescendant(_cf!.ByAutomationId("InFName")).AsTextBox()!.Enter(userDetails["Firstname"]);
                    
                }

                // Last Name
                if (userDetails.ContainsKey("Lastname"))
                {
                    _mainWindow!.FindFirstDescendant(_cf!.ByAutomationId("InLName")).AsTextBox()!.Enter(userDetails["Lastname"]);

                }

                // Age (ComboBox)
                if (userDetails.ContainsKey("Age"))
                {
                    var field = _mainWindow!.FindFirstDescendant(_cf.ByAutomationId("InAge"));
                    if (field != null)
                    {
                        ComboBox combo = field.AsComboBox();
                        IExpandCollapsePattern pattern = combo.Patterns.ExpandCollapse.Pattern;
                        pattern.Expand();
                        Thread.Sleep(ShortWait);
                        combo.Select(int.Parse(userDetails["Age"]));
                    }
                }

                // Country (ComboBox)
                if (userDetails.ContainsKey("Country"))
                {
                    var field = _mainWindow!.FindFirstDescendant(_cf.ByAutomationId("InCountry"));
                    if (field != null)
                    {
                        ComboBox combo = field.AsComboBox();
                        IExpandCollapsePattern pattern = combo.Patterns.ExpandCollapse.Pattern;
                        pattern.Expand();
                        Thread.Sleep(ShortWait);
                        combo.Select(userDetails["Country"]);
                    }
                }

                // Phone
                if (userDetails.ContainsKey("Phone"))
                {
                    var field = _mainWindow!.FindFirstDescendant(_cf.ByAutomationId("InPhone"));
                    if (field != null)
                        field.AsTextBox().Enter(userDetails["Phone"]);
                }

                // Email
                if (userDetails.ContainsKey("Email"))
                {
                    var field = _mainWindow!.FindFirstDescendant(_cf.ByAutomationId("InEmail"));
                    if (field != null)
                        field.AsTextBox().Enter(userDetails["Email"]);
                }

                // Password
                if (userDetails.ContainsKey("Password"))
                {
                    _mainWindow!.FindFirstDescendant(_cf!.ByAutomationId("InPass")).AsTextBox()!.Enter("12345");
                }

                // Card Number
                if (userDetails.ContainsKey("CardNumber"))
                {
                    var field = _mainWindow!.FindFirstDescendant(_cf!.ByAutomationId("InCard"));
                    if (field != null)
                        field.AsTextBox().Enter(userDetails["CardNumber"]);
                }

                // VIP Status (Checkbox)
                if (userDetails.ContainsKey("VipStatus") && userDetails["VipStatus"].ToLower() == "true")
                {
                    var field = _mainWindow!.FindFirstDescendant(_cf!.ByAutomationId("VipCheck"));
                    if (field != null)
                        field.AsCheckBox().Click();
                }

                Console.WriteLine("✓ User details entered successfully.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error entering user details: {ex.Message}", ex);
            }
        }

        [When("click the dropdown button and select age")]
        public void WhenClickTheDropdownButtonAndSelectAge(DataTable dataTable)
        {
            var userDetails = new Dictionary<string, string>();

            foreach (var row in dataTable.Rows)
            {
                userDetails[row["FieldId"]] = row["Value"];
            }

            if (userDetails.ContainsKey("Age"))
            {
                var field = _mainWindow!.FindFirstDescendant(_cf!.ByAutomationId("InAge"));
                if (field != null)
                {
                    ComboBox combo = field.AsComboBox();
                    IExpandCollapsePattern pattern = combo.Patterns.ExpandCollapse.Pattern;
                    pattern.Expand();
                    Thread.Sleep(ShortWait);
                    combo.Select(int.Parse(userDetails["Age"]));
                }
            }
        }

        [When("I check the VIP checkbox")]
        public void WhenICheckTheVIPCheckbox()
        {
            _mainWindow!.FindFirstDescendant(_cf!.ByAutomationId("VipCheck")).AsCheckBox()!.Click();

            Console.WriteLine("✓ User details entered successfully.");
        }

        [Then("I click the Ok button")]
        public void WhenIClickTheOkButton()
        {
            try
            {
                Console.WriteLine("Clicking Ok button...");
                var okButton = _mainWindow.FindFirstDescendant(_cf.ByName("Ok"));

                if (okButton == null)
                {
                    throw new Exception("Ok button not found.");
                }

                okButton.AsButton().Click();
                Thread.Sleep(MediumWait);
                Console.WriteLine("✓ Ok button clicked.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error clicking Ok button: {ex.Message}", ex);
            }
        }

        [Then("a Congratulations window should appear")]
        public void ThenACongratulationsWindowShouldAppear()
        {
            try
            {
                Console.WriteLine("Verifying Congratulations window...");
                Thread.Sleep(ShortWait);

                var congratulationsWindow = _mainWindow!.FindFirstDescendant(_cf!.ByName("Congratulations"));

                if (congratulationsWindow == null)
                {
                    throw new Exception("Congratulations window did not appear.");
                }

                Console.WriteLine("✓ Congratulations window verified.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error verifying Congratulations window: {ex.Message}", ex);
            }
        }

        [Then("I should be able to close the confirmation dialog")]
        public void ThenIShouldBeAbleToCloseTheConfirmationDialog()
        {
            try
            {
                Console.WriteLine("Closing confirmation dialog...");
                var congratulationsWindow = _mainWindow!.FindFirstDescendant(_cf!.ByName("Congratulations"));

                if (congratulationsWindow == null)
                {
                    throw new Exception("Congratulations window not found.");
                }

                var okButton = congratulationsWindow.FindFirstDescendant(_cf.ByName("OK"));
                if (okButton == null)
                {
                    throw new Exception("OK button not found.");
                }

                okButton.AsButton().Click();
                Thread.Sleep(ShortWait);
                Console.WriteLine("✓ Confirmation dialog closed.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error closing confirmation dialog: {ex.Message}", ex);
            }
        }

        [Then("I should be able to exit the application")]
        public void ThenIShouldBeAbleToExitTheApplication()
        {
            try
            {
                Console.WriteLine("Exiting application...");
                var exitButton = _mainWindow!.FindFirstDescendant(_cf!.ByName("Exit"));

                if (exitButton == null)
                {
                    throw new Exception("Exit button not found.");
                }

                exitButton.AsButton().Click();
                Thread.Sleep(ShortWait);

                var exitWindow = _mainWindow.FindFirstDescendant(_cf.ByName("Exit"));
                if (exitWindow != null)
                {
                    var yesButton = exitWindow.FindFirstDescendant(_cf.ByName("Yes"));
                    if (yesButton != null)
                    {
                        yesButton.AsButton().Click();
                        Thread.Sleep(ShortWait);
                    }
                }

                Console.WriteLine("✓ Application exited successfully.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error exiting application: {ex.Message}", ex);
            }
        }

        [When("click on Contact Us button and page opened")]
        public void WhenClickOnContactUsButton()
        {
           _mainWindow!.FindFirstDescendant(_cf!.ByName("Contact Us")).AsButton()!.Click();
        }

        [Then("I Capture the screenshot of the application")]
        public void ThenICaptureTheScreenshotOfTheApplication()
        {

            var loginBtn = _mainWindow!.FindFirstDescendant(_cf!.ByName("Log In"));
            var loginImg = Capture.Element(loginBtn!);
            loginImg.ToFile(@"C:\Automation\FlaUIBankSystem\BankSystemDemo.Automation\Screenshots\Login Button.png");

        }

        [When("click on Exchange button and page opened")]
        public void WhenClickOnExchangeButtonAndPageOpened()
        {
            _mainWindow!.FindFirstDescendant(_cf.ByName("ExChange Rates")).AsButton()!.Click();
            Console.WriteLine("Clicked on Exchange Rates button.");
        }

        [Then("Verify the pop up window and click on OK button")]
        public void ThenVerifyThePopUpWindowAndClickOnOKButton()
        {
            _mainWindow!.FindFirstDescendant(_cf.ByName("OK")).AsButton()!.Click();
            Console.WriteLine("Clicked OK button on pop-up window.");
        }

        [When("click on About button and page opened")]
        public void WhenClickOnAboutButtonAndPageOpened()
        {
            _mainWindow!.FindFirstDescendant(_cf.ByName("About Us")).AsButton()!.Click();
            Console.WriteLine("Clicked on About Us button.");
        }

        [Then("I enter feedback contact information")]
        public void ThenIEnterFeedbackContactInformation(DataTable dataTable)
        {
            var userDetails = new Dictionary<string, string>();

            foreach (var row in dataTable.Rows)
            {
                userDetails[row["Page"]] = row["Value"];
            }
            if (userDetails.ContainsKey("ContactUs"))
            {
                var field = _mainWindow!.FindFirstDescendant(_cf!.ByName("Please Leave Your Message Here"));
                if (field != null)
                    field.AsTextBox().Enter(userDetails["ContactUs"]);
            }
        }
    }
}
