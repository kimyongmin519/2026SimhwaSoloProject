using System;
using Core.Modules;
using UnityEngine;

namespace Agents
{
    public class AgentMover : MonoBehaviour, IModule, IMover
    {
        [Header("Agent Values")]
        [SerializeField] private float moveSpeed = 6f;
        [SerializeField] private LayerMask whatIsGround;
        [SerializeField] private Vector2 groundCheckSize;
        
        private Agent _owner;
        private Rigidbody2D _rigidbody;

        private float _movementX;
        private float _moveSpeedMultiplier;
        private float _originalGravity;

        public bool IsGrounded { get; private set; }
        public event Action<bool> OnGroundStatusChange; 
        public event Action<Vector2> OnVelocityChange;
        public bool CanManualMovement { get; private set; } = true;
        
        public void Initialize(ModuleOwner owner)
        {
            _owner = owner as Agent;
            _rigidbody = owner.GetComponent<Rigidbody2D>();
            
            _originalGravity = _rigidbody.gravityScale;
            _moveSpeedMultiplier = 1f;
        }

        public void SetMoveSpeedMultiplier(float value) => _moveSpeedMultiplier = value;
        public void SetGravityScale(float value) => _rigidbody.gravityScale = value;
        
        public void AddForceToAgent(Vector2 force) => _rigidbody.AddForce(force, ForceMode2D.Impulse);

        public void StopImmediately(bool xAxis, bool yAxis)
        {
            if (xAxis)
            {
                _rigidbody.linearVelocityX = 0;
                _movementX = 0;
            }
            if (yAxis)
                _rigidbody.linearVelocityY = 0;
        }
        
        public void SetMovement(float value) => _movementX = value;

        private void FixedUpdate()
        {
            CheckGround();
            MoveCharactor();
        }

        private void MoveCharactor()
        {
            bool before = IsGrounded;
            IsGrounded = Physics2D.OverlapBox(transform.position, groundCheckSize, 0, whatIsGround);
            
            if (before != IsGrounded)
                OnGroundStatusChange?.Invoke(IsGrounded);
        }

        private void CheckGround()
        {
            if (CanManualMovement)
            {
                _rigidbody.linearVelocityX = _movementX * moveSpeed * _moveSpeedMultiplier;
            }
            OnVelocityChange?.Invoke(_rigidbody.linearVelocity);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, groundCheckSize);
        }
    }
}