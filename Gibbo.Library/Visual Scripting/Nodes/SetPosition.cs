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
