using _06.GameLib.EventSystem;
using GameSystems.GameEvents.ChannelEvent;
using UnityEngine;

namespace GameSystems.CoreSystem
{
    [DefaultExecutionOrder(-20)]
    public class FirstLoader : MonoBehaviour
    {
        [field: SerializeField] public EventChannelSO PlayerChannel { get; private set; }
        [field: SerializeField] public EventChannelSO SystemChannel { get; private set; }
        private void Start()
        {
            SystemChannel.RaiseEvent(SystemEvents.LoadPref);
        }
    }
}