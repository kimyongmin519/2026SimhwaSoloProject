using Core.Modules;
using UnityEngine;

namespace Systems.CombatSystem
{
    public class HealthModule : MonoBehaviour, IModule, IAfterInitModule
    {
        public delegate void HealthChange(float before, float current, float max);
        public event HealthChange OnHealthChange;
        
        private float _currentHealth;

        [field: SerializeField] public float MaxHealth { get; private set; } = 30f;
        
        private ModuleOwner _owner;
        
        public float CurrentHealth
        {
            get => _currentHealth;
            set
            {
                float before = _currentHealth;
                _currentHealth = Mathf.Clamp(value, 0f, MaxHealth);
                if (!Mathf.Approximately(before, _currentHealth))
                {
                    OnHealthChange?.Invoke(before, _currentHealth, MaxHealth);
                }
            }
        }
        public void Initialize(ModuleOwner owner)
        {
            _owner = owner;
        }

        public void AfterInitialize()
        {
            _currentHealth = MaxHealth;
        }
        
        private void HandleMaxHealthChange(float current, float previous)
        {
            float healthDifference = current - previous;
            CurrentHealth += healthDifference;
            MaxHealth = current;
            CurrentHealth = Mathf.Clamp(CurrentHealth, 0f, MaxHealth);
        }

        public void ApplyDamage(float damage)
        {
            CurrentHealth -= damage;
        }

    }
}