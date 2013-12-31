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
    public class VisualScriptConnectionsCollection : System.Collections.CollectionBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="owner"></param>
        public VisualScriptConnectionsCollection()
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public VisualScriptConnection this[int index]
        {
            get { return (VisualScriptConnection)this.List[index]; }
            set { this.List[index] = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int IndexOf(VisualScriptConnection item)
        {
            return base.List.IndexOf(item);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        internal int Add(VisualScriptConnection item)
        {
            return this.List.Add(item);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        public void Remove(VisualScriptConnection item)
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
        public void AddRange(VisualScriptConnectionsCollection collection)
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
        public void AddRange(VisualScriptConnection[] collection)
        {
            this.AddRange(collection);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Contains(VisualScriptConnection item)
        {
            return this.List.Contains(item);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>
        public void Insert(int index, VisualScriptConnection item)
        {

            this.List.Insert(index, item);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        public VisualScriptConnection Find(Predicate<VisualScriptConnection> match)
        {
            foreach (VisualScriptConnection connection in this.List)
            {
                if (match(connection))
                    return connection;
            }

            return null;
        }
    }
}
