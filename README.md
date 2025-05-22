# Selenium UI Test 
This repository contains automated UI tests for the OrangeHRM demo site, built with **Selenium WebDriver**, **NUnit**, and **C#**.

## Technologies Used
- .NET 8
- Selenium WebDriver
- NUnit
- ChromeDriver
- GitHub Actions (CI/CD)

## Prerequisites
- [.NET SDK 8.0+](https://dotnet.microsoft.com/en-us/download)
- [Google Chrome](https://www.google.com/chrome/)
- Git

## Getting Started

### Clone the Repository
```bash
git clone https://github.com/yourusername/selenium-ui-tests.git
cd selenium-ui-tests
dotnet restore
dotnet test --no-restore --verbosity normal
```
## CI with Github Actions
A Ci Pipeline is includes in .github/workflows/test.yml, which:
- Installs Google Chrome and ChromeDriver

- Runs the tests in headless mode

- Uploads test results (.trx) as artifacts

## One-Command Setup
### Windows Shell
```bash
start .\setup.bat
```

## Notes
- Tests are designed to run headlessly by default.

- ChromeDriver installation is managed automatically in CI.
