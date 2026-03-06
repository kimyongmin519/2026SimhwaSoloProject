using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Core.Modules;
using UnityEngine;

namespace Weapons
{
    public class WeaponHandler : MonoBehaviour, IModule
    {
        protected ModuleOwner _owner;

        protected NotifyValue<GameObject> _currentWeapon;
        protected Dictionary<WeaponType, GameObject> _inventoryDict;
        public virtual void Initialize(ModuleOwner owner)
        {
            _owner = owner;
            int weaponCount = Enum.GetValues(typeof(WeaponType)).Length;

            _inventoryDict = new Dictionary<WeaponType, GameObject>();
            
            for (int i = 0; i < weaponCount; i++)
                _inventoryDict.Add((WeaponType)i, null);
            
            Weapon[] startWeapons = GetComponentsInChildren<Weapon>();

            foreach (Weapon weapon in startWeapons)
            {
                for (int j = 0; j < weaponCount; j++)
                {
                    if (weapon.WeaponData.WeaponType == (WeaponType)j)
                    {
                        _inventoryDict[(WeaponType)j] = weapon.gameObject;
                    }
                }
            }
            
            foreach (GameObject weapon in _inventoryDict.Values)
            {
                if (weapon != null)
                    weapon.SetActive(false);
            }
            
            _currentWeapon = new NotifyValue<GameObject>(null);
            
            Debug.Assert(_currentWeapon != null, "Not found weapon");
        }
    }
}
