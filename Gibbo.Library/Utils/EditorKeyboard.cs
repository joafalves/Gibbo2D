using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Gibbo.Library
{
    /// <summary>
    /// 
    /// </summary>
    public static class EditorKeyboard
    {
        // Since we only have access to KeyboardState's Array constructor, cache arrays to
        // prevent generating garbage on each set or lookup.

        static Keys[] _currentKeys = new Keys[0];
        static Dictionary<int, Keys[]> _arrayCache = new Dictionary<int, Keys[]>();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static KeyboardState GetState()
        {
            return new KeyboardState(_currentKeys);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="playerIndex"></param>
        /// <returns></returns>
        public static KeyboardState GetState(PlayerIndex playerIndex)
        {
            return new KeyboardState(_currentKeys);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keys"></param>
        public static void SetKeys(List<Keys> keys)
        {
            if (!_arrayCache.TryGetValue(keys.Count, out _currentKeys))
            {
                _currentKeys = new Keys[keys.Count];
                _arrayCache.Add(keys.Count, _currentKeys);
            }

            keys.CopyTo(_currentKeys);
        }
    }
}
