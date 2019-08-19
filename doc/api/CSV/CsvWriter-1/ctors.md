# SearchAThing.CSV.CsvWriter<T> constructors
## CsvWriter<T>(string, string, string, IReadOnlyDictionary<string, string>)
construct a csv writer to write on given filename with field and decimal separators.
            if specified propNameHeaderMapping allor to specify mapping between propertyname and a custom header.
            (useful if can't evaluated at compile time using CsvHeaderAttribute.

### Signature
```csharp
public CsvWriter<T>(string pathfilename, string fieldSeparator = ",", string decimalSeparator = ".", IReadOnlyDictionary<string, string> propNameHeaderMapping = null)
```
### Remarks

