using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace SearchAThing
{

    namespace CSV
    {

        /// <summary>
        /// enumerates csv into objects
        /// </summary>
        class CsvReaderEnumerator<T> : CsvFile<T>, IEnumerator<T> where T : class, new()
        {
            StreamReader sr = null;
            T current = null;

            /// <summary>
            /// creates an object enumerator from csv pathfilename with given field and decimal separator
            /// </summary>
            public CsvReaderEnumerator(string pathfilename, string fieldSeparator = ",", string decimalSeparator = ".") :
                base(pathfilename, fieldSeparator, decimalSeparator)
            {
            }

            public T Current => current;

            object IEnumerator.Current => this.Current;

            /// <summary>
            /// verify if another row is available
            /// </summary>
            /// <returns>true if another row are read, false if EOF reached</returns>
            public bool MoveNext()
            {
                // header row ( if first )
                if (sr == null)
                {
                    sr = new StreamReader(Pathfilename);
                    if (sr.EndOfStream)
                    {
                        current = null;
                        return false;
                    }

                    var line = sr.ReadLine();
                    var ss = line.Split(FieldSeparator);

                    foreach (var (col, idx, isLast) in Columns.WithIndexIsLast())
                    {
                        // TODO: manage string escape ( fieldSeparator can included into double quotes string )
                        if (ss[idx] != $"\"{col.Header}\"")
                            throw new InvalidDataException($"expecting \"{col.Header}\" instead of {ss[idx]}");
                    }
                }

                // read data row
                {
                    if (sr.EndOfStream)
                    {
                        current = null;
                        return false;
                    }

                    var line = sr.ReadLine();
                    var ss = line.Split(FieldSeparator);

                    var obj = new T();

                    foreach (var (col, idx, isLast) in Columns.WithIndexIsLast())
                    {
                        object prop = null;

                        if (col.IsNumber)
                        {
                            switch (col.NumberType)
                            {
                                case CsvColumnNumberType.numberInt:
                                    {
                                        prop = int.Parse(ss[idx]);
                                    }
                                    break;
                                case CsvColumnNumberType.numberDouble:
                                    {
                                        if (DecimalSeparatorIsInvariant)
                                            prop = double.Parse(ss[idx], CultureInfo.InvariantCulture);
                                        else
                                            prop = double.Parse(ss[idx].Replace(DecimalSeparator, "."), CultureInfo.InvariantCulture);
                                    }
                                    break;
                                case CsvColumnNumberType.numberFloat:
                                    {
                                        if (DecimalSeparatorIsInvariant)
                                            prop = float.Parse(ss[idx], CultureInfo.InvariantCulture);
                                        else
                                            prop = float.Parse(ss[idx].Replace(DecimalSeparator, "."), CultureInfo.InvariantCulture);
                                    }
                                    break;
                                case CsvColumnNumberType.numberDecimal:
                                    {
                                        if (DecimalSeparatorIsInvariant)
                                            prop = decimal.Parse(ss[idx], CultureInfo.InvariantCulture);
                                        else
                                            prop = decimal.Parse(ss[idx].Replace(DecimalSeparator, "."), CultureInfo.InvariantCulture);
                                    }
                                    break;
                            }
                        }
                        else if (col.IsText)
                        {
                            prop = ss[idx].StripBegin('"').StripEnd('"');
                        }
                        else
                        {
                            prop = ss[idx];
                        }

                        col.Property.SetValue(obj, prop);
                    }

                    current = obj;
                }

                return true;
            }

            /// <summary>
            /// restart from first row
            /// </summary>
            public void Reset()
            {
                sr.BaseStream.Seek(0, SeekOrigin.Begin);
                current = default(T);
            }

            /// <summary>
            /// dispose reader closing stream reader
            /// </summary>
            public void Dispose()
            {
                if (sr != null)
                {
                    sr.Close();
                    sr.Dispose();
                }
            }
        }

        /// <summary>
        /// read data from csv creating a object enumerable.
        /// an object template with a property foreach csv column is required with same order as they appears in csv.
        /// attributes CsvHeaderAttribute and CsvColumnOrderAttribute can be specified on properties to custommize header and order.
        /// </summary>
        public class CsvReader<T> : IEnumerable<T>, IDisposable where T : class, new()
        {
            CsvReaderEnumerator<T> enumerator = null;

            /// <summary>
            /// read csv from given pathfilename with field and decimal separator using templated object properties as descriptor for csv column headers to expect
            /// </summary>
            public CsvReader(string pathfilename, string fieldSeparator = ",", string decimalSeparator = ".")
            {
                enumerator = new CsvReaderEnumerator<T>(pathfilename, fieldSeparator, decimalSeparator);
            }

            public IEnumerator<T> GetEnumerator() => enumerator;

            IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

            public void Dispose()
            {
                enumerator.Dispose();
            }
        }

    }

}