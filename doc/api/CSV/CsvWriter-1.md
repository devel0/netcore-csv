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
|[CsvWriter<T>(string, CsvOptions)](CsvWriter-1/ctors.md)|construct a csv writer to write on given filename with field and decimal separators.|
|[CsvWriter<T>(string, bool, CsvOptions)](CsvWriter-1/ctors.md#csvwritertstring-bool-csvoptions)|construct a csv writer to write on given filename with field and decimal separators.            <br/>            data will appended if file exists|
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
|[AppendMode](CsvWriter-1/AppendMode.md)|
|[Columns](CsvWriter-1/Columns.md)|
|[DecimalSeparator](CsvWriter-1/DecimalSeparator.md)|
|[DecimalSeparatorIsInvariant](CsvWriter-1/DecimalSeparatorIsInvariant.md)|
|[FieldSeparator](CsvWriter-1/FieldSeparator.md)|
|[Options](CsvWriter-1/Options.md)|
|[Pathfilename](CsvWriter-1/Pathfilename.md)|
## Conversions
