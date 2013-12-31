using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.Serialization;

namespace Gibbo.Library
{
#if WINDOWS
    [Serializable]
#endif
    [DataContract(IsReference = true)]
    public class SystemObject
    {

    }
}
