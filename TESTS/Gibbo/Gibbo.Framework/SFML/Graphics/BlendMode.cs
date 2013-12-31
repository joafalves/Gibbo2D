using System;
using System.Runtime.InteropServices;

namespace Gibbo.Framework
{
    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Enumerate the blending modes available for drawable objects
    /// </summary>
    ////////////////////////////////////////////////////////////
    public enum BlendMode
    {
        /// <summary>Pixel = Src * a + Dest * (1 - a)</summary>
        Alpha,

        /// <summary>Pixel = Src + Dest</summary>
        Add,

        /// <summary>Pixel = Src * Dest</summary>
        Multiply,

        /// <summary>No blending</summary>
        None
    }
}
