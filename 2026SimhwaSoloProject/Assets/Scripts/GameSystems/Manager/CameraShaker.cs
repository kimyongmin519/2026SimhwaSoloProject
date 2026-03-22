using GameSystems.GameEvents.BusEvent;
using GameSystems.GameEvents.BusEvent.BusEvents;
using Unity.Cinemachine;
using UnityEngine;

namespace GameSystems.Manager
{
    public class CameraShaker : MonoBehaviour
    {
        private CinemachineImpulseSource _impulseSource;

        private void Awake()
        {
            _impulseSource = GetComponent<CinemachineImpulseSource>();

            KimBus<CameraShakeEvent>.OnEvent += HandleCameraShake;
        }

        private void HandleCameraShake(CameraShakeEvent evt)
        {
            _impulseSource.GenerateImpulse(evt.Magnitude);
        }
    }
}