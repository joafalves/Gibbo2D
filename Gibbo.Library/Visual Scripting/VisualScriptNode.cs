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
