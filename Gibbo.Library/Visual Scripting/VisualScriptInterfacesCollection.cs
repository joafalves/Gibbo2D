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
