using System;

namespace Systems.AnimationSystem
{
    public interface IAnimationTrigger
    {
        public event Action OnAnimationAttackTrigger;
        public event Action OnAnimationEndTrigger;
        public void AnimationAttackTrigger();
        public void AnimationEndTrigger();
    }
}