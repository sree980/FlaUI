Feature: Bank System Registration
  As a user
  I want to register in the Bank System application
  So that I can create an account with my details

@Automation @Regression
  Scenario:TC001 BankSystem - Data Entering in TextBox 
    Given the BankSystem application is launched
    When I click the Registration button
    When I enter the following user details:
      | FieldId    | Value                 |
      | Firstname  | Srikanth              |
      | Lastname   | Kalamanda             |  
      | Phone      |            9876543210 |
      | Email      | jhabcdefhjk@gmail.com |
      | Password   |                 12345 |
      | CardNumber |          456378963215 |
    When I check the VIP checkbox
    Then I click the Ok button

    Scenario:TC002 BankSystem - DropDown Selection 
    Given the BankSystem application is launched
    When I click the Registration button
    When I enter the following user details:
      | FieldId    | Value                 |
      | Age        |                     4 |
      | Country    | India                 |
    When I check the VIP checkbox
    Then I click the Ok button

    @Automation @Regression
    Scenario:TC003 BankSystem - CheckBox Selection 
    Given the BankSystem application is launched 
    Given the BankSystem application is launched
    When I click the Registration button
    When I check the VIP checkbox
    Then I click the Ok button

    @Automation @Regression
    Scenario:TC004 Test for Dropdown Scenario 
    Given the BankSystem application is launched
    When I click the Registration button
    And click the dropdown button and select age
      | FieldId | Value |
      | Age     |     4 | 

    @Automation @Regression
    Scenario:TC005 Test for Contact page feedback Scenario 
    Given the BankSystem application is launched
    When click on Contact Us button and page opened
	Then I enter feedback contact information 
    | Page      | Value                 |
    | ContactUs | This is the Test call |  

    @Automation @Regression
    Scenario:TC006 Test for Exchange Rate button 
    Given the BankSystem application is launched
    When click on Exchange button and page opened
 
     @Automation @Regression
    Scenario:TC007 Test for About Us button 
    Given the BankSystem application is launched
    When click on About button and page opened

    @Automation @Regression
    Scenario: TC008 BankSystem - Capture the Screenshot
    Given the BankSystem application is launched
	Then I Capture the screenshot of the application

   
   @Automation @Regression
    Scenario:TC009 Test for Exit the Application
    Given the BankSystem application is launched
    Then I should be able to exit the application