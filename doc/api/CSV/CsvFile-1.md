# CsvFile<T> Class
**Namespace:** SearchAThing.CSV

**Inheritance:** Object → CsvFile<T>

manage csv file properties such as pathfilename, field separator, decimal separator
            and templated object columns

## Signature
```csharp
public abstract class CsvFile
```
## Constructors
|**Name**|**Summary**|
|---|---|
|[CsvFile<T>(string, string, string, IReadOnlyDictionary<string, string>)](CsvFile-1/ctors.md)|csv reader/writer base class.<br/>            if specified propNameHeaderMapping allor to specify mapping between propertyname and a custom header.<br/>            (useful if can't evaluated at compile time using CsvHeaderAttribute).|
## Methods
|**Name**|**Summary**|
|---|---|
|[Equals](CsvFile-1/Equals.md)||
|[GetHashCode](CsvFile-1/GetHashCode.md)||
|[GetType](CsvFile-1/GetType.md)||
|[ToString](CsvFile-1/ToString.md)||
## Properties
|**Name**|**Summary**|
|---|---|
|[Columns](CsvFile-1/Columns.md)|
## Fields
- [DecimalSeparator](CsvFile-1/DecimalSeparator.md)
- [DecimalSeparatorIsInvariant](CsvFile-1/DecimalSeparatorIsInvariant.md)
- [FieldSeparator](CsvFile-1/FieldSeparator.md)
- [Pathfilename](CsvFile-1/Pathfilename.md)
- [PropNameHeaderMapping](CsvFile-1/PropNameHeaderMapping.md)
## Conversions
