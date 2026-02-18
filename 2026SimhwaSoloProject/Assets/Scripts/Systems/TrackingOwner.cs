using UnityEngine;

namespace Systems
{
    public class TrackingOwner : MonoBehaviour
    {
        private TrackingObject[] _trackingChildren;
        private Vector2 _trackingPos;
        
        private void Awake()
        {
            _trackingChildren = GetComponentsInChildren<TrackingObject>();
            Debug.Assert(_trackingChildren != null, "Tracking children in null");
        }

        private void LateUpdate()
        {
            foreach (TrackingObject module in _trackingChildren)
            {
                module.TrackingRotateAndPos(_trackingPos);
            }
        }

        public void SetTrackingPos(Vector2 pos)
        {
            _trackingPos = pos;
        }

    }
}