using System;
using System.Collections;
using UnityEngine;

namespace Systems.CombatSystem.Feedbacks
{
    public class BlinkFeedback : Feedback
    {
        [SerializeField] private SpriteRenderer targetRenderer;
        [SerializeField] private float blinkDuration;
        [SerializeField] private float blinkValue;
        
        private readonly int _blinkShaderParam = Shader.PropertyToID("_BlinkValue");
        private Material _material;
        
        private WaitForSeconds _waitForSeconds;

        private void Awake()
        {
            Debug.Assert(targetRenderer != null, $"{gameObject.name}에 타겟렌더러 세팅 안댐");
            _material = targetRenderer.material;
            
            _waitForSeconds = new WaitForSeconds(blinkDuration);
        }

        public override void PlayFeedback()
        {
            _material.SetFloat(_blinkShaderParam, blinkValue);
            StartCoroutine(SetNormalMaterialValue());
        }

        private IEnumerator SetNormalMaterialValue()
        {
            yield return _waitForSeconds;
            StopFeedback();
        }

        public override void StopFeedback()
        {
            base.StopFeedback();
            StopAllCoroutines();
            _material.SetFloat(_blinkShaderParam, 0);
        }
    }
}