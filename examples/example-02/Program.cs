using System;
using System.IO;
using System.Linq;
using SearchAThing;

namespace example_02
{
    class Program
    {
        static void Main(string[] args)
        {
            var pathfilename = "output.csv";

            new[] { 1, 2, 3 }.Select(w => new
            {
                str = $"string with val={w}",
                val = w
            }).ToCSV(pathfilename, new SearchAThing.CSV.CsvOptions()
            {
                PropNameToHeaderFunc = (propName) =>
                {
                    switch (propName)
                    {
                        case "val": return "Value";
                    }
                    return null;
                }
            });

            System.Console.WriteLine(File.ReadAllText(pathfilename));
        }
    }
}
