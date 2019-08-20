using SearchAThing.CSV;
using System.Linq;
using System.IO;

namespace example_01
{

    public enum TestEnum
    {
        unknown,
        enumA,
        enumB
    }

    public class MyData
    {
        public string name { get; set; }
        public double val { get; set; }
        public TestEnum enumVal { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var pathfilename = "output.csv";

            using (var csv = new CsvWriter<MyData>(pathfilename))
            {
                csv.Push(new MyData
                {
                    name = "name1",
                    val = 11.2,
                    enumVal = TestEnum.enumA
                });
                csv.Push(new MyData
                {
                    name = "name2",
                    val = 13.4,
                    enumVal = TestEnum.enumB
                });
            }

            // append more
            using (var csv = new CsvWriter<MyData>(pathfilename, true))
            {
                csv.Push(new MyData
                {
                    name = "name3",
                    val = 44.5
                });
                csv.Push(new MyData
                {
                    name = "name4",
                    val = 33.2
                });
            }

            // read back data            
            System.Console.WriteLine(File.ReadAllText(pathfilename));

            var sum = new CsvReader<MyData>(pathfilename).Sum(w => w.val);            

            var q = new CsvReader<MyData>(pathfilename)
                .GroupBy(w => w.enumVal)
                .Select(w => new
                {
                    enType = w.Key,
                    sum = w.Sum(y => y.val),
                });

            foreach (var x in q)
            {
                System.Console.WriteLine($"enum:{x.enType} sum:{x.sum}");
            }
        }
    }
}
