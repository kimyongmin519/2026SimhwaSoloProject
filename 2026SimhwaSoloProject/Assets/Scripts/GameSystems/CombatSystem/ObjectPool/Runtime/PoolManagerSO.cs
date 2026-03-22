using System.Collections.Generic;
using UnityEngine;

namespace _06.GameLib.ObjectPool.Runtime
{
    [CreateAssetMenu(fileName = "Pool manager", menuName = "Pooling/Pool manager", order = 10)]
    public class PoolManagerSO : ScriptableObject
    {
        public List<PoolItemSO> itemList = new();
        
        private Dictionary<PoolItemSO, Pool> _poolDict = new();
        private Transform _rootTrm;
        
        public void InitializePool(Transform rootTrm)
        {
            _rootTrm = rootTrm;
            _poolDict = new Dictionary<PoolItemSO, Pool>();

            foreach (PoolItemSO item in itemList)
            {
                IPoolable poolable = item.prefab.GetComponent<IPoolable>();
                Debug.Assert(poolable != null, $"Poolable component not found {item.prefab.name}");

                Pool pool = new Pool(item, _rootTrm, item.initCount);
                _poolDict.Add(item, pool);
            }
        }

        public T Pop<T>(PoolItemSO type) where T : IPoolable
        {
            Debug.Assert(_rootTrm != null, "오브젝트 풀을 사용하기 전에는 무적권 풀 초기화 필요");
            
            if (_poolDict.TryGetValue(type, out Pool pool))
            {
                return (T)pool.Pop();
            }

            return default;
        }

        public void Push(IPoolable item)
        {
            Debug.Assert(_rootTrm != null, "오브젝트를 사용하기 전에는 무적권 풀 초기화 필요");
            if (_poolDict.TryGetValue(item.PoolItem, out Pool pool))
            {
                pool.Push(item);
            }
        }
    }
}