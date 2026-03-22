using UnityEngine;

namespace _06.GameLib.ObjectPool.Runtime
{
    public class PoolMono : MonoBehaviour, IPoolable
    {
        [field:SerializeField] public PoolItemSO PoolItem { get;set; }
        public GameObject GameObject => this != null ? gameObject : null;
        public void ResetItem()
        {
            
        }
    }
}