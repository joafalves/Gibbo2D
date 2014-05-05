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
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.ComponentModel;

#if WINDOWS
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
#endif

using System.Runtime.Serialization;

namespace Gibbo.Library
{
    /// <summary>
    /// The particle emitter class
    /// </summary>
#if WINDOWS
    [Serializable]
#endif
    [DataContract]
    public class ParticleEmitter : GameObject
    {
        #region fields

        private int particleBurstCount = 0;

        [DataMember]
        private bool burst = false;

        [DataMember]
        private int maxParticles;
        [DataMember]
        private float nextSpawn;
        [DataMember]
        private float secElapsed;

        [DataMember]
        private float secPerSpawnMin;
        [DataMember]
        private float secPerSpawnMax;

        [DataMember]
        private float lifespanMin;
        [DataMember]
        private float lifespanMax;

        [DataMember]
        private float initialScaleMin;
        [DataMember]
        private float initialScaleMax;

        [DataMember]
        private float finalScaleMin;
        [DataMember]
        private float finalScaleMax;

        [DataMember]
        private float initialSpeedMin;
        [DataMember]
        private float initialSpeedMax;

        [DataMember]
        private float finalSpeedMin;
        [DataMember]
        private float finalSpeedMax;

        [DataMember]
        private float rotationStrength;

        [DataMember]
        private Vector2 spawnDirection;
        [DataMember]
        private Vector2 spawnAngleNoise;
        [DataMember]
        private Vector2 spawnAngleNoiseUser;
        [DataMember]
        private Color initialColor1;
        [DataMember]
        private Color initialColor2;

        [DataMember]
        private Color finalColor1;
        [DataMember]
        private Color finalColor2;

        //private bool restrictToBoundries;
        [DataMember]
        private bool enabled = true;

        [DataMember]
        private string texturePath;

        [DataMember]
        private BlendModes blendMode = BlendModes.NonPremultiplied;

#if WINDOWS
        [NonSerialized]
#endif
        private LinkedList<Particle> particles;

#if WINDOWS
        [NonSerialized]
#endif
        internal Texture2D texture;

#if WINDOWS
        [NonSerialized]
#endif
        internal BlendState blendState;

#if WINDOWS
        [NonSerialized]
#endif
        private Random random;

        #endregion

        #region properties

        /// <summary>
        /// Determines if the particle emitter should burst or play repeatedly
        /// </summary>
#if WINDOWS
        [Category("Emitter Basic Properties")]
        [DisplayName("Burst"), Description("Determines if the particle emitter should burst or play repeatedly")]
#endif
        public bool Burst
        {
            get { return burst; }
            set { burst = value; particleBurstCount = 0; }
        }

        /// <summary>
        /// The max amount of particles alive in this emmiter
        /// </summary>
        //[PropertyOrder(10)]
#if WINDOWS
        [Category("Emitter Basic Properties")]
        [DisplayName("Max Particles"), Description("The max amount of particles alive in this emmiter")]
#endif
        public int MaxParticles
        {
            get { return maxParticles; }
            set { maxParticles = value; }
        }

        /// <summary>
        /// The minimum amount of time to hold before spawning another particle
        /// </summary>
#if WINDOWS
        [Category("Emitter Basic Properties")]
        [DisplayName("Seconds Per Spawn (Min)"), Description("The minimum amount of time to hold before spawning another particle")]
#endif
        public float SecondsPerSpawnMin
        {
            get { return secPerSpawnMin; }
            set { secPerSpawnMin = value; }
        }

        /// <summary>
        /// The maximum amount of time to hold before spawning another particle
        /// </summary>
#if WINDOWS
        [Category("Emitter Basic Properties")]
        [DisplayName("Seconds Per Spawn (Max)"), Description("The maximum amount of time to hold before spawning another particle")]
#endif
        public float SecondsPerSpawnMax
        {
            get { return secPerSpawnMax; }
            set {
                if (value == 0) value = 0.00001f;                
                secPerSpawnMax = value; }
        }

        /// <summary>
        /// The minimum amount of time that a particle is kept alive
        /// </summary>
#if WINDOWS
        [Category("Emitter Basic Properties")]
        [DisplayName("Lifespan (Min)"), Description("The minimum amount of time that a particle is kept alive")]
#endif
        public float LifespanMin
        {
            get { return lifespanMin; }
            set { lifespanMin = value; }
        }

