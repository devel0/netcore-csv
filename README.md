# netcore-csv

[![NuGet Badge](https://buildstats.info/nuget/netcore-csv)](https://www.nuget.org/packages/netcore-csv/)

.NET core CSV

- [API Documentation](#api-documentation)
- [Quickstart](#quickstart)
- [How this project was built](#how-this-project-was-built)

<hr/>

## API Documentation

- [CSVReader](doc/api/CSV/CsvReader-1.md)
- [CSVWriter](doc/api/CSV/CsvWriter-1.md)
- [CsvColumnHeaderAttribute](doc/api/CSV/CsvColumnHeaderAttribute.md)
- [CsvColumnOrderAttribute](doc/api/CSV/CsvColumnOrderAttribute.md)
- [Extensions](doc/api/Extensions.md)

## Quickstart

```csharp
using System;
using System.Linq;
using SearchAThing;
using System.Diagnostics;
using SearchAThing.CSV;

namespace test
{

    public class MyData
    {
        public int i10 { get; set; }
        public int i20 { get; set; }
        [CsvHeader("SomeStringCol")]
        public string s1 { get; set; }
        [CsvHeader("FirstDblCol")]
        public double v1 { get; set; }
        public double v2 { get; set; }
        public double v3 { get; set; }
        public double v4 { get; set; }
        public double v5 { get; set; }
        public double v6 { get; set; }
        public double v7 { get; set; }
        public double v8 { get; set; }
        public double v9 { get; set; }
        public double v10 { get; set; }
        public double v11 { get; set; }
        public double v12 { get; set; }
        public double v13 { get; set; }
        public double v14 { get; set; }
        public double v15 { get; set; }
        public double v16 { get; set; }
        public double v17 { get; set; }
        public double v18 { get; set; }
        public double v19 { get; set; }
        public double v20 { get; set; }
        [CsvColumnOrder(0)]
        public double firstcol { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            const int CNT = 500000;
            {
                var stopw = new Stopwatch();
                stopw.Start();

                using var csv = new CsvWriter<MyData>("test.csv");

                var rnd = new Random();                

                for (int i = 0; i < CNT; ++i)
                {
                    var d = new MyData
                    {
                        v1 = rnd.NextDouble() * 1e9,
                        v2 = rnd.NextDouble() * 1e9,
                        v3 = rnd.NextDouble() * 1e9,
                        v4 = rnd.NextDouble() * 1e9,
                        v5 = rnd.NextDouble() * 1e9,
                        v6 = rnd.NextDouble() * 1e9,
                        v7 = rnd.NextDouble() * 1e9,
                        v8 = rnd.NextDouble() * 1e9,
                        v9 = rnd.NextDouble() * 1e9,
                        v10 = rnd.NextDouble() * 1e9,
                        v11 = rnd.NextDouble() * 1e9,
                        v12 = rnd.NextDouble() * 1e9,
                        v13 = rnd.NextDouble() * 1e9,
                        v14 = rnd.NextDouble() * 1e9,
                        v15 = rnd.NextDouble() * 1e9,
                        v16 = rnd.NextDouble() * 1e9,
                        v17 = rnd.NextDouble() * 1e9,
                        v18 = rnd.NextDouble() * 1e9,
                        v19 = rnd.NextDouble() * 1e9,
                        v20 = rnd.NextDouble() * 1e9,
                        i10 = rnd.Next(1, 10),
                        i20 = rnd.Next(1, 20),
                        s1 = $"str:[{rnd.Next()}]"
                    };

                    csv.Push(d);
                }

                System.Console.WriteLine($"written [{CNT}] rows in {stopw.Elapsed}");
            }

            {
                var stopw = new Stopwatch();
                stopw.Start();

                using var csv = new CsvReader<MyData>("test.csv");

                csv
                    .GroupBy(w => w.i10)
                    .Select(w => new
                    {
                        i10 = w.Key,
                        i20Min = w.Min(r => r.i20),
                        i20Max = w.Max(r => r.i20),
                        v1Mean = w.Select(w => w.v1).Mean(),
                        v2Mean = w.Select(w => w.v2).Mean(),
                        v3Mean = w.Select(w => w.v3).Mean(),
                        v4Mean = w.Select(w => w.v4).Mean(),
                        v5Mean = w.Select(w => w.v5).Mean(),
                        v6Mean = w.Select(w => w.v6).Mean(),
                        v7Mean = w.Select(w => w.v7).Mean(),
                        v8Mean = w.Select(w => w.v8).Mean(),                        
                        v9Mean = w.Select(w => w.v9).Mean(),                        
                        v10Mean = w.Select(w => w.v10).Mean(),                        
                        v11Mean = w.Select(w => w.v11).Mean(),                        
                        v12Mean = w.Select(w => w.v12).Mean(),                        
                        v13Mean = w.Select(w => w.v13).Mean(),                        
                        v14Mean = w.Select(w => w.v14).Mean(),                        
                        v15Mean = w.Select(w => w.v15).Mean(),                        
                        v16Mean = w.Select(w => w.v16).Mean(),                        
                        v17Mean = w.Select(w => w.v17).Mean(),                                                
                        v18Mean = w.Select(w => w.v18).Mean(),                                                
                        v20Mean = w.Select(w => w.v20).Mean(),                       
                    }).ToCSV("result.csv");

                System.Console.WriteLine($"queried [{CNT}] rows in {stopw.Elapsed}");
            }
        }
    }

}
```

Execution output:

```sh
devel0@main:/opensource/devel0/netcore-csv/example$ dotnet run bin/Release/netcoreapp3.0/test.dll 
written [500000] rows in 00:00:17.8520618
queried [500000] rows in 00:00:14.2929735
```

## How this project was built

```sh
mkdir netcore-csv
cd netcore-csv

dotnet new sln
dotnet new classlib -n netcore-csv
dotnet new console -n examples

cd netcore-csv
dotnet add package netcore-util --version 1.0.6
cd ..

cd example
dotnet add reference ../netcore-csv/netcore-csv.csproj
cd ..

dotnet sln netcore-csv.sln add netcore-csv/netcore-csv.csproj
dotnet sln netcore-csv.sln add example/example.csproj
dotnet build
```
