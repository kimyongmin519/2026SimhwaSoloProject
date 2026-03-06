using Systems.GameEvents.ChannelEvent;

namespace Systems.GameEvents.BusEvent
{
    public interface IBusEvent
    {
        
    }
    
    public class KimBus<T> where T : IBusEvent
    {
        public delegate void Event(T evt);
        public static event Event OnEvent;
        public static void RaiseEvent(T evt) => OnEvent?.Invoke(evt);
    }
}