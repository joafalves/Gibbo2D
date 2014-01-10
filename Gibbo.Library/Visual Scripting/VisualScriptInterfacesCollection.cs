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

namespace Gibbo.Library
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class VisualScriptInterfacesCollection : System.Collections.CollectionBase
    {
        private VisualScriptNode owner;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="owner"></param>
        public VisualScriptInterfacesCollection(VisualScriptNode owner)
        {
            this.owner = owner;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public VisualScriptNodeInterface this[int index]
        {
            get { return (VisualScriptNodeInterface)this.List[index]; }
            set { this.List[index] = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int IndexOf(VisualScriptNodeInterface item)
        {
            return base.List.IndexOf(item);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int Add(VisualScriptNodeInterface item)
        {
            item.Parent = this.owner;
            return this.List.Add(item);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        public void Remove(VisualScriptNodeInterface item)
        {
            this.InnerList.Remove(item);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        public void CopyTo(Array array, int index)
        {
            this.List.CopyTo(array, index);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="collection"></param>
        public void AddRange(VisualScriptInterfacesCollection collection)
        {
            for (int i = 0; i < collection.Count; i++)
            {
                this.List.Add(collection[i]);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="collection"></param>
        public void AddRange(VisualScriptNodeInterface[] collection)
        {
            this.AddRange(collection);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Contains(VisualScriptNodeInterface item)
        {
            return this.List.Contains(item);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>
        public void Insert(int index, VisualScriptNodeInterface item)
        {
            item.Parent = this.owner;
            this.List.Insert(index, item);
        }
    }
}
