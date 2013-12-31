using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    public class GameObjectCollection : ICollection<GameObject>
    {
        [DataMember]
        private List<GameObject> innerList = new List<GameObject>();

        [DataMember]
        private GameObject owner = null;

        public int Count
        {
            get { 
                if(innerList == null)
                    innerList = new List<GameObject>();

                return innerList.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public GameObjectCollection(GameObject owner)
        {
            this.owner = owner;
            innerList = new List<GameObject>();
        }

        #region methods

        public GameObject this[int index]
        {
            get { return (GameObject)this.innerList[index]; }
            set { this.innerList[index] = value; }
        }

        public int IndexOf(GameObject item)
        {
            return innerList.IndexOf(item);
        }

        public void Add(GameObject item)
        {
            if (this.owner != null)
                item.Transform.Parent = this.owner.Transform;
            else
                item.Transform.Parent = null;

            innerList.Add(item);
        }

        public void Clear()
        {
            innerList.Clear();
        }

        public void Insert(int index, GameObject item)
        {
            if (this.owner != null)
                item.Transform.Parent = this.owner.Transform;
            else
                item.Transform.Parent = null;

            if (index < 0)
                index = 0;

            this.innerList.Insert(index, item);
        }

        public bool Contains(GameObject item)
        {
            return innerList.Contains(item);
        }

        public void CopyTo(GameObject[] array, int arrayIndex)
        {
            this.innerList.CopyTo(array, arrayIndex);
        }


        public bool Remove(GameObject item)
        {
            SceneManager.ActiveScene.markedForRemoval.Add(item);
            return true;
        }

        internal void Delete(GameObject item)
        {
            item.RemoveAllComponents();

            innerList.Remove(item);
        }

        public void Remove(GameObject item, bool removeComponents)
        {
            if (removeComponents)
                item.RemoveAllComponents();

            innerList.Remove(item);
        }

        public IEnumerator<GameObject> GetEnumerator()
        {
            if (innerList == null)
                innerList = new List<GameObject>();

            return innerList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return innerList.GetEnumerator();
        }

        public GameObject Find(Predicate<GameObject> match)
        {
            foreach (GameObject gameObject in this.innerList)
            {
                if (match(gameObject))
                    return gameObject;
            }

            return null;
        }

        public int FindIndex(Predicate<GameObject> match)
        {
            for (int i = 0; i < this.innerList.Count; i++)
            {
                if (match(this.innerList[i]))
                    return i;
            }

            return -1;
        }

        public void AddRange(GameObjectCollection collection)
        {
            for (int i = 0; i < collection.Count; i++)
            {
                this.Add(collection[i]);
            }
        }

        public void AddRange(GameObject[] collection)
        {
            this.AddRange(collection);
        }

        #endregion
    }
}
