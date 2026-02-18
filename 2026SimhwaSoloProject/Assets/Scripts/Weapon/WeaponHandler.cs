using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Core.Modules;
using UnityEngine;
using Weapon.Guns;

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
            _currentWeapon = new NotifyValue<GameObject>(_inventoryDict[WeaponType.SECONDARY_GUN]);
            
            Debug.Assert(_currentWeapon != null, "Not found weapon");
        }
    }
}
