using UnityEngine;

namespace _06.GameLib.Runtime
{
    [CreateAssetMenu(fileName = "Pool Item", menuName = "Pooling/Pool Item", order = 10)]
    public class PoolItemSO : ScriptableObject
    {
        [HideInInspector] public string poolingName;
        public GameObject prefab;
        public int initCount;
    }
}