        /// <summary>
        /// The maximum amount of time that a particle is kept alive
        /// </summary>
#if WINDOWS
        [Category("Emitter Basic Properties")]
        [DisplayName("Lifespan (Max)"), Description("The maximum amount of time that a particle is kept alive")]
#endif
        public float LifespanMax
        {
            get { return lifespanMax; }
            set { lifespanMax = value; }
        }

        /// <summary>
        /// The initial scale of a particle (mininal range)
        /// </summary>
#if WINDOWS
        [Category("Emitter Transform Properties")]
        [DisplayName("Initial Scale (Min)"), Description("The initial scale of a particle (minimum range)")]
#endif
        public float InitialScaleMin
        {
            get { return initialScaleMin; }
            set { initialScaleMin = value; }
        }

        /// <summary>
        /// The initial scale of a particle (mininal range)
        /// </summary>
#if WINDOWS
        [Category("Emitter Transform Properties")]
        [DisplayName("Initial Scale (Max)"), Description("The initial scale of a particle (maximum range)")]
#endif
        public float InitialScaleMax
        {
            get { return initialScaleMax; }
            set { initialScaleMax = value; }
        }

        /// <summary>
        /// The initial scale of a particle (mininal range)
        /// </summary>
#if WINDOWS     
        [Category("Emitter Transform Properties")]
        [DisplayName("Final Scale (Min)"), Description("The final scale of a particle (minimum range)")]
#endif
        public float FinalScaleMin
        {
            get { return finalScaleMin; }
            set { finalScaleMin = value; }
        }

        /// <summary>
        /// The initial scale of a particle (mininal range)
        /// </summary>
#if WINDOWS
        [Category("Emitter Transform Properties")]
        [DisplayName("Final Scale (Max)"), Description("The final scale of a particle (maximum range)")]
#endif
        public float FinalScaleMax
        {
            get { return finalScaleMax; }
            set { finalScaleMax = value; }
        }

        /// <summary>
        /// The initial scale of a particle (mininal range)
        /// </summary>
#if WINDOWS
        [Category("Emitter Transform Properties")]
        [DisplayName("Initial Speed (Min)"), Description("The initial speed of a particle (minimum range)")]
#endif
        public float InitialSpeedMin
        {
            get { return initialSpeedMin; }
            set { initialSpeedMin = value; }
        }

        /// <summary>
        /// The initial scale of a particle (mininal range)
        /// </summary>
#if WINDOWS
        [Category("Emitter Transform Properties")]
        [DisplayName("Initial Speed (Max)"), Description("The initial Speed of a particle (maximum range)")]
#endif
        public float InitialSpeedMax
        {
            get { return initialSpeedMax; }
            set { initialSpeedMax = value; }
        }

        /// <summary>
        /// The initial scale of a particle (mininal range)
        /// </summary>
#if WINDOWS
        [Category("Emitter Transform Properties")]
        [DisplayName("Final Speed (Min)"), Description("The final Speed of a particle (minimum range)")]
#endif
        public float FinalSpeedMin
        {
            get { return finalSpeedMin; }
            set { finalSpeedMin = value; }
        }

        /// <summary>
        /// The initial scale of a particle (mininal range)
        /// </summary>
#if WINDOWS
        [Category("Emitter Transform Properties")]
        [DisplayName("Final Speed (Max)"), Description("The final Speed of a particle (maximum range)")]
#endif
        public float FinalSpeedMax
        {
            get { return finalSpeedMax; }
            set { finalSpeedMax = value; }
        }

        /// <summary>
        /// The direction of a particle
        /// </summary>
#if WINDOWS
        [Category("Emitter Behaviour Properties")]
        [DisplayName("Spawn Direction"), Description("The direction of a particle")]
#endif
        public Vector2 SpawnDirection
        {
            get { return spawnDirection; }
            set { spawnDirection = value; }
        }

