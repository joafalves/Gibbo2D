//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.ComponentModel;

//namespace Microsoft.Xna.Framework.Design
//{
//    [Serializable]
//    public class ColorConverter : ExpandableObjectConverter
//    {
//        // Return true if we need to convert from a string.
//        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
//        {
//            if (sourceType == typeof(string)) return true;
//            //else if (sourceType == typeof(System.Windows.Media.Color)) return true;
//            return base.CanConvertFrom(context, sourceType);
//        }

//        // Return true if we need to convert into a string.
//        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
//        {
//            if (destinationType == typeof(String)) return true;
//            //else if (destinationType == typeof(System.Windows.Media.Color)) return true;
//            return base.CanConvertTo(context, destinationType);
//        }

//        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
//        {
//            if (value.GetType() == typeof(string))
//            {
//                // Split the string separated by commas.
//                string txt = (string)(value);
              
//                string[] fields = txt.Split(new char[] { ';' });

//                try
//                {
//                    return new Color()
//                    {
//                        R = Convert.ToByte(fields[0]),
//                        G = Convert.ToByte(fields[1]),
//                        B = Convert.ToByte(fields[2]),
//                        A = Convert.ToByte(fields[3])
//                    };
//                }
//                catch
//                {
//                    throw new InvalidCastException(
//                        "Cannot convert the string '" +
//                        value.ToString() + "' into a Color");
//                }
//            }
//            else if (value.GetType() == typeof(System.Windows.Media.Color))
//            {
//                try
//                {
//                    System.Windows.Media.Color color = (System.Windows.Media.Color)value;

//                    return new Color()
//                    {
//                        R = Convert.ToByte(color.R),
//                        G = Convert.ToByte(color.G),
//                        B = Convert.ToByte(color.B),
//                        A = Convert.ToByte(color.A)
//                    };
//                }
//                catch
//                {
//                    throw new InvalidCastException(
//                        "Cannot convert the string '" +
//                        value.ToString() + "' into a Color");
//                }
//            }
//            else
//            {
//                return base.ConvertFrom(context, culture, value);
//            }
//        }

//        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
//        {
//            if (destinationType == typeof(System.Windows.Media.Color))
//            {
//                Color color = (Color)value;
//                return System.Windows.Media.Color.FromArgb(color.A, color.R, color.G, color.B);
//            }
//            else if (destinationType == typeof(string)) return value.ToString();
            
//            return base.ConvertTo(context, culture, value, destinationType);
//        }

//        // Return true to indicate that the object supports properties.
//        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
//        {
//            return true;
//        }

//        // Return a property description collection.
//        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
//        {
//            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(value, attributes);

//            string[] sortOrder = new string[4];

//            sortOrder[0] = "R";
//            sortOrder[1] = "G";
//            sortOrder[2] = "B";
//            sortOrder[3] = "A";

//            // Return a sorted list of properties
//            return properties.Sort(sortOrder);
//        }    
//    }
//}
