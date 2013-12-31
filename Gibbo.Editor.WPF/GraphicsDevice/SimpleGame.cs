using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gibbo.Editor.WPF
{
    class SimpleGame : GameControl
    {
        #region fields

        SpriteBatch spriteBatch;

        #endregion

        #region properties


        #endregion

        #region methods

        protected override void Initialize()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            base.Initialize();
        }


        protected override void Update(GameTime gameTime)
        {

        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);


        }

        #endregion


    }
}
