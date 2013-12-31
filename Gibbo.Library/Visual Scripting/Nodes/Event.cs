using Gibbo.Library.VisualScripting.Values;
using System;

namespace Gibbo.Library.VisualScripting.Nodes
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public enum EventTypes { OnSceneStart };

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class Event : VisualScriptNode
    {
        #region fields

        private VisualScriptNodeInterfaceOutput trigger;
        private EventTypes eventType = EventTypes.OnSceneStart;

        #endregion

        #region properties

        /// <summary>
        /// 
        /// </summary>
        public EventTypes EventType
        {
            get { return eventType; }
            set
            {
                eventType = value;
                this.Name = GibboHelper.SplitCamelCase(value.ToString());
            }
        }

        #endregion

        #region constructors

        /// <summary>
        /// 
        /// </summary>
        public Event()
        {
            // Interfaces
            trigger = new VisualScriptNodeInterfaceOutput()
            {
                Name = "Trigger",
                Transmission = new Trigger(),
                Key = 0
            };
        }

        #endregion

        #region methods

        /// <summary>
        /// 
        /// </summary>
        public override void Execute()
        {
            foreach (VisualScriptConnection connection in trigger.Connections)
            {
                connection.OutputInterface.Parent.Execute();
            }
        }

        #endregion
    }
}
