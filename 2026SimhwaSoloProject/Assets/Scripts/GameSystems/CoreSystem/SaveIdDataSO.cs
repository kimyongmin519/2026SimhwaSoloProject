using UnityEngine;

namespace GameSystems.CoreSystem
{
    [CreateAssetMenu(fileName = "Save id", menuName = "System/Save id", order = 0)]
    public class SaveIdDataSO : ScriptableObject
    {
        [field:SerializeField] public int Id { get; private set; }
        [SerializeField, TextArea] private string description;
    }
}