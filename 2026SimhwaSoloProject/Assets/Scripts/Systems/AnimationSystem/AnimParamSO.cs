using System;
using UnityEngine;

namespace Systems.AnimationSystem
{
    [CreateAssetMenu(fileName = "Anim param", menuName = "Anim/Anim param", order = 15)]
    public class AnimParamSO : ScriptableObject
    {
        [field: SerializeField] public string ParamName { get; private set; }
        [field: SerializeField] public int HashValue { get; private set; }

        private void OnValidate()
        {
            HashValue = Animator.StringToHash(ParamName);
        }
    }
}
