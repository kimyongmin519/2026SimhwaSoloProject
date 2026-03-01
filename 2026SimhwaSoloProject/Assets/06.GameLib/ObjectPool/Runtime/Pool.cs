using System.Collections.Generic;
using _06.GameLib.Runtime;
using UnityEngine;

namespace _06.GameLib.ObjectPool.Runtime
{
    public class Pool
    {
        private readonly Stack<IPoolable> _pool;
        private readonly Transform _parent;
        private readonly GameObject _prefab;

        public Pool(PoolItemSO poolItem, Transform parent, int initCount)
        {
            _parent = parent;
            _prefab = poolItem.prefab;
            _pool = new Stack<IPoolable>();

            for (int i = 0; i < initCount; i++)
            {
                GameObject gameObject = Object.Instantiate(_prefab, _parent);
                gameObject.SetActive(false);
                IPoolable poolable = gameObject.GetComponent<IPoolable>();
                Debug.Assert(poolable != null, "gameObject should have a poolable");
                
                _pool.Push(poolable);
            }
        }

        public IPoolable Pop()
        {
            IPoolable item;

            if (_pool.Count == 0)
            {
                GameObject gameObject = Object.Instantiate(_prefab, _parent);
                item = gameObject.GetComponent<IPoolable>();
            }
            else
            {
                item = _pool.Pop();
                item.GameObject.SetActive(true);
            }
            item.ResetItem();
            return item;
        }

        public void Push(IPoolable item)
        {
            item.GameObject.SetActive(false);
            _pool.Push(item);
        }
    }
}