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
using System.Collections;
using System.Collections.Generic;

namespace FarseerPhysics.Common.Decomposition.CDT.Util
{
    internal struct FixedBitArray3 : IEnumerable<bool>
    {
        public bool _0, _1, _2;

        public bool this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0:
                        return _0;
                    case 1:
                        return _1;
                    case 2:
                        return _2;
                    default:
                        throw new IndexOutOfRangeException();
                }
            }
            set
            {
                switch (index)
                {
                    case 0:
                        _0 = value;
                        break;
                    case 1:
                        _1 = value;
                        break;
                    case 2:
                        _2 = value;
                        break;
                    default:
                        throw new IndexOutOfRangeException();
                }
            }
        }

        #region IEnumerable<bool> Members

        public IEnumerator<bool> GetEnumerator()
        {
            return Enumerate().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        public bool Contains(bool value)
        {
            for (int i = 0; i < 3; ++i) if (this[i] == value) return true;
            return false;
        }

        public int IndexOf(bool value)
        {
            for (int i = 0; i < 3; ++i) if (this[i] == value) return i;
            return -1;
        }

        public void Clear()
        {
            _0 = _1 = _2 = false;
        }

        public void Clear(bool value)
        {
            for (int i = 0; i < 3; ++i) if (this[i] == value) this[i] = false;
        }

        private IEnumerable<bool> Enumerate()
        {
            for (int i = 0; i < 3; ++i) yield return this[i];
        }
    }
}