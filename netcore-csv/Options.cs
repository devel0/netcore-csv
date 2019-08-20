using System;
using System.Collections.Generic;
using System.Reflection;

namespace SearchAThing
{

    namespace CSV
    {

        /// <summary>
        /// csv reader/writer options
        /// </summary>
        public class CsvOptions
        {

            /// <summary>
            /// csv field separator
            /// </summary>            
            public string FieldSeparator { get; set; } = ",";

            /// <summary>
            /// csv number decimal separator
            /// </summary>
            public string DecimalSeparator { get; set; } = ".";

            /// <summary>
            /// custom dictionary to map property name to csv header
            /// </summary>            
            public Dictionary<string, string> PropNameHeaderMapping { get; set; } = null;

            /// <summary>
            /// custom function to map property name to csv header
            /// </summary>            
            public Func<string, string> PropNameToHeaderFunc { get; set; } = null;

        }

    }
}