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

        [Obsolete("Target property should be used instead", true)]
        internal GameObject target;

        [Obsolete("Not used anymore", true)]
        internal float duration; // milliseconds
        
        [Obsolete("Not used anymore", true)]
        internal float initialDelay; // milliseconds

        private float _duration;
        private float _initialDelay;
        private float _currentElapsed;
        private Transform _initialTransform;
        private Transform _targetTransform;
        private bool _paused = true;
        private bool _waitingForDelay = false;
        private bool _loop = false;
        private bool _inverse = false;

        #endregion

        #region properties
        /// <summary>
        /// Deep Copy of initial transform
        /// </summary>
        public Transform InitialTransform
        {
            get { return _initialTransform.DeepCopy(); }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool Loop
        {
            get { return _loop; }
            set { _loop = value; }
        }

        /// <summary>
        /// Transform as Target
        /// </summary>
        public Transform Target
        {
            get;
            private set;
        }

        //[Obsolete("Transform should be used instead", true)]
        //public GameObject Target
        //{
        //    get;
        //    private set;
        //}
        

        #endregion

        #region constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        public Tween(Transform target)
        {
            Target = target;
        }

        public Tween(GameObject target)
        {
            Target = target.Transform;
        }

        #endregion

        #region methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            if (_waitingForDelay)
            {
                _currentElapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                if (_currentElapsed > _initialDelay)
                {
                    _currentElapsed = 0;
                    _waitingForDelay = false;

                    EventHandler handler = TweenStarted;
                    if (handler != null)
                    {
                        handler(this, null); // notify completed
                    }
                }
            }

            if (!_paused && !_waitingForDelay)
            {
                _currentElapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                _currentElapsed = MathHelper.Clamp(_currentElapsed, 0, _duration);

                SetTransform();

                if (_currentElapsed == _duration)
                {
                    if (!_loop)
                    {
                        _paused = true;
                    }
                    else
                    {
                        _currentElapsed = 0;
                        SetTransform();
                    }

                    if (_initialDelay != 0)
                    {
                        _waitingForDelay = true;
                    }

                    EventHandler handler = TweenCompleted;
                    if (handler != null)
                    {
                        handler(this, null); // notify completed
                    }
                }
            }
        }

        private void SetTransform()
        {
            if (_targetTransform.position.X != _initialTransform.position.X)
                Target.SetPositionX(_initialTransform.Position.X + ((_currentElapsed * (_targetTransform.Position.X - _initialTransform.Position.X)) / _duration));

            if (_targetTransform.position.Y != _initialTransform.position.Y)
            {
                Target.SetPositionY(_initialTransform.Position.Y + ((_currentElapsed * (_targetTransform.Position.Y - _initialTransform.Position.Y)) / _duration));
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
                if (_targetTransform.Rotation != _initialTransform.rotation)
                    Target.Rotation = _initialTransform.rotation + ((_currentElapsed * (_targetTransform.rotation - _initialTransform.rotation)) / _duration);
            }
            else
            {
                // inversed rotation
                if (_targetTransform.Rotation != _initialTransform.rotation)
                    Target.Rotation = _initialTransform.rotation + ((_currentElapsed * (-1 * _targetTransform.rotation - _initialTransform.rotation)) / _duration);
            }

            if (_targetTransform.scale != _initialTransform.scale)
                Target.Scale = _initialTransform.scale + ((_currentElapsed * (_targetTransform.scale - _initialTransform.scale)) / _duration);
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
                scale = new Vector2(Target.scale.X, Target.scale.Y),
                rotation = Target.rotation
            };

            _inverse = inverseRotation;

            this.To(t, duration, inverseRotation, initialDelay);
        }

        public void To(Vector2 position, float duration, float initialDelay = 0)
        {
            Transform t = new Transform()
            {
                position = position,
                scale = new Vector2(Target.scale.X, Target.scale.Y),
                rotation = Target.rotation
            };

            this.To(t, duration, false, initialDelay);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="targetTransform"></param>
        /// <param name="duration"></param>
        /// <param name="initialDelay"></param>
        public void To(Transform targetTransform, float duration, float initialDelay = 0)
        {
            this.To(targetTransform, duration, false, initialDelay);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="targetTransform"></param>
        /// <param name="duration"></param>
        /// <param name="inverseRotation"></param>
        /// <param name="initialDelay"></param>
        public void To(Transform targetTransform, float duration, bool inverseRotation = false, float initialDelay = 0)
        {
            this._targetTransform = targetTransform.DeepCopy();
            this._initialTransform = Target.DeepCopy();

            this._inverse = inverseRotation;

            this._duration = duration;
            this._currentElapsed = 0;
            this._paused = false;
            this._initialDelay = initialDelay;

            if (initialDelay != 0)
                _waitingForDelay = true;
            else
                _waitingForDelay = false;
        }

        #endregion
    }
}
