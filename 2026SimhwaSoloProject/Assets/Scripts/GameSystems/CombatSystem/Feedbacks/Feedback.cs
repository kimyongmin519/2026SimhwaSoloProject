using UnityEngine;

namespace GameSystems.CombatSystem.Feedbacks
{
    public abstract class Feedback : MonoBehaviour
    {
        public abstract void PlayFeedback();
        public virtual void StopFeedback() {}
    }
}
