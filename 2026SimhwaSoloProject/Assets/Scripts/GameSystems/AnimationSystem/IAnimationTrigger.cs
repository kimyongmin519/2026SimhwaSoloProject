using System;

namespace GameSystems.AnimationSystem
{
    public interface IAnimationTrigger
    {
        public event Action OnAnimationAttackTrigger;
        public event Action OnAnimationEndTrigger;
        public void AnimationAttackTrigger();
        public void AnimationEndTrigger();
    }
}