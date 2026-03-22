using System;
using _06.GameLib.EventSystem;
using Agents.Players;
using Core;
using Core.Modules;
using GameSystems.GameEvents.ChannelEvent;
using UnityEngine;
using Weapons.Guns;

namespace Weapons
{
    public class PlayerWeaponHandler : WeaponHandler
    {
        private Player _player;
        [SerializeField] private Transform weaponCenter;
        [SerializeField] private PlayerInputSO playerInput;
        [SerializeField] private EventChannelSO playerChannel;
         
        public Weapon CurrentWeaponClass { get; private set; }

        [SerializeField] private WeaponType startWeaponType;

        private bool _completeStartSetting = false;

        public Action<float> OnWeaponChanged;

        public override void Initialize(ModuleOwner owner)
        {
            base.Initialize(owner);
            _player = owner as Player;
            Debug.Assert(_player != null, "owner not casting player");

            _player.PlayerInput.OnWeaponChanged += ChangeWeapon;

            _currentWeapon.Value = _inventoryDict[startWeaponType];
            CurrentWeaponClass = _currentWeapon.Value.GetComponent<Weapon>();
            Debug.Assert(CurrentWeaponClass != null, $"{gameObject.name} Start weapon is null");
            
            ChangeWeapon(startWeaponType);
        }
        
        public void ChangeWeapon(WeaponType type)
        {
            if (_inventoryDict[type] == null) return;
            
            if (type == CurrentWeaponClass.WeaponData.WeaponType && _completeStartSetting) return;
            
            _completeStartSetting = true;
            
            CurrentWeaponClass = _currentWeapon.Value.GetComponent<Weapon>();
            
            Debug.Assert(_inventoryDict[type] != null, "weapon is null");
            _player.PlayerInput.OnAttackPressed -= CurrentWeaponClass.WeaponUse;
            _player.PlayerInput.OnAttackReleased -= CurrentWeaponClass.WeaponCancel;

            AbstractGun gunClass = CurrentWeaponClass as AbstractGun;
            if (gunClass != null)
                _player.PlayerInput.OnReloadPressed-= gunClass.GunReload;
                
            _currentWeapon.Value.gameObject.SetActive(false);

            _currentWeapon.Value = _inventoryDict[type];
            Debug.Assert(_currentWeapon.Value != null, "Change weapon is null");

            _currentWeapon.Value.gameObject.SetActive(true);
            
            CurrentWeaponClass = _currentWeapon.Value.GetComponent<Weapon>();
            
            gunClass = CurrentWeaponClass as AbstractGun;
            
            _player.PlayerInput.OnAttackPressed += CurrentWeaponClass.WeaponUse;
            _player.PlayerInput.OnAttackReleased += CurrentWeaponClass.WeaponCancel;
            if (gunClass != null)
                _player.PlayerInput.OnReloadPressed += gunClass.GunReload;
            
            CurrentWeaponClass.WeaponSetting();
            CurrentWeaponClass.WeaponEquip();
            
            OnWeaponChanged?.Invoke(CurrentWeaponClass.WeaponData.WeaponMoveSpeedMultifier);
            
            playerChannel.RaiseEvent(PlayerUIEvent.PlayerInventoryChange.Init(CurrentWeaponClass));
        }

        public void InventoryChanged(PlayerInventoryChange evt)
        {
            Weapon newWeapon = Instantiate(evt.Weapon.WeaponData.WeaponPrefab, weaponCenter).GetComponent<Weapon>();

            if (_inventoryDict[newWeapon.WeaponData.WeaponType] != null)
            {
                Destroy(_inventoryDict[newWeapon.WeaponData.WeaponType]);
                _inventoryDict[newWeapon.WeaponData.WeaponType] = newWeapon.gameObject;
            }
            else
            {
                _inventoryDict[newWeapon.WeaponData.WeaponType] = newWeapon.gameObject;
            }
            
            ChangeWeapon(newWeapon.WeaponData.WeaponType);
        }

        private void Update()
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            CurrentWeaponClass?.SetTarget(mousePos);
        }

        private void OnDestroy()
        {
            _player.PlayerInput.OnWeaponChanged -= ChangeWeapon;
        }

    }
}