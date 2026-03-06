using DG.Tweening;
using UnityEngine;

namespace Systems
{
    public class TrackingObject : MonoBehaviour
    {
        [Header("Tracking Setting")]
        [SerializeField] private float smoothPosDelay;
        [SerializeField] private float posDistance;
        [SerializeField] private float rotateDelay;
        [SerializeField] private bool canRotate;
        [SerializeField] private bool canPosMove;
        [SerializeField] private bool canFlip;
        
        private float _height;
        private float _originYSize;
        
        private Tween _tween;
        private void Awake()
        {
            _height = transform.localPosition.y;
            Debug.Log(_height);
            _originYSize = transform.localScale.y;
        }
        
        public void TrackingRotateAndPos(Vector2 targetPos)
        {
            Vector2 mouseDir = (targetPos - (Vector2)transform.position).normalized;
            float angle = Mathf.Atan2(mouseDir.y, mouseDir.x) * Mathf.Rad2Deg;
            Vector3 euler = transform.eulerAngles;
            
            if (canRotate)
            {
                transform.rotation = Quaternion.Euler(euler.x, euler.y, angle);
            }
            if (canPosMove)
            {
                _tween.Kill();
                _tween = transform.DOLocalMove(
                 (targetPos / posDistance) + (Vector2.up * _height), smoothPosDelay);
            }
            if (canFlip)
            {
                Flip(targetPos);
            }
        }

        private void Flip(Vector3 targetPos)
        {
            Vector3 scale = transform.localScale;
            Vector3 position = transform.position;
            
            if (targetPos.x > position.x)
                transform.localScale = new Vector3(scale.x, _originYSize, scale.z);
            else
                transform.localScale = new Vector3(scale.x, -_originYSize, scale.z);
        }
    }
}
