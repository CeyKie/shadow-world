using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Basics
{
    public class PoolObject<T> where T : MonoBehaviour
    {
        private readonly Dictionary<string, T> objectPool;

        public PoolObject(int amount, T pooledObject, Transform parent)
        {
            objectPool = new Dictionary<string, T>();
            
            for (var i = 0; i < amount; i++)
            {
                var pooled = Object.Instantiate(pooledObject, parent);
                Add(i.ToString(), pooled);
                pooled.transform.SetAsLastSibling();
            }
            
            pooledObject.gameObject.SetActive(false);
        }

        public T GetPoolObject(string name)
        {
            return !objectPool.TryGetValue(name, out var monoBehaviour) ? null : monoBehaviour;
        }

        private void Add(string name, T monoBehaviour)
        {
            objectPool.Add(name, monoBehaviour);
        }

        public void Clear()
        {
            foreach (var keyValuePair in objectPool)
            {
                Object.Destroy(keyValuePair.Value);
            }

            objectPool.Clear();
        }
    }
}