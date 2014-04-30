using Microsoft.Xna.Framework;
using System;
using System.Runtime.Serialization;

namespace Gibbo.Library
{
    /// <summary>
    /// 
    /// </summary>
#if WINDOWS
    [Serializable]
#endif
    [DataContract]
    public class ConstantTween : ITween
    {
        #region events

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler TweenCompleted;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler TweenStarted;

        #endregion

        #region fields

        internal GameObject target;
        private float currentElapsed;
        internal float rate;
        internal float initialDelay; // milliseconds
        private Transform initialTransform;
        private Transform targetTransform;
        private bool paused = true;
        private bool waitingForDelay = false;
        private bool loop = false;

        #endregion

        #region properties

        /// <summary>
        /// 
        /// </summary>
        public Transform InitialTransform
        {
            get { return initialTransform.DeepCopy(); }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool Loop
        {
            get { return loop; }
            set { loop = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public GameObject Target
        {
            get { return target; }
        }

        #endregion

        #region constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        public ConstantTween(GameObject target)
        {
            this.target = target;
        }

        #endregion

        #region methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            if (waitingForDelay)
            {
                currentElapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                if (currentElapsed > initialDelay)
                {
                    currentElapsed = 0;
                    waitingForDelay = false;

                    EventHandler handler = TweenStarted;
                    if (handler != null)
                    {
                        handler(this, null); // notify completed
                    }
                }
            }

            if (!paused && !waitingForDelay)
            {
                currentElapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                //currentElapsed = MathHelper.Clamp(currentElapsed, 0, duration);

                SetTransform(rate * ((float)gameTime.ElapsedGameTime.TotalSeconds));

                //Console.WriteLine(targetTransform.Position + "::" + target.Transform.Position);

                // TODO: check other properties
                if (targetTransform.Position == target.Transform.Position)
                {
                    if (!loop)
                    {
                        paused = true;
                    }
                    else
                    {
                        currentElapsed = 0;
                        SetTransform(rate * ((float)gameTime.ElapsedGameTime.TotalSeconds));
                    }

                    if (initialDelay != 0)
                    {
                        waitingForDelay = true;
                    }

                    EventHandler handler = TweenCompleted;
                    if (handler != null)
                    {
                        handler(this, null); // notify completed
                    }
                }
            }
        }

        private void SetTransform(float trate)
        {
            //Console.WriteLine("trate: " + trate);

            if (targetTransform.position.X != initialTransform.position.X)
            {
                int direction = -1;
                if (initialTransform.position.X < targetTransform.position.X)
                    direction = 1;

                target.Transform.SetPositionX(target.Transform.Position.X + trate * direction);

                if ((direction == 1 && target.Transform.position.X > targetTransform.position.X)
                    || (direction == -1 && target.Transform.position.X < targetTransform.position.X))
                    target.Transform.SetPositionX(targetTransform.position.X);             
            }
            if (targetTransform.position.Y != initialTransform.position.Y)
            {
                int direction = -1;
                if (initialTransform.position.Y < targetTransform.position.Y)
                    direction = 1;

                target.Transform.SetPositionY(target.Transform.Position.Y + trate * direction);

                if ((direction == 1 && target.Transform.position.Y > targetTransform.position.Y)
                    || (direction == -1 && target.Transform.position.Y < targetTransform.position.Y))
                {
                    target.Transform.SetPositionY(targetTransform.position.Y);                    
                }         
            }

            // TODO: rotation:
            //if (targetTransform.Rotation != initialTransform.rotation)
            //    target.Transform.Rotation = initialTransform.rotation + ((currentElapsed * (targetTransform.rotation - initialTransform.rotation)) / duration);

            //if (targetTransform.scale != initialTransform.scale)
            //    target.Transform.Scale = initialTransform.scale + ((currentElapsed * (targetTransform.scale - initialTransform.scale)) / duration);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        /// <param name="rate"></param>
        /// <param name="initialDelay"></param>
        public void To(Vector2 position, float rate, float initialDelay = 0)
        {
            Transform t = new Transform()
            {
                position = position,
                scale = new Vector2(target.Transform.scale.X, target.Transform.scale.Y),
                rotation = target.Transform.rotation
            };

            this.To(t, rate, initialDelay);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="targetTransform"></param>
        /// <param name="rate"></param>
        /// <param name="initialDelay"></param>
        public void To(Transform targetTransform, float rate, float initialDelay = 0)
        {
            this.targetTransform = targetTransform.DeepCopy();
            this.initialTransform = target.Transform.DeepCopy();

            this.rate = rate;
            this.currentElapsed = 0;
            this.paused = false;
            this.initialDelay = initialDelay;

            if (initialDelay != 0)
                waitingForDelay = true;
            else
                waitingForDelay = false;
        }

        #endregion
    }
}
