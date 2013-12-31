using Gibbo.Library;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Gibbo.Editor.WPF
{
    partial class GameViewGameControl : GameControl
    {
        #region fields

        

        #endregion

        #region properties


        #endregion

        #region methods

        protected override void Initialize()
        {
            
            base.Initialize();
        }


        protected override void Update(GameTime gameTime)
        {
            try
            {
                if (SceneManager.ActiveScene != null)
                {
                    SceneManager.Update(gameTime);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ups: " + ex.ToString() + "\nTarget:>" + ex.TargetSite);
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            if (SceneManager.ActiveScene != null)
            {
                GraphicsDevice.Clear(SceneManager.ActiveScene.BackgroundColor);
            }
            else
            {
                GraphicsDevice.Clear(Color.CornflowerBlue);
            }

            try
            {
                if (SceneManager.ActiveScene != null)
                {
                    SceneManager.Draw(gameTime);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ups: " + ex.ToString() + "\nTarget:>" + ex.TargetSite);
            }
        }

        #endregion


    }
}
