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
