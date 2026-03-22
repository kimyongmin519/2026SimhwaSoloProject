using System;
using TMPro;
using UnityEngine;

namespace UI
{
    public class DataViewText : MonoBehaviour
    {
        [SerializeField] private string description;
        [SerializeField] private TextMeshProUGUI text;

        public void SetIntText(int value)
        {
            text.SetText(description + value);
        }
            
    }
}
