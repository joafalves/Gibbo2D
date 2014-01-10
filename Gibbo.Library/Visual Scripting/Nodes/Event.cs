#region Copyrights
/*
Gibbo2D - Copyright - 2013 Gibbo2D Team
Founders - Joao Alves <joao.cpp.sca@gmail.com> and Luis Fernandes <luisapidcloud@hotmail.com>

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE. 
*/
#endregion
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
