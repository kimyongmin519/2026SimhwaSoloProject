using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Weapons;

namespace Core
{
    [CreateAssetMenu(fileName = "Player Input", menuName = "System/PlayerInput", order = 0)]
    public class PlayerInputSO : ScriptableObject, Controls.IPlayerActions
    {
        private Controls _controls;
        
        public Vector2 InputDirection { get; private set; }
        public Vector2 MousePos { get; private set; }
        public event Action OnAttackPressed;
        public event Action OnAttackReleased;
        public event Action OnJumpPressed;
        public event Action OnReloadPressed;
        public event Action<WeaponType> OnWeaponChanged;

        private void OnEnable()
        {
            if (_controls == null)
            {
                _controls = new Controls();
                _controls.Player.SetCallbacks(this);
            }
            _controls.Player.Enable();
        }

        private void OnDisable()
        {
            _controls.Player.Disable();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            InputDirection = context.ReadValue<Vector2>();
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnAttackPressed?.Invoke();
            else if (context.canceled)
                OnAttackReleased?.Invoke();
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
        }

        public void OnCrouch(InputAction.CallbackContext context)
        {
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnJumpPressed?.Invoke();
        }

        public void OnReload(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnReloadPressed?.Invoke();
        }

        public void OnMousePos(InputAction.CallbackContext context)
        {
            MousePos = Camera.main.ScreenToWorldPoint(context.ReadValue<Vector2>());
        }

        public void OnMainWeapon(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnWeaponChanged?.Invoke(WeaponType.MAIN_WEAPON);
        }

        public void OnSecondaryWeapon(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnWeaponChanged?.Invoke(WeaponType.SECONDARY_WEAPON);
        }

        public void OnMeleeWeapon(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnWeaponChanged?.Invoke(WeaponType.MELEE);
        }
    }
}
