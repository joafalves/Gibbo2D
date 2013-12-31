using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gibbo.Framework
{
    public static class GameTime
    {
        public static TimeSpan Elapsed { get; set; }
        public static TimeSpan Total { get; set; }

        public static float DeltaTime { get { return (float)Elapsed.TotalSeconds; } }
    }
}
