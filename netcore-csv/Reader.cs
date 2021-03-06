﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

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
            public CsvReaderEnumerator(string pathfilename, CsvOptions options = null) :
                base(pathfilename, options)
            {
            }

            public T Current => current;

            object IEnumerator.Current => this.Current;

            bool resetRequest = true;

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
                }

                if (resetRequest)
                {
                    var line = sr.ReadLine();
                    var ss = line.Split(FieldSeparator);

                    foreach (var (col, idx, isLast) in Columns.WithIndexIsLast())
                    {
                        // TODO: manage string escape ( fieldSeparator can included into double quotes string )
                        if (ss[idx] != $"\"{col.Header}\"" && ss[idx] != col.Header)
                            throw new InvalidDataException($"expecting \"{col.Header}\" instead of {ss[idx]}");
                    }

                    resetRequest = false;
                }

                // read data row
                {
                    if (sr.EndOfStream)
                    {
                        current = null;
                        return false;
                    }

                    var line = sr.ReadLine();
                    var ss = new List<string>();
                    var inStr = false;
                    var token = new StringBuilder();
                    for (int i = 0; i < line.Length; ++i)
                    {
                        if (!inStr && line[i] != FieldSeparator)
                        {
                            if (line[i] == StringDelimiter)
                                inStr = true;
                            else
                                token.Append(line[i]);
                        }
                        else if (inStr)
                        {
                            if (line[i] != StringDelimiter)
                            {
                                token.Append(line[i]);
                            }
                            else if (i + 1 < line.Length && line[i + 1] == StringDelimiter)
                            {
                                token.Append(line[i]);
                                ++i;
                            }
                            else
                                inStr = false;
                        }
                        else if (line[i] == FieldSeparator)
                        {
                            ss.Add(token.ToString());
                            token = new StringBuilder();
                        }
                    }
                    ss.Add(token.ToString());
                    //var ss = line.Split(FieldSeparator);

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
                                            prop = double.Parse(ss[idx].Replace(DecimalSeparator, '.'), CultureInfo.InvariantCulture);
                                    }
                                    break;
                                case CsvColumnNumberType.numberFloat:
                                    {
                                        if (DecimalSeparatorIsInvariant)
                                            prop = float.Parse(ss[idx], CultureInfo.InvariantCulture);
                                        else
                                            prop = float.Parse(ss[idx].Replace(DecimalSeparator, '.'), CultureInfo.InvariantCulture);
                                    }
                                    break;
                                case CsvColumnNumberType.numberDecimal:
                                    {
                                        if (DecimalSeparatorIsInvariant)
                                            prop = decimal.Parse(ss[idx], CultureInfo.InvariantCulture);
                                        else
                                            prop = decimal.Parse(ss[idx].Replace(DecimalSeparator, '.'), CultureInfo.InvariantCulture);
                                    }
                                    break;
                            }
                        }
                        //else if (col.IsText || col.Property.PropertyType.IsEnum)
                        //{
                        //    prop = ss[idx].StripBegin('"').StripEnd('"');
                        //}
                        else
                        {
                            prop = ss[idx];
                        }

                        if (col.Property.PropertyType.IsEnum)
                            col.Property.SetValue(obj, Enum.Parse(col.Property.PropertyType, (string)prop));
                        else
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
                resetRequest = true;
                sr.BaseStream.Seek(0, SeekOrigin.Begin);
                current = default(T);
            }

            /// <summary>
            /// dispose reader closing stream reader
            /// </summary>
            internal void StreamDispose()
            {
                if (sr != null)
                {
                    sr.Close();
                    sr.Dispose();
                }
            }

            public void Dispose()
            {
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
            /// underlying CsvFile
            /// </summary>
            public CsvFile<T> File => enumerator as CsvFile<T>;

            /// <summary>
            /// read csv from given pathfilename with field and decimal separator using templated object properties as descriptor for csv column headers to expect
            /// </summary>
            public CsvReader(string pathfilename, CsvOptions options = null)
            {
                enumerator = new CsvReaderEnumerator<T>(pathfilename, options);
            }

            /// <summary>
            /// reset from first row
            /// </summary>
            public void Reset()
            {
                enumerator.Reset();
            }

            public IEnumerator<T> GetEnumerator() => enumerator;

            IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

            /// <summary>
            /// dispose stream closing file
            /// </summary>
            public void Dispose()
            {
                enumerator.StreamDispose();
            }
        }

    }

}