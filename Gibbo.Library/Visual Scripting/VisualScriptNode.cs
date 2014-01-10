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
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gibbo.Library
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public abstract class VisualScriptNode
    {
        #region fields

        private string name;  
        private VisualScriptInterfacesCollection inputInterfaces;
        private VisualScriptInterfacesCollection outputInterfaces;
        private Vector2 editorPosition;

        #endregion

        #region properties

        /// <summary>
        /// 
        /// </summary>
        public Vector2 EditorPosition
        {
            get { return editorPosition; }
            set { editorPosition = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public VisualScriptInterfacesCollection InputInterfaces
        {
            get { return inputInterfaces; }
            set { inputInterfaces = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public VisualScriptInterfacesCollection OutputInterfaces
        {
            get { return outputInterfaces; }
            set { outputInterfaces = value; }
        }

        #endregion

        #region constructors

        /// <summary>
        /// 
        /// </summary>
        public VisualScriptNode()
        {
            inputInterfaces = new VisualScriptInterfacesCollection(this);
            outputInterfaces = new VisualScriptInterfacesCollection(this);
        }

        #endregion

        #region methods

        /// <summary>
        /// 
        /// </summary>
        public abstract void Execute();

        #endregion
    }
}
