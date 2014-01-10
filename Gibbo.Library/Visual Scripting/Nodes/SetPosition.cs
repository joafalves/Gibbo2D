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
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gibbo.Library.VisualScripting.Nodes
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class SetPosition : VisualScriptNode
    {
        #region fields

        private bool increment = false;  
        private Vector2 position = Vector2.Zero;

        private VisualScriptNodeInterfaceInput activator;
        private VisualScriptNodeInterfaceInput gameObjects;

        #endregion

        #region properties

        /// <summary>
        /// 
        /// </summary>
        public bool Increment
        {
            get { return increment; }
            set { increment = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        #endregion

        #region constructors

        /// <summary>
        /// 
        /// </summary>
        public SetPosition()
        {
            this.Name = "Set Position";

            // Interfaces
            activator = new VisualScriptNodeInterfaceInput()
            {
                Name = "Activator",
                RequiredType = typeof(Trigger),
                Key = 0
            };

            gameObjects = new VisualScriptNodeInterfaceInput()
            {
                Name = "Game Object(s)",
                RequiredType = typeof(GameObject),
                Key = 1
            };

            this.InputInterfaces.Add(activator);
            this.InputInterfaces.Add(gameObjects);
        }

        #endregion

        #region methods

        /// <summary>
        /// 
        /// </summary>
        public override void Execute()
        {
            if (gameObjects.Connections.Count == 0) return;

            foreach (VisualScriptConnection connection in gameObjects.Connections)
            {
                if (increment)
                {
                    GameObject go = (connection.OutputInterface.Transmission as GameObject);
                    go.Transform.Position =
                        new Vector2()
                        {
                            X = go.Transform.Position.X + position.X,
                            Y = go.Transform.Position.Y + position.Y
                        };
                }
                else
                {
                    (connection.OutputInterface.Transmission as GameObject).Transform.Position =
                        new Vector2()
                        {
                            X = position.X,
                            Y = position.Y
                        };
                }
            }
        }

        #endregion
    }
}
