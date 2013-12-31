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
    public abstract class VisualScriptNodeInterface
    {
        #region fields

        private VisualScriptNode parent = null;
        private VisualScriptConnectionsCollection connections = new VisualScriptConnectionsCollection();
        private string name = string.Empty;

        internal int Key = 0;

        #endregion

        #region properties

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
        public VisualScriptNode Parent
        {
            get { return parent; }
            set { parent = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public VisualScriptConnectionsCollection Connections
        {
            get { return connections; }
        }

        #endregion

        #region constructors

        /// <summary>
        /// 
        /// </summary>
        public void RemoveConnections()
        {
            foreach (VisualScriptConnection connection in connections)
            {
                connection.InputInterface.connections.Remove(connection);
                connection.OutputInterface.connections.Remove(connection);
            }
        }

        #endregion

        #region methods



        #endregion
    }
}