        /// <summary>
        /// The spawning noise angle of a particle
        /// </summary>
#if WINDOWS
        [Category("Emitter Behaviour Properties")]
        [DisplayName("Spawn Angle Noise"), Description("The spawning angle noise of a particle")]
#endif
        public Vector2 SpawnAngleNoise
        {
            get { return spawnAngleNoiseUser; }
            set
            {
                spawnAngleNoiseUser = value;
                spawnAngleNoise = new Vector2((float)value.X * (float)MathHelper.Pi, (float)value.Y * -(float)MathHelper.Pi);
            }
        }

        /// <summary>
        /// One possible initial color of a particle
        /// </summary>    
#if WINDOWS
        [Category("Emitter Color Properties")]
        [DisplayName("Initial Color 1"), Description("One possible initial color of a particle")]
#endif
        public Color InitialColor1
        {
            get { return initialColor1; }
            set { initialColor1 = value; }
        }

        /// <summary>
        /// One possible initial color of a particle
        /// </summary>
#if WINDOWS
        [Category("Emitter Color Properties")]
        [DisplayName("Initial Color 2"), Description("One possible initial color of a particle")]
#endif
        public Color InitialColor2
        {
            get { return initialColor2; }
            set { initialColor2 = value; }
        }

        /// <summary>
        /// One possible final color of a particle
        /// </summary>
#if WINDOWS
        [Category("Emitter Color Properties")]
        [DisplayName("Final Color 1"), Description("One possible final color of a particle")]
#endif
        public Color FinalColor1
        {
            get { return finalColor1; }
            set { finalColor1 = value; }
        }

        /// <summary>
        /// One possible final color of a particle
        /// </summary>
#if WINDOWS
        [Category("Emitter Color Properties")]
        [DisplayName("Final Color 2"), Description("One possible final color of a particle")]
#endif
        public Color FinalColor2
        {
            get { return finalColor2; }
            set { finalColor2 = value; }
        }

        ///// <summary>
        ///// Determines if the particles are restricted to the collision model of the emitter
        ///// </summary>
        //[Category("Emitter Behaviour Properties")]
        //[DisplayName("Restrict To Boundries"), Description("Determines if the particles are restricted to the collision model of the emitter")]
        //public bool RestrictToBoundries
        //{
        //    get { return restrictToBoundries; }
        //    set { restrictToBoundries = value; }
        //}

        /// <summary>
        /// Determines if the particles are enabled
        /// </summary>
#if WINDOWS
        [Category("Emitter Basic Properties")]
        [DisplayName("Enabled"), Description("Determines if the particles are enabled")]
#endif
        public bool Enabled
        {
            get { return enabled; }
            set { enabled = value; }
        }

        /// <summary>
        /// The relative path to the texture
        /// </summary>
#if WINDOWS
        [Category("Emitter Basic Properties")]
        [DisplayName("Image Name"), Description("The relative path to the texture")]
#endif
        public string ImageName
        {
            get { return texturePath; }
            set { texturePath = value; this.LoadTexture(); }
        }

        /// <summary>
        /// The blending mode
        /// </summary>
#if WINDOWS
        [Category("Emitter Color Properties")]
        [DisplayName("Blend Mode"), Description("The blending mode")]
#endif
        public BlendModes BlendMode
        {
            get { return blendMode; }
            set { blendMode = value; this.LoadState(); }
        }

        /// <summary>
        /// The rotation strength of the particles
        /// </summary>
#if WINDOWS
        [Category("Emitter Behaviour Properties")]
        [DisplayName("Rotation Strength"), Description("The rotation strength of the particles")]
#endif
        public float RotationStrength
        {
            get { return rotationStrength; }
            set { rotationStrength = value; }
        }

        #endregion

        #region constructors

        /// <summary>
        /// 
        /// </summary>
        public ParticleEmitter()
        {
            SecondsPerSpawnMin = 0.01f;
            SecondsPerSpawnMax = 0.015f;
            SpawnDirection = new Vector2(0, -1);
            SpawnAngleNoise = new Vector2(1, 1);
            LifespanMin = 1;
            LifespanMax = 2;
            InitialScaleMin = 1;
            InitialScaleMax = 2;
            FinalScaleMin = 2;
            FinalScaleMax = 4;
            InitialColor1 = Color.White;
            InitialColor2 = Color.White;
            FinalColor1 = Color.Black;
            FinalColor2 = Color.Black;
            InitialSpeedMin = 80;
            InitialSpeedMax = 120;
            FinalSpeedMin = 0;
            FinalSpeedMax = 20;
            MaxParticles = 100;

            random = new Random();
        }

