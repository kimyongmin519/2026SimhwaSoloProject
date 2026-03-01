using Agents.Players;
using Core;
using Core.Modules;
using UnityEngine;

namespace Weapon
{
    public class PlayerWeaponHandler : WeaponHandler
    {
        private Player _player;
        [SerializeField] private WeaponDataSO testWeaponData;
        [SerializeField] private PlayerInputSO playerInput;

        private Weapon _currentWeaponClass;

        [SerializeField] private WeaponType startWeaponType;

        private bool _completeStartSetting = false;

        public override void Initialize(ModuleOwner owner)
        {
            base.Initialize(owner);
            _player = owner as Player;
            Debug.Assert(_player != null, "owner not casting player");

            _player.PlayerInput.OnWeaponChanged += ChangeWeapon;

            _currentWeapon.Value = _inventoryDict[startWeaponType];
            _currentWeaponClass = _currentWeapon.Value.GetComponent<Weapon>();
            Debug.Assert(_currentWeaponClass != null, $"{gameObject.name} Start weapon is null");

            ChangeWeapon(startWeaponType);
        }

        public void ChangeWeapon(WeaponType type)
        {
            if (type == _currentWeaponClass.WeaponData.WeaponType && _completeStartSetting) return;
            
            _completeStartSetting = true;
            
            _currentWeaponClass = _currentWeapon.Value.GetComponent<Weapon>();
            
            Debug.Assert(_inventoryDict[type] != null, "weapon is null");
            _player.PlayerInput.OnAttackPressed -= _currentWeaponClass.WeaponUse;
            _player.PlayerInput.OnAttackReleased -= _currentWeaponClass.WeaponCancel;
            _currentWeapon.Value.gameObject.SetActive(false);

            _currentWeapon.Value = _inventoryDict[type];
            Debug.Assert(_currentWeapon.Value != null, "Change weapon is null");

            _currentWeaponClass = _currentWeapon.Value.GetComponent<Weapon>();
            
            _player.PlayerInput.OnAttackPressed += _currentWeaponClass.WeaponUse;
            _player.PlayerInput.OnAttackReleased += _currentWeaponClass.WeaponCancel;
            _currentWeapon.Value.gameObject.SetActive(true);
            _currentWeaponClass.WeaponSetting();
            _currentWeaponClass.WeaponEquip();
        }

        private void Update()
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _currentWeaponClass?.SetTarget(mousePos);
        }

        private void OnDestroy()
        {
            _player.PlayerInput.OnWeaponChanged -= ChangeWeapon;
        }

    }
}