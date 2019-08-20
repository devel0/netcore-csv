using System;
using System.Collections.Generic;
using System.IO;
using SearchAThing.CSV;
using static System.FormattableString;

namespace SearchAThing
{

    namespace CSV
    {

        /// <summary>
        /// write object data to csv as row autogenerating first row header using object properties as name and order.
        /// attributes CsvHeaderAttribute and CsvColumnOrderAttribute can be specified on properties to custommize header and order.
        /// </summary>
        public class CsvWriter<T> : CsvFile<T>, IDisposable where T : class
        {
            StreamWriter sw = null;

            /// <summary>
            /// construct a csv writer to write on given filename with field and decimal separators.            
            /// </summary>
            public CsvWriter(string pathfilename, CsvOptions opts = null) :
                base(pathfilename, opts)
            {
            }

            /// <summary>
            /// construct a csv writer to write on given filename with field and decimal separators.            
            /// data will appended if file exists
            /// </summary>
            public CsvWriter(string pathfilename, bool append, CsvOptions opts = null) :
                base(pathfilename, append, opts)
            {
            }

            /// <summary>
            /// write a csv row getting data from given object public properties
            /// </summary>
            public void Push(T obj)
            {
                // write header row ( if first )
                if (sw == null)
                {
                    var outHdr = true;
                    if (AppendMode)
                    {
                        outHdr = false;
                        if (File.Exists(Pathfilename) && new FileInfo(Pathfilename).Length == 0) outHdr = true;
                    }
                    sw = new StreamWriter(Pathfilename, AppendMode);

                    if (outHdr)
                    {
                        foreach (var (col, idx, isLast) in Columns.WithIndexIsLast())
                        {
                            sw.Write($"\"{col.Header}\"{(isLast ? "" : FieldSeparator)}");
                        }
                        sw.WriteLine();
                    }
                }

                // write data row            
                foreach (var (col, idx, isLast) in Columns.WithIndexIsLast())
                {
                    var val = col.Property.GetValue(obj, null);
                    if (col.IsNumber)
                    {
                        if (DecimalSeparatorIsInvariant)
                            sw.Write(Invariant($"{val}{(isLast ? "" : FieldSeparator)}"));
                        else
                        {
                            var str = Invariant($"{val}").Replace(".", DecimalSeparator);
                            sw.Write($"{str}{(isLast ? "" : FieldSeparator)}");
                        }
                    }
                    else if (col.IsText)
                    {
                        sw.Write($"\"{val}\"{(isLast ? "" : FieldSeparator)}");
                    }
                    else
                    {
                        sw.Write($"{val}{(isLast ? "" : FieldSeparator)}");
                    }
                }
                sw.WriteLine();
            }

            /// <summary>
            /// dispose the writer closing writer stream
            /// </summary>
            public void Dispose()
            {
                if (sw != null)
                {
                    sw.Close();
                    sw.Dispose();
                }
            }
        }

    }

    public static class Extensions
    {

        /// <summary>
        /// generate csv file from this object enumerable using properties name and their order as csv header.
        /// if specified propNameHeaderMapping allor to specify mapping between propertyname and a custom header.
        /// (useful if can't evaluated at compile time using CsvHeaderAttribute.
        /// </summary>
        public static void ToCSV<T>(this IEnumerable<T> coll, string pathfilename, CsvOptions options = null) where T : class
        {
            using var csv = new CsvWriter<T>(pathfilename, options);

            foreach (var x in coll) csv.Push(x);
        }

    }

}