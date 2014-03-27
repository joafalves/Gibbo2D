using Microsoft.Xna.Framework;
using System;
using System.Runtime.Serialization;

namespace Gibbo.Library
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITween
    {
        #region methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameTime"></param>
        void Update(GameTime gameTime);

        #endregion
    }
}
