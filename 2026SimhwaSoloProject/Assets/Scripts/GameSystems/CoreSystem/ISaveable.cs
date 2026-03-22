namespace GameSystems.CoreSystem
{
    public interface ISaveable
    {
        SaveIdDataSO SaveId { get; }
        string GetSaveData();
        void RestoreData(string data);
    }
}