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
    public class VisualScriptConnection
    {
        #region fields

        private VisualScriptNodeInterfaceInput _inputInterface;
        private VisualScriptNodeInterfaceOutput _outputInterface;

        #endregion

        #region properties

        /// <summary>
        /// 
        /// </summary>
        public VisualScriptNodeInterfaceOutput OutputInterface
        {
            get { return _outputInterface; }
        }

        /// <summary>
        /// 
        /// </summary>
        public VisualScriptNodeInterfaceInput InputInterface
        {
            get { return _inputInterface; }
        }

        #endregion

        #region methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="outputInterface"></param>
        /// <param name="inputInterface"></param>
        /// <returns></returns>
        public static bool EstablishConnection(VisualScriptNodeInterfaceOutput outputInterface ,VisualScriptNodeInterfaceInput inputInterface)
        {
            if (inputInterface.RequiredType == outputInterface.Transmission.GetType())
            {
               VisualScriptConnection connection = 
                   new VisualScriptConnection() { 
                       _outputInterface = outputInterface,
                       _inputInterface = inputInterface };

               outputInterface.Connections.Add(connection);
               inputInterface.Connections.Add(connection);

               return true;
            }

            return false;
        }

        #endregion
    }
}
