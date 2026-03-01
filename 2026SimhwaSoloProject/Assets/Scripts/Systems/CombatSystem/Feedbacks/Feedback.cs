using UnityEngine;

namespace Systems.CombatSystem.Feedbacks
{
    public abstract class Feedback : MonoBehaviour
    {
        public abstract void PlayFeedback();
        public virtual void StopFeedback() {}
    }
}
