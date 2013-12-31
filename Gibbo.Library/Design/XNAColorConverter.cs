using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Globalization;
using Microsoft.Xna.Framework;

namespace Gibbo.Library
{
#if WINDOWS
    class XNAColorConverter : TypeConverter
    {
        // This is used, for example, by DefaultValueAttribute to convert from string to MyColor.
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value.GetType() == typeof(System.Drawing.Color))
            {
                System.Drawing.Color c = (System.Drawing.Color)value;
                return Color.FromNonPremultiplied(c.R, c.G, c.B, c.A);
            }
            return base.ConvertFrom(context, culture, value);
        }
        // This is used, for example, by the PropertyGrid to convert MyColor to a string.
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destType)
        {
            if ((destType == typeof(string)) && (value is Color))
            {
                Color color = (Color)value;
                return color.ToString();
            }
            return base.ConvertTo(context, culture, value, destType);
        }
    }
#endif
}
