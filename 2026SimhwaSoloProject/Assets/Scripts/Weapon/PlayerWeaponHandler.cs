using Agents.Players;
using Core.Modules;
using UnityEngine;

namespace Weapon
{
    public class PlayerWeaponHandler : WeaponHandler
    {
        private Player _player;
        [SerializeField] private WeaponDataSO testWeaponData;

        public override void Initialize(ModuleOwner owner)
        {
            base.Initialize(owner);
            _player = owner as Player;
            
            ChangeWeapon(_inventoryDict[WeaponType.SECONDARY_GUN]);
        }

        private void ChangeWeapon(GameObject weaponObj)
        {
            Weapon weapon = _currentWeapon.Value.GetComponent<Weapon>();
            
            Debug.Assert(weaponObj != null, "weapon is null");
            _player.PlayerInput.OnAttackPressed -= weapon.WeaponUse;
            _player.PlayerInput.OnAttackReleased -= weapon.WeaponCancel;

            _currentWeapon.Value = weaponObj;
            Debug.Assert(_currentWeapon.Value != null, "Change weapon is null");

            weapon = _currentWeapon.Value.GetComponent<Weapon>();
            
            _player.PlayerInput.OnAttackPressed += weapon.WeaponUse;
            _player.PlayerInput.OnAttackReleased += weapon.WeaponCancel;
        }
    }
}