using System;
using Core.Modules;
using Systems.CombatSystem;

namespace Agents
{
    public class Agent : ModuleOwner
    {
        public HealthModule HealthModule { get; private set; }

        protected override void InitializeComponents()
        {
            base.InitializeComponents();
            HealthModule = GetModule<HealthModule>();
        }

        protected virtual void Start()
        {
            
        }

        public void TakeDamage(float damage)
        {
            HealthModule.ApplyDamage(damage);
        }
    }
}
