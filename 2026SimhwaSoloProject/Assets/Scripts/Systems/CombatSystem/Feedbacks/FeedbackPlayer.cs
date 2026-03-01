using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Systems.CombatSystem.Feedbacks
{
    public class FeedbackPlayer : MonoBehaviour
    {
        private List<Feedback> _feedbacks;

        private void Awake()
        {
            _feedbacks = GetComponents<Feedback>().ToList();
        }

        public void PlayerAllFeedbacks()
        {
            StopAllFeedbacks();
            _feedbacks.ForEach(feedback => feedback.PlayFeedback());
        }

        private void StopAllFeedbacks()
        {
            _feedbacks.ForEach(feedback => feedback.StopFeedback());
        }
    }
}