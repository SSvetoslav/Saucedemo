Selenium Tests for Saucedemo with GitHub Actions
This repository contains Selenium tests for the Saucedemo website, written in C# and executed using .NET. Additionally, it includes a GitHub Actions workflow to run the tests in parallel and publish a detailed test report as an artifact.

Objective
Automate key interactions on the Saucedemo website using Selenium and C#.
Ensure test execution is seamless via GitHub Actions, with parallel jobs to optimize runtime.
Generate and publish detailed test reports for easy review.
Features
Selenium Tests

Scenarios include:
Logging in with valid and invalid credentials.
Adding items to the cart.
Proceeding to checkout.
Completing the checkout process.
GitHub Actions Workflow

Configures dependencies and environment setup.
Executes tests in parallel using a .NET test runner (e.g., NUnit).
Generates comprehensive test reports.
Publishes test reports as artifacts for easy review.
Repository Structure
plaintext
Copy code
├── Saucedemo/
│   ├── Dependencies/         # Contains all installed Nuget packages  
│   ├── Credentials/          # Contains User credentials
│   ├── Pages/                # Page Object Model (POM) classes  
│   ├── Tests/                # Contains test files  
│   ├── Utils/                # Helper and utility classes 
│   └── workflows/  
│       └── selenium_tests.yml # GitHub Actions workflow definition  
└── .gitignore                # Ignored files 

Prerequisites
How to Run Tests Locally
Clone the repository:

bash
    Copy code
    
    git clone <repository_url>  
    
    cd <repository_directory>  
    
Restore dependencies:

bash:

    dotnet restore 

    Run the tests:

    dotnet test  

GitHub Actions Workflow :
The workflow is defined in /.github/workflows/selenium_tests.yml. Key features:

Parallel Execution: Runs tests concurrently to optimize runtime.
Environment Setup: Installs dependencies and sets up Selenium WebDriver.
Test Execution: Executes tests using NUnit.
Test Reporting: Generates a detailed test report.
Artifact Publishing: Publishes the test report as an artifact for review.
Running the Workflow
The workflow triggers automatically on push or pull requests to the main branch. You can also trigger it manually from the GitHub Actions tab.

Test Report
After the workflow execution:

Navigate to the Actions tab in the GitHub repository.
Select the latest run and locate the "Artifacts" section.
Download the test report artifact for detailed results.

NOTE: I am using the latest version of Selenium WebDriver and it is not necessary to specify the path to 'chromedriver.exe'.
