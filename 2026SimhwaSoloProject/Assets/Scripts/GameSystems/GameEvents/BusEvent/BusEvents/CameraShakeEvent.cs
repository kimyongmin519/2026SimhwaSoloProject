namespace GameSystems.GameEvents.BusEvent.BusEvents
{
    public struct CameraShakeEvent : IBusEvent
    {
        public float Duration { get; set; }
        public float Magnitude { get; set; }
        
        public CameraShakeEvent(float shakeMagnitude, float shakeDuration = 0.1f)
        {
            Duration = shakeDuration;
            Magnitude = shakeMagnitude;
        }
    }
}