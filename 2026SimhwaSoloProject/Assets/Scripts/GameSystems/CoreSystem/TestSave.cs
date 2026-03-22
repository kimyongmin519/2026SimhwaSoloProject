using GameSystems.GameEvents.BusEvent;
using GameSystems.GameEvents.BusEvent.BusEvents;
using UnityEngine;
using UnityEngine.Events;

namespace GameSystems.CoreSystem
{
    public class TestSave : MonoBehaviour, ISaveable
    {
        public UnityEvent<int> OnChangedKillCount;
        private void Awake()
        {
            KimBus<KillEvent>.OnEvent += HandleKillCount;
        }

        private void Start()
        {
            OnChangedKillCount?.Invoke(CurrentKill);
        }

        #region 세이브 로직

        public int CurrentKill { get; private set; }
        [field:SerializeField] public SaveIdDataSO SaveId { get; set; }
        
        public struct TestSaveData
        {
            public int killCount;
        }
        
        private void HandleKillCount(KillEvent evt)
        {
            CurrentKill++;
            OnChangedKillCount?.Invoke(CurrentKill);
        }

        public string GetSaveData()
        {
            TestSaveData testSaveData = new TestSaveData
            {
                killCount = CurrentKill
            };
            return JsonUtility.ToJson(testSaveData);
        }

        public void RestoreData(string data)
        {
            TestSaveData testData = JsonUtility.FromJson<TestSaveData>(data);
            CurrentKill = testData.killCount;
            Debug.Log(CurrentKill);
        }
        #endregion

        private void OnDestroy()
        {
            KimBus<KillEvent>.OnEvent -= HandleKillCount;
        }
    }
}
