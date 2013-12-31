using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#if WINRT
using System.Runtime.Serialization;
#endif

namespace Gibbo.Library
{
    /// <summary>
    /// Class used if you want object components to update and draw in the editor.
    /// </summary>
#if WINDOWS
    [Serializable]
#elif WINRT
    [DataContract]
#endif
    public class ExtendedObjectComponent : ObjectComponent
    {

    }
}
