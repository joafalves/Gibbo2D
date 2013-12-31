using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarseerPhysics.Dynamics;

namespace Gibbo.Library
{
    /// <summary>
    /// 
    /// </summary>
    public static class Physics
    {
        /// <summary>
        /// The active physics world
        /// </summary>
        public static World World
        {
            get
            {
                return SceneManager.ActiveScene.World;
            }
        }
    }
}
