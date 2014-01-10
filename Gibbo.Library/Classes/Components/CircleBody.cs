using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Factories;

namespace Gibbo.Library
{
    /// <summary>
    /// Circle Body
    /// </summary>
    [Info("Circle Body:\nA body with a circular shape attached.")]
    public class CircleBody : PhysicalBody
    {
        #region fields

        private float radius = 100;
        private float density = 1;

        #endregion

        #region properties

        /// <summary>
        /// The density of the body
        /// </summary>
#if WINDOWS
        [Category("Physical Body Properties"), DisplayName("Radius")]
#endif
        public float Radius
        {
            get { return radius; }
            set
            {
                radius = value;

                if (radius <= 0)
                    radius = 1;

                if (Transform.GameObject.Body != null)
                    (Transform.GameObject.Body.FixtureList[0].Shape as CircleShape).Radius = ConvertUnits.ToSimUnits(radius * Transform.Scale.X);
            }
        }

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
                    (Transform.GameObject.Body.FixtureList[0].Shape as CircleShape).Density = density;
            }
        }

        #endregion

        #region methods

        /// <summary>
        /// Initializes the body
        /// </summary>
        public override void Initialize()
        {
            Transform.GameObject.Body = BodyFactory.CreateCircle(SceneManager.ActiveScene.World, ConvertUnits.ToSimUnits(radius * Transform.scale.X), density);
            Transform.gameObject.physicalBody = this;

            Transform.Position = Transform.position;
            Transform.Rotation = Transform.rotation;

            // Update the body properties:
            UpdateBodyProperties();
        }

        internal override void ResetBody()
        {
            if (Transform.GameObject.Body == null || Transform.gameObject.Body.FixtureList == null) return;

            (Transform.GameObject.Body.FixtureList[0].Shape as CircleShape).Radius = ConvertUnits.ToSimUnits(radius * Transform.Scale.X);
            (Transform.GameObject.Body.FixtureList[0].Shape as CircleShape).Density = density;
        }

        #endregion
    }
}
