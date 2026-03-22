using UnityEngine;

namespace _06.GameLib.ObjectPool.Runtime
{
    public interface IPoolable
    {
        public PoolItemSO PoolItem { get; }
        public GameObject GameObject { get; }
        public void ResetItem();
    }
}