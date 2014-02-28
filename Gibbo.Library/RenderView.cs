using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Gibbo.Library
{
#if WINDOWS
    [Serializable]
#endif
    [DataContract]
    public class RenderView
    {
        #region fields

        [DataMember]
        private Camera camera = new Camera();

        [DataMember]
        private Viewport viewport = new Viewport();

        #endregion

        #region properties

        /// <summary>
        /// 
        /// </summary>
        public Camera Camera
        {
            get { return camera; }
            set { camera = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Viewport Viewport
        {
            get { return viewport; }
            set { viewport = value; }
        }

        #endregion
    }
}
