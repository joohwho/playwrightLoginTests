Feature: LoginApp

A short summary of the feature

@loginApp
Scenario: Login sucessfully
	Given I am on login page
	Given I fill in "student" in the Username
	Given I fill in "Password123" in the Password
	When I press the Submit button
	Then I should see the "Logged In Successfully" page
