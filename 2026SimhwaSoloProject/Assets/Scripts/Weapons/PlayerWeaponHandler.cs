using System;
using Agents.Players;
using Core;
using Core.Modules;
using UnityEngine;
using Weapons.Guns;

namespace Weapons
{
    public class PlayerWeaponHandler : WeaponHandler
    {
        private Player _player;
        [SerializeField] private WeaponDataSO testWeaponData;
        [SerializeField] private PlayerInputSO playerInput;
        [SerializeField] private Transform ping;
        [SerializeField] private Weapon testWeapon;

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

        private void Start()
        {
            //InventoryChanged(testWeapon);
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

            CurrentWeaponClass = _currentWeapon.Value.GetComponent<Weapon>();
            
            gunClass = CurrentWeaponClass as AbstractGun;
            
            _player.PlayerInput.OnAttackPressed += CurrentWeaponClass.WeaponUse;
            _player.PlayerInput.OnAttackReleased += CurrentWeaponClass.WeaponCancel;
            if (gunClass != null)
                _player.PlayerInput.OnReloadPressed += gunClass.GunReload;
            _currentWeapon.Value.gameObject.SetActive(true);
            CurrentWeaponClass.WeaponSetting();
            CurrentWeaponClass.WeaponEquip();
            
            OnWeaponChanged?.Invoke(CurrentWeaponClass.WeaponData.WeaponMoveSpeedMultifier);
        }

        public void InventoryChanged(Weapon weapon)
        {
            Weapon newWeapon = Instantiate(weapon, ping);

            if (_inventoryDict[newWeapon.WeaponData.WeaponType] != null)
            {
                Instantiate(weapon, transform.position, Quaternion.identity);
                _inventoryDict[newWeapon.WeaponData.WeaponType] = newWeapon.gameObject;
            }
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