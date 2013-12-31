using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
    public class VisualScript
    {
        #region fields

        private List<VisualScriptNode> nodes = new List<VisualScriptNode>();
        private string name;

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
        public List<VisualScriptNode> Nodes
        {
            get { return nodes; }
            set { nodes = value; }
        }

        #endregion

        #region constructors

        /// <summary>
        /// 
        /// </summary>
        public VisualScript()
        {
            
        }

        #endregion

        #region methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.name;
        }

        #endregion
    }
}
