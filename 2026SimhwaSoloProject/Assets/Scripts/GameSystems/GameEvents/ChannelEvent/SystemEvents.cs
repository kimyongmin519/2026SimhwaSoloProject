using _06.GameLib.EventSystem;

namespace GameSystems.GameEvents.ChannelEvent
{
    public class SystemEvents
    {
        public static readonly SavePrefEvent SavePref = new SavePrefEvent();
        public static readonly LoadPrefEvent LoadPref = new LoadPrefEvent();
    }
    
    public class SavePrefEvent : GameEvent { }
    public class LoadPrefEvent : GameEvent { }
}