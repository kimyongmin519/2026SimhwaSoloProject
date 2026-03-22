using System;
using System.Collections.Generic;
using System.Linq;
using _06.GameLib.EventSystem;
using GameSystems.CoreSystem;
using GameSystems.GameEvents.ChannelEvent;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameSystems.Manager
{
    public class DataManager : MonoBehaviour
    {
        [Serializable]
        public struct SaveData
        {
            public int Id;
            public string Data;
        }
        [Serializable]
        public struct DataCollection
        {
            public List<SaveData> Collection;
        }

        [SerializeField] private string prefKey = "saveData";
        
        private List<SaveData> _unUsedData = new List<SaveData>();
        [field:SerializeField] public EventChannelSO SystemChannel { get; private set; }

        private void Awake()
        {
            SystemChannel.AddListener<SavePrefEvent>(HandleSavePrefEvent);
            SystemChannel.AddListener<LoadPrefEvent>(HandleLoadPrefEvent);
        }

        private void Update()
        {
            if (Keyboard.current.pKey.wasPressedThisFrame)
            {
                SystemChannel.RaiseEvent(SystemEvents.SavePref);
            }
        }

        private void OnDestroy()
        {
            SystemChannel.RemoveListener<SavePrefEvent>(HandleSavePrefEvent);
            SystemChannel.RemoveListener<LoadPrefEvent>(HandleLoadPrefEvent);
        }

        #region 세이브 로직
        private string GetSceneSaveData()
        {
            IEnumerable<ISaveable> saveableObjects =
                FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<ISaveable>();
            
            List<SaveData> toSaveData = new List<SaveData>();
            foreach (ISaveable saveable in saveableObjects)
            {
                toSaveData.Add(new SaveData{Id = saveable.SaveId.Id, Data = saveable.GetSaveData()});
            }
            toSaveData.AddRange(_unUsedData);
            DataCollection dataCollection = new DataCollection{Collection = toSaveData};

            Debug.Log(dataCollection);
            return JsonUtility.ToJson(dataCollection);
        }

        private void HandleSavePrefEvent(SavePrefEvent evt)
        {
            string saveData = GetSceneSaveData();
            PlayerPrefs.SetString(prefKey, saveData);
            Debug.Log($"Save Data: {saveData}");
        }
        #endregion

        #region 로드 로직
        private void HandleLoadPrefEvent(LoadPrefEvent obj)
        {
            string loadJson = PlayerPrefs.GetString(prefKey, string.Empty);
            RestoreData(loadJson);
        }

        private void RestoreData(string json)
        {
            IEnumerable<ISaveable> saveables = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<ISaveable>();
            DataCollection parsedData = string.IsNullOrEmpty(json)
                ? new DataCollection() : JsonUtility.FromJson<DataCollection>(json);
            
            _unUsedData.Clear();

            if (parsedData.Collection != null)
            {
                foreach (SaveData saveData in parsedData.Collection)
                {
                    ISaveable saveable = saveables.FirstOrDefault(s => s.SaveId.Id == saveData.Id);
                    if (saveable != null)
                    {
                        saveable.RestoreData(saveData.Data);
                    }
                    else
                    {
                        _unUsedData.Add(saveData);
                    }
                }
            }
        }

        #endregion

        [ContextMenu("Clear Pref Data")]
        public void ClearPrefData()
        {
            PlayerPrefs.DeleteKey(prefKey);
        }
    }
}