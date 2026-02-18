using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Core
{
    [CreateAssetMenu(fileName = "Player Input", menuName = "System/PlayerInput", order = 0)]
    public class PlayerInputSO : ScriptableObject, Controls.IPlayerActions
    {
        private Controls _controls;
        
        public Vector2 InputDirection { get; private set; }
        public Vector2 ScreenMousePos { get; private set; }
        public event Action OnAttackPressed;
        public event Action OnAttackReleased;

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
        }

        public void OnMousePos(InputAction.CallbackContext context)
        {
            ScreenMousePos = context.ReadValue<Vector2>();
        }
    }
}
