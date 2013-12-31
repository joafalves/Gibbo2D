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
    public class VisualScriptNodeInterfaceOutput : VisualScriptNodeInterface
    {
        #region fields

        private object transmission;

        #endregion

        #region methods

        /// <summary>
        /// 
        /// </summary>
        public object Transmission
        {
            get { return transmission; }
            set { transmission = value; }
        }

        #endregion
    }
}
