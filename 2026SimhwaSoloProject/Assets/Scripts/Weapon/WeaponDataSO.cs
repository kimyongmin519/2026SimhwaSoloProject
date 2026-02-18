using System;
using UnityEngine;

namespace Weapon
{
    public class WeaponDataSO : ScriptableObject
    {
        [field:SerializeField] public float PingHeight {get; private set;}
        [field: SerializeField] public GameObject WeaponPrefab { get; private set; }
        [field:SerializeField] public WeaponType WeaponType {get; private set;}
    }
}