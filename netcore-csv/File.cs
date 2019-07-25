using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SearchAThing;

namespace SearchAThing
{

    namespace CSV
    {

        /// <summary>
        /// manage csv file properties such as pathfilename, field separator, decimal separator
        /// and templated object columns
        /// </summary>
        public abstract class CsvFile<T> where T : class
        {
            public readonly string Pathfilename;
            public readonly string FieldSeparator;
            public readonly string DecimalSeparator;
            public readonly bool DecimalSeparatorIsInvariant;

            List<CsvColumn> columns = null;
            public IReadOnlyList<CsvColumn> Columns => columns;

            public CsvFile(string pathfilename, string fieldSeparator = ",", string decimalSeparator = ".")
            {
                Pathfilename = pathfilename;
                FieldSeparator = fieldSeparator;
                DecimalSeparator = decimalSeparator;
                DecimalSeparatorIsInvariant = decimalSeparator == ".";

                columns = new List<CsvColumn>();
                var props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (var (prop, propIdx, isLast) in props.Where(p => p.CanRead).WithIndexIsLast())
                {
                    var header = prop.Name;
                    {
                        var q = prop.GetCustomAttributes(true).OfType<CsvHeaderAttribute>();
                        if (q.Count() > 0)
                        {
                            header = q.First().Header;
                        }
                    }
                    var order = 1000;
                    {
                        var q = prop.GetCustomAttributes(true).OfType<CsvColumnOrderAttribute>();
                        if (q.Count() > 0)
                        {
                            order = q.First().Order;
                        }
                    }
                    columns.Add(new CsvColumn(header, order, prop));
                }
                columns = columns.OrderBy(w => w.Order).ToList();
            }

        }

    }

}
