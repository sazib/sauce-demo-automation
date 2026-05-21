# Test Data

This directory can be used to store test data files such as:

- CSV files with test data
- JSON files with test configurations
- Excel files with data-driven test inputs
- Sample files for upload testing

## Usage Example

```csharp
// Reading test data from CSV
var testDataPath = Path.Combine("TestData", "users.csv");
var users = File.ReadAllLines(testDataPath);
```

## Best Practices

1. **Keep data separate from code** - Easier to maintain and update
2. **Use meaningful file names** - Clearly indicate what data is contained
3. **Version control test data** - Track changes to test data
4. **Document data format** - Add README or comments explaining structure
5. **Avoid sensitive data** - Never commit real credentials or PII
