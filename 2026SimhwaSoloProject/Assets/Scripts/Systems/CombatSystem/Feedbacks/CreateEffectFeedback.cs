
using Systems.AnimationSystem;
using Systems.GameEvents;
using UnityEngine;

namespace Systems.CombatSystem.Feedbacks
{
    public class CreateEffectFeedback : Feedback
    {
        [field:SerializeField] public EventChannelSO CreateChannel { get; private set; }

        [SerializeField] private AnimParamSO effectParam;
        [SerializeField] private float effectDuration;
        [SerializeField] private bool isPoolingEffect = true;
        [SerializeField] private float randomOffset;
        public override void PlayFeedback()
        {
            
        }
    }
}