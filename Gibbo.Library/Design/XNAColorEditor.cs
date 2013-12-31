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