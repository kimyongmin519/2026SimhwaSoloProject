using Agents.FSM;
using UnityEngine;
using Weapons;

namespace Agents.Players.States
{
    public class PlayerMeleeAttackState : AbstractPlayerState
    {
        private WeaponRenderer _weaponRenderer;
        private Vector2 _attackDir;
        
        public PlayerMeleeAttackState(Agent owner) : base(owner)
        {
            
        }

        public override void Enter()
        {
            base.Enter();
            _player.Handler.CurrentWeaponClass.OnWeaponUseEvent?.Invoke();
            
            _player.Mover.StopImmediately(true,true);
            _player.Mover.CanManualMovement = false;

            _weaponRenderer = _player.Handler.CurrentWeaponClass.Renderer;
            Debug.Assert(_weaponRenderer != null, "_weaponRenderer != null");

            Vector2 dir =
                (_player.PlayerInput.MousePos - (Vector2)_player.transform.position).normalized;

            _attackDir = dir;
            _weaponRenderer.OnAnimationAttackTrigger += HandleDashStart;
            _weaponRenderer.OnSpecialTrigger += HandleDashEnd;
        }

        public override void Exit()
        {
            base.Exit();
            _player.Mover.CanManualMovement = true;
            _weaponRenderer.OnAnimationAttackTrigger -= HandleDashStart;
            _weaponRenderer.OnSpecialTrigger -= HandleDashEnd;
        }
        private void HandleDashStart()
        {
            _player.Mover.AddForceToAgent(_attackDir * (_player.JumpPower * 3f));
        }
        
        private void HandleDashEnd()
        {
            _player.Mover.StopImmediately(true,true);
            _player.ChangeState(PlayerStateEnum.IDLE);
        }
    }
}