# CsvWriter<T> Class
**Namespace:** SearchAThing.CSV

**Inheritance:** Object → CsvFile<T> → CsvWriter<T>

write object data to csv as row autogenerating first row header using object properties as name and order.
            attributes CsvHeaderAttribute and CsvColumnOrderAttribute can be specified on properties to custommize header and order.

## Signature
```csharp
public class CsvWriter : SearchAThing.CSV.CsvFile<T>, System.IDisposable
```
## Constructors
|**Name**|**Summary**|
|---|---|
|[CsvWriter<T>(string, string, string)](CsvWriter-1/ctors.md)|construct a csv writer to write on given filename with field and decimal separators|
## Methods
|**Name**|**Summary**|
|---|---|
|[Dispose](CsvWriter-1/Dispose.md)|dispose the writer closing writer stream|
|[Equals](CsvWriter-1/Equals.md)||
|[GetHashCode](CsvWriter-1/GetHashCode.md)||
|[GetType](CsvWriter-1/GetType.md)||
|[Push](CsvWriter-1/Push.md)||
|[ToString](CsvWriter-1/ToString.md)||
## Properties
|**Name**|**Summary**|
|---|---|
|[Columns](CsvWriter-1/Columns.md)|
## Fields
- [DecimalSeparator](CsvWriter-1/DecimalSeparator.md)
- [DecimalSeparatorIsInvariant](CsvWriter-1/DecimalSeparatorIsInvariant.md)
- [FieldSeparator](CsvWriter-1/FieldSeparator.md)
- [Pathfilename](CsvWriter-1/Pathfilename.md)
## Conversions
