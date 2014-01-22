using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Xceed.Wpf.Toolkit
{
    public class XNAColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            // Do the conversion from bool to visibility
            if (value is Microsoft.Xna.Framework.Color)
            {
                Microsoft.Xna.Framework.Color c = (Microsoft.Xna.Framework.Color)value;
                return new Color() { R = c.R, G = c.G, B = c.B, A = c.A };
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            // Do the conversion from visibility to bool
            if (value is Color)
            {
                Color c = (Color)value;
                return new Microsoft.Xna.Framework.Color(c.R, c.G, c.B, c.A);
            }

            return null;
        }
    }
}
