#!/bin/bash

# SauceDemo Test Execution Script
# Usage: ./run-tests.sh [options]

set -e

# Default values
BROWSER="Chrome"
HEADLESS="false"
ENVIRONMENT="Development"
FILTER=""
VERBOSITY="normal"
PARALLEL="false"

# Colors for output
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
NC='\033[0m' # No Color

# Function to print colored output
print_info() {
    echo -e "${GREEN}[INFO]${NC} $1"
}

print_warning() {
    echo -e "${YELLOW}[WARN]${NC} $1"
}

print_error() {
    echo -e "${RED}[ERROR]${NC} $1"
}

# Function to display usage
usage() {
    cat << EOF
Usage: ./run-tests.sh [OPTIONS]

Options:
    -b, --browser BROWSER       Browser to use (Chrome, Firefox, Edge) [default: Chrome]
    -h, --headless              Run in headless mode
    -e, --environment ENV       Test environment (Development, CI) [default: Development]
    -f, --filter FILTER         Test filter (e.g., "Category=smoke")
    -v, --verbosity LEVEL       Verbosity level (quiet, minimal, normal, detailed) [default: normal]
    -p, --parallel              Run tests in parallel
    --help                      Display this help message

Examples:
    ./run-tests.sh                                    # Run all tests with defaults
    ./run-tests.sh -b Firefox -h                      # Run in Firefox headless
    ./run-tests.sh -f "Category=smoke"                # Run smoke tests only
    ./run-tests.sh -b Chrome -h -f "Category=smoke"   # Smoke tests in Chrome headless
    ./run-tests.sh -p                                 # Run tests in parallel

EOF
}

# Parse command line arguments
while [[ $# -gt 0 ]]; do
    case $1 in
        -b|--browser)
            BROWSER="$2"
            shift 2
            ;;
        -h|--headless)
            HEADLESS="true"
            shift
            ;;
        -e|--environment)
            ENVIRONMENT="$2"
            shift 2
            ;;
        -f|--filter)
            FILTER="$2"
            shift 2
            ;;
        -v|--verbosity)
            VERBOSITY="$2"
            shift 2
            ;;
        -p|--parallel)
            PARALLEL="true"
            shift
            ;;
        --help)
            usage
            exit 0
            ;;
        *)
            print_error "Unknown option: $1"
            usage
            exit 1
            ;;
    esac
done

# Print configuration
print_info "Test Execution Configuration:"
echo "  Browser: $BROWSER"
echo "  Headless: $HEADLESS"
echo "  Environment: $ENVIRONMENT"
echo "  Filter: ${FILTER:-None}"
echo "  Verbosity: $VERBOSITY"
echo "  Parallel: $PARALLEL"
echo ""

# Set environment variables
export BROWSER="$BROWSER"
export HEADLESS="$HEADLESS"
export TEST_ENVIRONMENT="$ENVIRONMENT"

# Check if .NET is installed
if ! command -v dotnet &> /dev/null; then
    print_error ".NET SDK is not installed. Please install .NET 8.0 or later."
    exit 1
fi

print_info "Using .NET version: $(dotnet --version)"

# Restore dependencies
print_info "Restoring dependencies..."
dotnet restore SauceDemo.AutomationFramework.sln

# Build solution
print_info "Building solution..."
dotnet build SauceDemo.AutomationFramework.sln --configuration Release --no-restore

# Prepare test command
TEST_CMD="dotnet test SauceDemo.AutomationFramework.sln --configuration Release --no-build --verbosity $VERBOSITY"

# Add filter if specified
if [ -n "$FILTER" ]; then
    TEST_CMD="$TEST_CMD --filter \"$FILTER\""
fi

# Add parallel execution if specified
if [ "$PARALLEL" = "true" ]; then
    TEST_CMD="$TEST_CMD -- NUnit.NumberOfTestWorkers=4"
fi

# Add logger for test results
TEST_CMD="$TEST_CMD --logger \"trx;LogFileName=test-results.trx\""

# Run tests
print_info "Running tests..."
echo "Command: $TEST_CMD"
echo ""

eval $TEST_CMD
TEST_EXIT_CODE=$?

# Check test results
if [ $TEST_EXIT_CODE -eq 0 ]; then
    print_info "✓ All tests passed!"
else
    print_error "✗ Some tests failed. Check the output above for details."
fi

# Display results location
print_info "Test results saved to: SauceDemo.Tests/TestResults/"
print_info "Logs saved to: SauceDemo.Tests/logs/"

if [ "$HEADLESS" = "false" ] && [ $TEST_EXIT_CODE -ne 0 ]; then
    print_info "Screenshots saved to: SauceDemo.Tests/Screenshots/"
fi

exit $TEST_EXIT_CODE
