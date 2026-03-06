using UnityEngine;

namespace Weapons
{
    public class WeaponDataSO : ScriptableObject
    {
        [field:SerializeField] public float PingHeight {get; private set;}
        [field: SerializeField] public float WeaponMoveSpeedMultifier { get; private set; } = 1f;
        [field: SerializeField] public GameObject WeaponPrefab { get; private set; }
        [field:SerializeField] public WeaponType WeaponType {get; private set;}
    }
}