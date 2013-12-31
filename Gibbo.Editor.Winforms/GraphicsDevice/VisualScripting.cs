﻿using Gibbo.Library;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Gibbo.Library.UI;
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace Gibbo.Editor
{
    class VisualScripting : GameControl
    {
        #region fields

        SpriteBatch spriteBatch;
        Color clearColor;

        Texture2D background;

        bool panStarted;
        Vector2 panLastPos;
        Vector2 panStartPos;

        VisualScript activeVisualScript;

        Dictionary<VisualScriptNode, Window> visualNetwork = new Dictionary<VisualScriptNode, Window>();

        #endregion

        #region properties

        public VisualScript ActiveVisualScript
        {
            get { return activeVisualScript; }
            set { activeVisualScript = value; UnloadNetwork(); }
        }

        public bool MiddleMouseKeyDown { get; set; }
        public bool LeftMouseKeyDown { get; set; }

        #endregion

        #region methods

        protected override void Initialize()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            clearColor = Color.FromNonPremultiplied(228, 230, 232, 255);

            background = TextureLoader.FromFile(AppDomain.CurrentDomain.BaseDirectory + @"\Gibbo.Content\bgvs.png");

            base.Initialize();
        }

        private void UnloadNetwork()
        {
            //foreach (VisualScriptNode node in activeVisualScript.Nodes)
            //{
            //    visualNetwork[node] = new Window()
            //    {
            //        Name = node.Name,
            //        Position = node.EditorPosition,
            //    };
            //}
        }

        protected override void Update(GameTime gameTime)
        {
            Input(gameTime);

            UpdateCurrentCursor();

            GameInput.Update();

        }

        private void UpdateCurrentCursor()
        {
            if (panStarted)
            {
                Cursor = Cursors.Hand;
            }
            else
            {
                //switch (editorMode)
                //{

                //    default:
                Cursor = Cursors.Default;
                //      break;
                //}
            }
        }

        private void Input(GameTime gameTime)
        {
            float delta = (float)gameTime.ElapsedGameTime.Milliseconds;

            // mouse span
            if (MiddleMouseKeyDown)
            {
                if (!panStarted)
                {
                    panStarted = true;
                    panStartPos = gameMouseWorldPosition;
                }
                else
                {
                    if (panLastPos != gameMouseWorldPosition)
                    {
                        Vector2 alpha = gameMouseWorldPosition - panStartPos;

                        Camera.Position = new Vector2()
                        {
                            X = (int)(Camera.Position.X - alpha.X),
                            Y = (int)(Camera.Position.Y - alpha.Y)
                        };

                        panLastPos = gameMouseWorldPosition;
                    }
                }
            }
            else
            {
                panStartPos = Vector2.Zero;
                panStarted = false;
            }

            if ((GameInput.IsKeyDown(Keys.W) || GameInput.IsKeyDown(Keys.Up)) && GameInput.IsKeyUp(Keys.LeftControl)) Camera.Position += (new Vector2(0, -delta));
            if ((GameInput.IsKeyDown(Keys.S) || GameInput.IsKeyDown(Keys.Down)) && GameInput.IsKeyUp(Keys.LeftControl)) Camera.Position += (new Vector2(0, +delta));
            if ((GameInput.IsKeyDown(Keys.A) || GameInput.IsKeyDown(Keys.Left)) && GameInput.IsKeyUp(Keys.LeftControl)) Camera.Position += (new Vector2(-delta, 0));
            if ((GameInput.IsKeyDown(Keys.D) || GameInput.IsKeyDown(Keys.Right)) && GameInput.IsKeyUp(Keys.LeftControl)) Camera.Position += (new Vector2(+delta, 0));
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(clearColor);

            DrawBackground(gameTime);
            DrawVisualScript(gameTime);
        }

        private void DrawBackground(GameTime gameTime)
        {
            // Draw Background
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.LinearWrap, null, null, null, Camera.TransformMatrix);

            spriteBatch.Draw(background, new Vector2((int)Camera.Position.X - GraphicsDevice.Viewport.Width, (int)Camera.Position.Y - GraphicsDevice.Viewport.Height), new Rectangle((int)Camera.Position.X,
                    (int)Camera.Position.Y, GraphicsDevice.Viewport.Width * 2, GraphicsDevice.Viewport.Height * 2), Color.White, 0, Vector2.Zero, 1,
                    SpriteEffects.None, 1);

            spriteBatch.End();
        }

        private void DrawVisualScript(GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.LinearWrap, null, null, null, Camera.TransformMatrix);

            foreach (Window window in visualNetwork.Values)
            {
                window.Draw(gameTime, spriteBatch);
            }

            spriteBatch.End();
        }

        #endregion

    }
}
