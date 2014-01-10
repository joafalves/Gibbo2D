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
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Common;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Joints;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;

namespace Gibbo.Library
{
    /// <summary>
    /// Rectangle Body
    /// </summary>
    [Info("Rectangle Body:\nA body with a rectangular shape attached.")]
    public class RectangleBody : PhysicalBody
    {
        #region fields

        private int width = 100;
        private int height = 100;
        private float density = 1;

        #endregion

        #region properties

        /// <summary>
        /// The density of the body
        /// </summary>
#if WINDOWS
        [Category("Physical Body Properties"), DisplayName("Density")]
#endif
        public float Density
        {
            get { return density; }
            set
            {
                density = value;

                if (Transform.GameObject.Body != null)
                    (Transform.GameObject.Body.FixtureList[0].Shape as PolygonShape).Density = value;
            }
        }

        /// <summary>
        /// The Width of the body shape
        /// </summary>
#if WINDOWS
        [Category("Physical Body Properties"), DisplayName("Width")]
#endif
        public int Width
        {
            get { return width; }
            set
            {
                width = value;

                if (Transform.GameObject.Body != null)
                    ResetBody();
            }
        }

        /// <summary>
        /// The Height of the body shape
        /// </summary>
#if WINDOWS
        [Category("Physical Body Properties"), DisplayName("Height")]
#endif
        public int Height
        {
            get { return height; }
            set
            {
                height = value;

                if (Transform.GameObject.Body != null)
                    ResetBody();
            }
        }

        #endregion

        #region methods

        /// <summary>
        /// Initializes the body
        /// </summary>
        public override void Initialize()
        {
            Transform.GameObject.Body = BodyFactory.CreateRectangle(SceneManager.ActiveScene.World, ConvertUnits.ToSimUnits(width * Transform.scale.X), ConvertUnits.ToSimUnits(height * Transform.scale.Y), density);
            Transform.gameObject.physicalBody = this;

            Transform.Position = Transform.position;
            Transform.Rotation = Transform.rotation;

            // Update the body properties:
            UpdateBodyProperties();
        }

        internal override void ResetBody()
        {
            if (Transform.GameObject.Body.FixtureList[0].Shape != null && Transform.GameObject.Body.FixtureList[0].Shape is PolygonShape)
            {
                Vertices newVertices = PolygonTools.CreateRectangle((ConvertUnits.ToSimUnits(width) / 2) * Transform.scale.X, (ConvertUnits.ToSimUnits(height) / 2) * Transform.scale.Y);
                (Transform.GameObject.Body.FixtureList[0].Shape as PolygonShape).Vertices = newVertices;
                (Transform.GameObject.Body.FixtureList[0].Shape as PolygonShape).Density = density;
            }
        }

        #endregion
    }
}
