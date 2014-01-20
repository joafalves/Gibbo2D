#region Copyrights
/*
Gibbo2D License - Version 1.0
Copyright (c) 2013 - Gibbo2D Team
Founders Joao Alves <joao.cpp.sca@gmail.com> & Luis Fernandes <luisapidcloud@hotmail.com>

Permission is granted to use this software and associated documentation files (the "Software") free of charge, 
to any person or company. The code can be used, modified and merged without restrictions, but you cannot sell 
the software itself and parts where this license applies. Still, permission is granted for anyone to sell 
applications made using this software (for example, a game). This software cannot be claimed as your own, 
except for copyright holders. This license notes should also be available on any of the changed or added files.

The software is provided "as is", without warranty of any kind, express or implied, including but not limited to 
the warranties of merchantability, fitness for a particular purpose and non-infrigement. In no event shall the 
authors or copyright holders be liable for any claim, damages or other liability.

*/
#endregion

using System;
using Gibbo.Library;

namespace Samples
{
    /// <summary>
    /// Sample controller.
    /// 
    /// Notice:
    /// If you want the controller to be updated / drawn in the editor, 
    /// implement ExtendedObjectComponent instead.
    /// </summary>
    class SampleController : ObjectComponent
    {
        #region fields

        private string anotherDummyValue = "Dummy Value";

        #endregion

        #region properties

        /// <summary>
        /// Gets or sets the dummy value.
        /// This value can be edited in the editor.
        /// </summary>
        /// <value>The dummy value.</value>
        public int DummyValue { get; set; }

        /// <summary>
        /// Gets or sets another dummy value.
        /// This value can be edited in the editor.
        /// </summary>
        /// <value>Another dummy value.</value>
        public string AnotherDummyValue
        {
            get
            {
                return anotherDummyValue;
            }
            set
            {
                anotherDummyValue = value;
            }
        }

        #endregion

        #region methods

        /// <summary>
        /// Initialize this instance.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();

            // TODO: initialize your component members
            
        }

        /// <summary>
        /// Component Update 
        /// </summary>
        /// <param name="gameTime">Game time.</param>
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Update(gameTime);

            // TODO: update your component logic

        }

        /// <summary>
        /// Component Draw
        /// </summary>
        /// <param name="gameTime">Game time.</param>
        /// <param name="spriteBatch">Sprite batch.</param>
        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);

            // TODO: draw your component members
            
        }

        /// <summary>
        /// Raises the collision enter event.
        /// This event is thrown when another object collides with this game object collision model boundries.
        /// </summary>
        /// <param name="other">The other object</param>
        public override void OnCollisionEnter(GameObject other)
        {
            base.OnCollisionEnter(other);

            // TODO: handle your object collision

        }

        /// <summary>
        /// Raises the mouse click event.
        /// This event is thrown when there is a mouse click inside this game object collision model boundries.
        /// </summary>
        /// <param name="buttonPressed">Button pressed.</param>
        public override void OnMouseClick(MouseEventButton buttonPressed)
        {
            base.OnMouseClick(buttonPressed);

            // TODO: handle mouse clicks

        }

        /// <summary>
        /// Raises the mouse click event.
        /// This event is thrown when there is a mouse down inside this game object collision model boundries.
        /// </summary>
        /// <param name="buttonPressed">Button pressed.</param>
        public override void OnMouseDown(MouseEventButton buttonPressed)
        {
            base.OnMouseDown(buttonPressed);

            // TODO: handle mouse down

        }

        /// <summary>
        /// Raises the mouse click event.
        /// This event is thrown when there is a mouse move inside this game object collision model boundries.
        /// </summary>
        public override void OnMouseMove()
        {
            base.OnMouseMove();

            // TODO: handle mouse move

        }

        #endregion
    }
}