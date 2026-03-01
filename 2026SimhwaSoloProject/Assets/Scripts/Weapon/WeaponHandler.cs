using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Core.Modules;
using UnityEngine;

namespace Weapon
{
    public class WeaponHandler : MonoBehaviour, IModule
    {
        protected ModuleOwner _owner;

        protected NotifyValue<GameObject> _currentWeapon;
        protected Dictionary<WeaponType, GameObject> _inventoryDict;
        public virtual void Initialize(ModuleOwner owner)
        {
            _owner = owner;
            _inventoryDict = GetComponentsInChildren<Weapon>().ToDictionary(
                weapon => weapon.WeaponData.WeaponType,
                weapon => weapon.gameObject);
            
            foreach (GameObject weapon in _inventoryDict.Values)
            {
                weapon.SetActive(false);
            }
            
            _currentWeapon = new NotifyValue<GameObject>(null);
            
            Debug.Assert(_currentWeapon != null, "Not found weapon");
        }
    }
}
