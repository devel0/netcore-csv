# SearchAThing.CSV.CsvFile<T> constructors
## CsvFile<T>(string, string, string, IReadOnlyDictionary<string, string>)
csv reader/writer base class.
            if specified propNameHeaderMapping allor to specify mapping between propertyname and a custom header.
            (useful if can't evaluated at compile time using CsvHeaderAttribute).

### Signature
```csharp
public CsvFile<T>(string pathfilename, string fieldSeparator = ",", string decimalSeparator = ".", IReadOnlyDictionary<string, string> propNameHeaderMapping = null)
```
### Remarks

