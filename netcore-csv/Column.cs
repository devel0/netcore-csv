using System;
using System.Reflection;

namespace SearchAThing
{

    namespace CSV
    {

        public enum CsvColumnNumberType { numberInt, numberDouble, numberFloat, numberDecimal };
        public enum CsvColumnTextType { textString, textChar };

        public class CsvColumn
        {
            public readonly string Header;
            public readonly int Order;
            public readonly PropertyInfo Property;
            public readonly bool IsNumber;
            public readonly bool IsText;

            public readonly CsvColumnNumberType NumberType;
            public readonly CsvColumnTextType TextType;

            static Type intType = typeof(int);
            static Type doubleType = typeof(double);
            static Type floatType = typeof(float);
            static Type decimalType = typeof(decimal);
            static Type stringType = typeof(string);
            static Type charType = typeof(char);

            public CsvColumn(string header, int order, PropertyInfo property)
            {
                Header = header;
                Order = order;
                Property = property;

                if (property.PropertyType == intType)
                {
                    NumberType = CsvColumnNumberType.numberInt;
                    IsNumber = true;
                }
                else if (property.PropertyType == doubleType)
                {
                    NumberType = CsvColumnNumberType.numberDouble;
                    IsNumber = true;
                }
                else if (property.PropertyType == floatType)
                {
                    NumberType = CsvColumnNumberType.numberFloat;
                    IsNumber = true;
                }
                else if (property.PropertyType == decimalType)
                {
                    NumberType = CsvColumnNumberType.numberDecimal;
                    IsNumber = true;
                }
                else if (property.PropertyType == stringType)
                {
                    TextType = CsvColumnTextType.textString;
                    IsText = true;
                }
                else if (property.PropertyType == charType)
                {
                    TextType = CsvColumnTextType.textChar;
                    IsText = true;
                }

                // TOOO: other types ( datetime, ... )
            }
        }

    }

}