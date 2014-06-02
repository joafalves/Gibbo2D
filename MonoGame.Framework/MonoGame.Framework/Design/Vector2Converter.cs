using System;
using System.ComponentModel;
using System.Globalization;

namespace Microsoft.Xna.Framework.Design
{
    [Serializable]
    public class Vector2Converter : ExpandableObjectConverter
    {
        // Return true if we need to convert from a string.
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string)) return true;
            return base.CanConvertFrom(context, sourceType);
        }

        // Return true if we need to convert into a string.
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(String)) return true;
            return base.CanConvertTo(context, destinationType);
        }

        // Convert from a string.
        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            if (value.GetType() == typeof(string))
            {
                // Split the string separated by commas.
                string txt = (string)(value);

                string[] fields = txt.Split(new char[] { ';' });

                try
                {
                    return new Vector2() { X = float.Parse(fields[0]), Y = float.Parse(fields[1]) };
                }
                catch
                {
                   Console.WriteLine(
                        "Cannot convert the string '" +
                        value.ToString() + "' into a Vector2, values: " + fields[0] + "::" + fields[1]);

                   return Vector2.Zero;
                }
            }
            else
            {
                return base.ConvertFrom(context, culture, value);
            }
        }

        // Convert the StreetAddress to a string.
        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {

            if (destinationType == typeof(string)) return value.ToString();
            return base.ConvertTo(context, culture, value, destinationType);
        }

        // Return true to indicate that the object supports properties.
        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return false;
        }

        // Return a property description collection.
        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            return TypeDescriptor.GetProperties(value);

            //PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(value, attributes);

            //string[] sortOrder = new string[2];

            //sortOrder[0] = "X";
            //sortOrder[1] = "Y";

            //// Return a sorted list of properties
            //return properties.Sort(sortOrder);
        }
    }
}
