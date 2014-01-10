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

using System.Runtime.Serialization;

namespace Gibbo.Library
{
#if WINDOWS
    [Serializable]
#endif
    [DataContract]
    class PropertyLabel
    {
        #region inner classes

        /// <summary>
        /// 
        /// </summary>
#if WINDOWS
        [Serializable]
#endif
        [DataContract]
        public class EqualityComparer : IEqualityComparer<PropertyLabel>
        {
            /// <summary>
            /// 
            /// </summary>
            /// <param name="x"></param>
            /// <param name="y"></param>
            /// <returns></returns>
            bool IEqualityComparer<PropertyLabel>.Equals(PropertyLabel x, PropertyLabel y)
            {
                return x.Equals(y);
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="obj"></param>
            /// <returns></returns>
            int IEqualityComparer<PropertyLabel>.GetHashCode(PropertyLabel obj)
            {
                return base.GetHashCode();
            }
        }

        #endregion

        #region fields

        [DataMember]
        private string typeName;

        [DataMember]
        private string name;

        #endregion

        #region properties

        /// <summary>
        /// 
        /// </summary>
        public string TypeName
        {
            get { return typeName; }
            set { typeName = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        #endregion

        #region constructors

        /// <summary>
        /// 
        /// </summary>
        public PropertyLabel() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="name"></param>
        public PropertyLabel(string typeName, string name)
        {
            this.typeName = typeName;
            this.name = name;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(PropertyLabel other)
        {
            return (this.typeName.Equals(other.typeName) && this.name.Equals(other.name));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static bool operator ==(PropertyLabel p1, PropertyLabel p2)
        {
            return p1.Equals(p2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static bool operator !=(PropertyLabel p1, PropertyLabel p2)
        {
            return !p1.Equals(p2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.name + ", " + this.typeName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion
    }
}
