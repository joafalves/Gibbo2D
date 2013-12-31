using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Common;
using FarseerPhysics.Common.Decomposition;
using FarseerPhysics.Common.PolygonManipulation;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Gibbo.Library
{
    /// <summary>
    /// 
    /// </summary>
    [Info("Texture Body:\nA body with a dynamic shape that is generated based on a given texture.")]
    public class TextureBody : PhysicalBody
    {
        #region fields

        private string texturePath = "";
        private float density = 1;

#if WINDOWS
        [NonSerialized]
#endif
        private Texture2D texture;

        #endregion

        #region properties

        /// <summary>
        /// The relative path to the texture
        /// </summary>
#if WINDOWS
        [Category("Physical Body Properties")]
        [DisplayName("Texture Path"), Description("The relative path to the texture")]
#endif
        public string TexturePath
        {
            get { return texturePath; }
            set { texturePath = value; this.LoadTexture(); }
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
                    foreach (Fixture s in Transform.gameObject.Body.FixtureList)
                        s.Shape.Density = value;
            }
        }

        #endregion

        #region methods

#if WINDOWS
        [NonSerialized]
#endif
        private uint[] data;

        private void LoadTexture()
        {
            texture = TextureLoader.FromContent(texturePath);

            if (texture != null)
            {
                data = new uint[texture.Width * texture.Height];
                texture.GetData(data);

                ResetBody();
            }
        }

        /// <summary>
        /// Initializes the body
        /// </summary>
        public override void Initialize()
        {
            Transform.gameObject.physicalBody = this;

            //Transform.GameObject.Body = BodyFactory.CreateCircle(SceneManager.ActiveScene.World, ConvertUnits.ToSimUnits(radius), density);
            LoadTexture();
        }

        internal override void ResetBody()
        {
            if (texture != null)
            {
                //Console.WriteLine("aa");
                Vertices outline = PolygonTools.CreatePolygon(data, texture.Width, true);
                Vector2 centroid = -new Vector2(texture.Width / 2, texture.Height / 2);//-outline.GetCentroid();
                outline.Translate(ref centroid);
                outline = SimplifyTools.DouglasPeuckerSimplify(outline, 0.1f);
                List<Vertices> result = Triangulate.ConvexPartition(outline, TriangulationAlgorithm.Bayazit);
                Vector2 scale = ConvertUnits.ToSimUnits(new Vector2(Transform.Scale));
                foreach (Vertices vertices in result)
                {
                    vertices.Scale(ref scale);
                }

                Transform.GameObject.Body = BodyFactory.CreateCompoundPolygon(SceneManager.ActiveScene.World, result, density);

                Transform.Position = Transform.position;
                Transform.Rotation = Transform.rotation;

                UpdateBodyProperties();
            }
        }

        #endregion
    }
}