        #endregion

        #region methods

        /// <summary>
        /// Initializes this instance
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();

            random = new Random();

            if (texture == null && texturePath != null && texturePath.Trim() != "")
            {
                LoadTexture();
            }

            LoadState();

            nextSpawn = MathExtension.LinearInterpolate(SecondsPerSpawnMin, SecondsPerSpawnMax, random.NextDouble());
            secElapsed = 0.0f;
            particles = new LinkedList<Particle>();
        }

        private void LoadTexture()
        {
            texture = TextureLoader.FromContent(texturePath);
        }

        private void LoadState()
        {
            switch (this.blendMode)
            {
                case BlendModes.NonPremultiplied:
                    this.blendState = BlendState.NonPremultiplied;
                    break;
                case BlendModes.AlphaBlend:
                    this.blendState = BlendState.AlphaBlend;
                    break;
                case BlendModes.Additive:
                    this.blendState = BlendState.Additive;
                    break;
            }
        }

        /// <summary>
        /// Updates this instance
        /// </summary>
        /// <param name="gameTime">The gametime</param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (texture != null)
            {
                secElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;
                while (secElapsed > nextSpawn)
                {
                    if (particles.Count < MaxParticles && enabled)
                    {
                        // Spawn a particle
                        Vector2 StartDirection = Vector2.Transform(SpawnDirection, Matrix.CreateRotationZ(MathExtension.LinearInterpolate(spawnAngleNoise.X, spawnAngleNoise.Y, random.NextDouble())));
                        StartDirection.Normalize();
                        Vector2 EndDirection = StartDirection * MathExtension.LinearInterpolate(FinalSpeedMin, FinalSpeedMax, random.NextDouble());
                        StartDirection *= MathExtension.LinearInterpolate(InitialSpeedMin, InitialSpeedMax, random.NextDouble());
                        particles.AddLast(new Particle(
                            Transform.Position,
                            StartDirection,
                            EndDirection,
                            MathExtension.LinearInterpolate(LifespanMin, LifespanMax, random.NextDouble()),
                            MathExtension.LinearInterpolate(InitialScaleMin, InitialScaleMax, random.NextDouble()),
                            MathExtension.LinearInterpolate(FinalScaleMin, FinalScaleMax, random.NextDouble()),
                            MathExtension.LinearInterpolate(InitialColor1, InitialColor2, random.NextDouble()),
                            MathExtension.LinearInterpolate(FinalColor1, FinalColor2, random.NextDouble()),
                            this)
                        );

                        particles.Last.Value.Update(secElapsed);
                    }

                    secElapsed -= nextSpawn;
                    nextSpawn = MathExtension.LinearInterpolate(SecondsPerSpawnMin, SecondsPerSpawnMax, random.NextDouble());

                    if (burst)
                        particleBurstCount++;

                    if (burst && particleBurstCount >= maxParticles)
                    {
                        this.enabled = false;
                        this.particleBurstCount = 0;
                    }
                }
            }

            LinkedListNode<Particle> node = particles.First;
            while (node != null)
            {
                bool isAlive = node.Value.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
                node = node.Next;
                if (!isAlive)
                {
                    if (node == null)
                    {
                        particles.RemoveLast();
                    }
                    else
                    {
                        particles.Remove(node.Previous);
                    }
                }
            }
        }

        /// <summary>
        /// Draws this instance
        /// </summary>
        /// <param name="gameTime">The gametime</param>
        /// <param name="spriteBatch">The spriteBatch</param>
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);

            if (texture != null && Visible)
            {
                spriteBatch.Begin(SpriteSortMode.Deferred, this.blendState, null, null, null, null, SceneManager.ActiveCamera.TransformMatrix);

                LinkedListNode<Particle> node = particles.First;
                while (node != null)
                {
                    node.Value.Draw(spriteBatch);
                    node = node.Next;
                }

                spriteBatch.End();
            }
        }

        /// <summary>
        /// Clears the active particles
        /// </summary>
        public void Clear()
        {
            particles.Clear();
        }

        #endregion
    }
}
