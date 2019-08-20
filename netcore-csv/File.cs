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
            public string Pathfilename { get; private set; }


            public CsvOptions Options { get; private set; }

            public string FieldSeparator => Options.FieldSeparator;

            public string DecimalSeparator => Options.DecimalSeparator;

            public bool DecimalSeparatorIsInvariant => DecimalSeparator == ".";

            List<CsvColumn> columns = null;
            public IReadOnlyList<CsvColumn> Columns => columns;

            /// <summary>
            /// if file exists with size great than 0 then header will not placed
            /// else file will overwritten with new starting header,                        
            /// </summary>
            public bool AppendMode { get; private set; }

            /// <summary>
            /// csv reader/writer base class.            
            /// </summary>            
            public CsvFile(string pathfilename, CsvOptions options)
            {
                Pathfilename = pathfilename;
                Options = (options == null) ? new CsvOptions() : options;
                Init();
            }

            /// <summary>
            /// csv reader/writer base class.                        
            /// </summary>            
            /// <param name="append"> if file exists with size great than 0 then header will not placed
            /// else file will overwritten with new starting header,</param>            
            public CsvFile(string pathfilename, bool append, CsvOptions options)
            {
                Pathfilename = pathfilename;
                Options = (options == null) ? new CsvOptions() : options;
                AppendMode = append;
                Init();
            }

            void Init()
            {
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
                    {
                        string str = null;
                        if (Options.PropNameHeaderMapping != null && Options.PropNameHeaderMapping.TryGetValue(prop.Name, out str))
                        {
                            header = str;
                        }
                        if (Options.PropNameToHeaderFunc != null)
                        {
                            var q = Options.PropNameToHeaderFunc(prop.Name);
                            if (!string.IsNullOrEmpty(q))
                                header = q;
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
