# 🧪 TokeroTests - Playwright + NUnit Test Framework

## 📌 Project Overview
This framework automates **integration testing** for the Tokero website using:

- ✅ **C# & .NET 9.0**
- ✅ **NUnit** for structured test execution
- ✅ **Microsoft Playwright** for browser automation
- ✅ **Parallel execution** support for speed

---

## 📂 Project Structure

```plaintext
TokeroTests/
├── Global/             # Global setup, utilities, configs
├── Tests/              # All automated test cases
├── Reports/            # Test reports, screenshots, logs
├── TestData/           # Test configuration files (JSON, etc.)
├── README.md           # Project documentation
```

---

## Installation Guide

### 1️⃣ Install Prerequisites

Make sure you have the following installed:

- ✅ [.NET 9.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
- ✅ [JetBrains Rider](https://www.jetbrains.com/rider/) (or Visual Studio)
- ✅ Microsoft Playwright CLI
- ✅ NUnit Test Framework

---

### 2️⃣ Setup Project

- ☑️ *Installs dependencies*
- ☑️ *Prepares Playwright browsers*
- ☑️ *Sets up project structure*

Run the following commands in your terminal or IDE terminal (e.g., Rider):

```sh
dotnet add package NUnit
dotnet add package NUnit3TestAdapter
dotnet add package Microsoft.Playwright
playwright install
```
---

## 🚀 Running Tests

You can run tests from the command line using the .NET CLI. All commands below assume you're in the `TokeroTests/` root directory.

## ⚡ Performance Tests - Run Separately

**Performance tests** should be executed independently to ensure accurate results and avoid potential conflicts with functional tests.

### Why Run Performance Tests Separately?

- **Avoid Interference**: Running functional tests alongside performance tests can lead to inconsistent performance metrics due to shared system resources.
- **Resource Contention**: Both test types might compete for resources, affecting the performance results.
- **Ensure Accurate Metrics**: Running performance tests in isolation provides clear, reliable load and response time data.

### 🏃‍♂️ How to Run Performance Tests Separately

To run performance tests in isolation, use the following command:

```bash
dotnet test --filter Category=Performance
```

### ✅ Run All Tests

Runs all tests in the project using NUnit:

```sh
dotnet test
```

⚡ Run Tests in Parallel
By default, NUnit runs tests in parallel if properly configured. You can also filter and run specific categories (e.g., UI tests):

```sh
dotnet test --filter Category=UI
```



Run a Specific Test Class
To not run a specific test method, use the --filter flag with the fully qualified name of the test class and the operator "!=" :

```sh
dotnet test --filter "Category!=Performance"
```

Run a Specific Test Method
To run a specific test method, use the --filter flag with the fully qualified name of the test method:

```sh
dotnet test --filter "Category=CrossBrowser"
```

Clean & Rebuild Before Running Tests (Optional)
To clean and rebuild your project before running tests:

```
dotnet clean
dotnet build
dotnet test
```
