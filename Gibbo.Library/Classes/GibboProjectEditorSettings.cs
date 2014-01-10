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
using System;
using System.ComponentModel;
using Microsoft.Xna.Framework;
using System.Runtime.Serialization;

namespace Gibbo.Library
{
    /// <summary>
    /// The editor project settings 
    /// </summary>
#if WINDOWS
    [Serializable, TypeConverter(typeof(ExpandableObjectConverter))]
#endif
    [DataContract]
    public class GibboProjectEditorSettings
    {
        #region fields

        [DataMember]
        private string lastOpenScenePath = string.Empty;
        [DataMember]
        private bool showCollisions;
        [DataMember]
        private bool showGrid;
        [DataMember]
        private bool snapToGrid;
        [DataMember]
        private int gridSpacing = 32;
        [DataMember]
        private int gridThickness = 1;
        [DataMember]
        private int gridNumberOfLines = 100;
        [DataMember]
        private Color gridColor = new Color(0, 0, 0, 80);

        #endregion

        #region properties

        /// <summary>
        /// The last open scene path
        /// </summary>
#if WINDOWS
        [Browsable(false)]
#endif
        public string LastOpenScenePath
        {
            get { return lastOpenScenePath; }
            set { lastOpenScenePath = value; }
        }

        /// <summary>
        /// Determines if collisions models are to be drawn
        /// </summary>
#if WINDOWS
        [DisplayName("Show Collision"), Description("Determines if the collision models will be drawn")]
#endif
        public bool ShowCollisions
        {
            get { return showCollisions; }
            set { showCollisions = value; }
        }

        /// <summary>
        /// Determines if the grid is to be shown
        /// </summary>
#if WINDOWS
        [DisplayName("Grid Visible"), Description("Determines if the grid is visible")]
#endif
        public bool ShowGrid
        {
            get { return showGrid; }
            set { showGrid = value; }
        }

        /// <summary>
        /// Determines if the object transformations will snap to the grid.
        /// </summary>
#if WINDOWS
        [DisplayName("Snap To Grid"), Description("Determines if the move tool should snap to the nearest grid point")]
#endif
        public bool SnapToGrid
        {
            get { return snapToGrid; }
            set { snapToGrid = value; }
        }

        /// <summary>
        /// The unit size of the grid. The value is measured in pixels.
        /// </summary>
#if WINDOWS
        [DisplayName("Grid Unit Size"), Description("The grid unit size, measured in pixels")]
#endif
        public int GridSpacing
        {
            get { return gridSpacing; }
            set { gridSpacing = value; }
        }

        /// <summary>
        /// The color of the grid.
        /// </summary>
        //[EditorAttribute(typeof(XNAColorEditor), typeof(System.Drawing.Design.UITypeEditor))]
#if WINDOWS
        [DisplayName("Grid Color"), Description("The color of the grid")]
#endif
        public Color GridColor
        {
            get { return gridColor; }
            set { gridColor = value; }
        }

        /// <summary>
        /// The thickness of the grid.
        /// </summary>
#if WINDOWS
        [DisplayName("Grid Thickness"), Description("The thickness of the grid")]
#endif
        public int GridThickness
        {
            get { return gridThickness; }
            set
            {
                if (value > 4) gridThickness = 4;
                else gridThickness = value;
            }
        }

        /// <summary>
        /// The number of lines to be drawn.
        /// </summary>
#if WINDOWS
        [DisplayName("Grid Lines"), Description("The maximum grid lines to be displayed")]
#endif
        public int GridNumberOfLines
        {
            get { return gridNumberOfLines; }
            set
            {
                if (value > 500) gridNumberOfLines = 500;
                else gridNumberOfLines = value;
            }
        }

        #endregion

        #region methods

        /// <summary>
        /// To String
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Empty;
        }

        #endregion
    }
}
