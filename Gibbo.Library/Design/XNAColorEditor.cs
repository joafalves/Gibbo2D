#region Copyrights
/*
Gibbo2D - Copyright - 2013 Gibbo2D Team
Founders - Joao Alves <joao.cpp.sca@gmail.com> and Luis Fernandes <luisapidcloud@hotmail.com>

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE. 
*/
#endregion
#if WINDOWS

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using System.ComponentModel;
using Microsoft.Xna.Framework;
using System.Windows.Forms;

namespace Gibbo.Library
{
    public class XNAColorEditor : UITypeEditor
    {
        private IWindowsFormsEditorService service;

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            // This tells it to show the [...] button which is clickable firing off EditValue below.
            return UITypeEditorEditStyle.Modal;
        }
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if (provider != null)
                service = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

            if (service != null)
            {
                // This is the code you want to run when the [...] is clicked and after it has been verified.

                // Get our currently selected color.
                Color c = (Color)value;

                // Create a new instance of the ColorDialog.
                ColorDialog selectionControl = new ColorDialog();

                // Set the selected color in the dialog.
                selectionControl.Color = System.Drawing.Color.FromArgb(c.A, c.R, c.G, c.B);

                // Show the dialog.
                selectionControl.ShowDialog();

                // Return the newly selected color.
                value = Color.FromNonPremultiplied(selectionControl.Color.R,
                    selectionControl.Color.G,
                    selectionControl.Color.B,
                    selectionControl.Color.A);
            }

            return value;
        }
    }
}

#endif