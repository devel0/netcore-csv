using System;

namespace SearchAThing
{

    namespace CSV
    {

        /// <summary>
        /// allow to specify custom header instead of use property name
        /// </summary>
        public class CsvHeaderAttribute : Attribute
        {

            public string Header { get; private set; }

            public CsvHeaderAttribute(string header)
            {
                Header = header;
            }

        }

        /// <summary>
        /// allow to specify column order respect other object properties
        /// </summary>
        public class CsvColumnOrderAttribute : Attribute
        {

            public int Order { get; private set; }

            public CsvColumnOrderAttribute(int order)
            {
                Order = order;
            }

        }

        /// <summary>
        /// ignore specified property from being serialized to/from csv
        /// </summary>
        public class CsvIgnoreAttribute : Attribute
        {
            
            public CsvIgnoreAttribute()
            {                
            }

        }

    }

}
