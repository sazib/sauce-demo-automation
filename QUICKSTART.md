# Quick Start Guide

Get up and running with the SauceDemo Automation Framework in 5 minutes!

## Prerequisites Check

```bash
# Check if .NET is installed
dotnet --version
# Should show: 8.0.x or later

# If not installed, download from:
# https://dotnet.microsoft.com/download/dotnet/8.0
```

## 3-Step Setup

### Step 1: Clone the Repository

```bash
git clone <repository-url>
cd SauceDemo.AutomationFramework
```

### Step 2: Restore Dependencies

```bash
dotnet restore
```

### Step 3: Run Tests

```bash
# Run all tests
dotnet test

# Or use the provided script
./run-tests.sh              # Linux/Mac
```

## That's It!

You should see tests running in Chrome browser.

## Common Commands

```bash
# Run smoke tests only
dotnet test --filter "Category=smoke"

# Run in headless mode
export HEADLESS=true        # Linux/Mac
dotnet test

# Run in Firefox
export BROWSER=Firefox      # Linux/Mac
dotnet test

# Run with detailed output
dotnet test --verbosity detailed
```

## Using the Test Scripts

### Linux/Mac

```bash
# Make script executable (first time only)
chmod +x run-tests.sh

# Run all tests
./run-tests.sh

# Run smoke tests in headless Chrome
./run-tests.sh -b Chrome -h -f "Category=smoke"

# See all options
./run-tests.sh --help
```


## IDE Setup

### Visual Studio

1. Open `SauceDemo.AutomationFramework.sln`
2. Build solution (Ctrl+Shift+B)
3. Open Test Explorer (Test → Test Explorer)
4. Click "Run All"

### Rider

1. Open `SauceDemo.AutomationFramework.sln`
2. Build solution (Ctrl+Shift+F9)
3. Open Unit Tests (View → Tool Windows → Unit Tests)
4. Right-click and run tests

### VS Code

1. Open folder in VS Code
2. Install C# Dev Kit extension
3. Open Testing view (Ctrl+Shift+T)
4. Run tests from explorer

## Configuration

### Quick Config Changes

Edit `SauceDemo.Tests/appsettings.json`:

```json
{
  "TestSettings": {
    "Browser": "Chrome",        // Chrome, Firefox, or Edge
    "Headless": false,          // true for headless mode
    "LogLevel": "Information"   // Debug, Information, Warning, Error
  }
}
```

### Environment Variables (Override Config)

```bash
# Linux/Mac
export BROWSER=Firefox
export HEADLESS=true
export BASE_URL=https://www.saucedemo.com

# Windows PowerShell
$env:BROWSER="Firefox"
$env:HEADLESS="true"
$env:BASE_URL="https://www.saucedemo.com"
```

## Test Results

After running tests, check:

- **Console Output**: Real-time test results
- **Test Results**: `SauceDemo.Tests/TestResults/*.trx`
- **Logs**: `SauceDemo.Tests/logs/*.log`
- **Screenshots** (on failure): `SauceDemo.Tests/Screenshots/*.png`

## Troubleshooting

### "dotnet: command not found"

Install .NET SDK: https://dotnet.microsoft.com/download/dotnet/8.0

### "Driver not found" or "ChromeDriver" errors

The framework automatically downloads drivers. If issues persist:

```bash
# Clear driver cache
rm -rf ~/.webdriver

# Run tests again
dotnet test
```

### Tests timeout or fail

Increase timeouts in `appsettings.json`:

```json
{
  "TestSettings": {
    "ImplicitWaitSeconds": 20,
    "PageLoadTimeoutSeconds": 60
  }
}
```

### Build errors

```bash
# Clean and rebuild
dotnet clean
dotnet restore
dotnet build
```

## Next Steps

- ✅ Read [README.md](README.md) for detailed documentation

## Test Scenarios Included

### 1. End-to-End Purchase ✅
- Complete purchase with single product
- Complete purchase with multiple products

### 2. Negative Scenarios ✅
- Invalid login credentials
- Locked out user
- Missing checkout information

### 3. Navigation & State ✅
- Cart persistence across navigation
- Checkout cancellation
- Product removal

## Quick Reference

| Command | Description |
|---------|-------------|
| `dotnet test` | Run all tests |
| `dotnet test --filter "Category=smoke"` | Run smoke tests |
| `dotnet build` | Build solution |
| `dotnet clean` | Clean build artifacts |
| `./run-tests.sh -h` | Show script help (Linux/Mac) |
| `.\run-tests.ps1 -Help` | Show script help (Windows) |

## Support

For detailed information, see:
- [README.md](README.md) - Complete documentation
---
