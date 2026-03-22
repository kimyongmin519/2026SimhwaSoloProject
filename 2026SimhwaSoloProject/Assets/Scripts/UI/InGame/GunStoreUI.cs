using System;
using _06.GameLib.EventSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Weapons.Guns;

namespace UI.InGame
{
    public class GunStoreUI : MonoBehaviour
    {
        [SerializeField] private GunDataSO gunData;

        [SerializeField] private Image gunImage;
        [SerializeField] private TextMeshProUGUI gunNameText;
        [SerializeField] private TextMeshProUGUI gunPriceText;
        
        [SerializeField] private EventChannelSO uiChannel;

        private void OnValidate()
        {
            if (gunData != null)
            {
                gunImage.sprite = gunData.WeaponImage;
                gunNameText.SetText(gunData.WeaponPrefab.name);
                if (gunData.GunPrice <= 0)
                    gunPriceText.SetText("FreeGun!");
                else
                    gunPriceText.SetText($"{gunData.GunPrice}$");
            }
        }
    }
}
