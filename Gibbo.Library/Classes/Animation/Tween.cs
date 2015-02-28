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
    public class Tween : ITween
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

        internal Transform target;
        private float currentElapsed;
        internal float duration; // milliseconds
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
        public Transform Target
        {
            get { return target; }
        }

        #endregion

        #region constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        public Tween(Transform target)
        {
            this.target = target;
        }

        public Tween(GameObject target)
        {
            this.target = target.Transform;
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
                currentElapsed = MathHelper.Clamp(currentElapsed, 0, duration);

                SetTransform();

                if (currentElapsed == duration)
                {
                    if (!loop)
                    {
                        paused = true;
                    }
                    else
                    {
                        currentElapsed = 0;
                        SetTransform();
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

        private bool _inverse = false;

        private void SetTransform()
        {
            if (targetTransform.position.X != initialTransform.position.X)
                target.SetPositionX(initialTransform.Position.X + ((currentElapsed * (targetTransform.Position.X - initialTransform.Position.X)) / duration));

            if (targetTransform.position.Y != initialTransform.position.Y)
            {
                target.SetPositionY(initialTransform.Position.Y + ((currentElapsed * (targetTransform.Position.Y - initialTransform.Position.Y)) / duration));
                //Console.WriteLine("entreiy");
            }
            //if(targetTransform.position != initialTransform.position)
            //    target.Transform.Position = new Vector2()
            //    {
            //        X = initialTransform.Position.X + ((currentElapsed * (targetTransform.Position.X - initialTransform.Position.X)) / duration),
            //        Y = initialTransform.Position.Y + ((currentElapsed * (targetTransform.Position.Y - initialTransform.Position.Y)) / duration)
            //    };

            //or: (untested)
            //target.Position = initialPosition + (currentElapsed * (destination - initialPosition) / duration);

            if (!_inverse)
            {
                if (targetTransform.Rotation != initialTransform.rotation)
                    target.Rotation = initialTransform.rotation + ((currentElapsed * (targetTransform.rotation - initialTransform.rotation)) / duration);
            }
            else
            {
                // inversed rotation
                if (targetTransform.Rotation != initialTransform.rotation)
                    target.Rotation = initialTransform.rotation + ((currentElapsed * (-1 * targetTransform.rotation - initialTransform.rotation)) / duration);
            }

            if (targetTransform.scale != initialTransform.scale)
                target.Scale = initialTransform.scale + ((currentElapsed * (targetTransform.scale - initialTransform.scale)) / duration);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        /// <param name="duration"></param>
        /// <param name="initialDelay"></param>
        public void To(Vector2 position, float duration, bool inverseRotation = false, float initialDelay = 0)
        {
            Transform t = new Transform()
            {
                position = position,
                scale = new Vector2(target.scale.X, target.scale.Y),
                rotation = target.rotation
            };

            _inverse = inverseRotation;

            this.To(t, duration, inverseRotation, initialDelay);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="targetTransform"></param>
        /// <param name="duration"></param>
        /// <param name="initialDelay"></param>
        /// <param name="inverseRotation"></param>
        public void To(Transform targetTransform, float duration, bool inverseRotation = false, float initialDelay = 0)
        {
            this.targetTransform = targetTransform.DeepCopy();
            this.initialTransform = target.DeepCopy();

            _inverse = inverseRotation;

            this.duration = duration;
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
