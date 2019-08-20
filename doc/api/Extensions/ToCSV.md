# SearchAThing.Extensions.ToCSV method
## ToCSV<T>(IEnumerable<T>, string, CsvOptions)
generate csv file from this object enumerable using properties name and their order as csv header.
            if specified propNameHeaderMapping allor to specify mapping between propertyname and a custom header.
            (useful if can't evaluated at compile time using CsvHeaderAttribute.

### Signature
```csharp
public static void ToCSV<T>(IEnumerable<T> coll, string pathfilename, CsvOptions options = null)
```
### Returns

### Remarks

