using System;
using _06.GameLib.EventSystem;
using GameSystems.GameEvents.ChannelEvent;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.InGame
{
    public class GunUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI currentAmmoText;
        [SerializeField] private TextMeshProUGUI leftAmmoText;
        [SerializeField] private Image gunImage;
        [SerializeField] private EventChannelSO playerUIChannel;

        private void Awake()
        {
            playerUIChannel.AddListener<PlayerInventoryChange>(HandleGunImageChange);
            playerUIChannel.AddListener<GunValueChanged>(HandleGunValueChange);
        }
        private void OnDestroy()
        {
            playerUIChannel.RemoveListener<PlayerInventoryChange>(HandleGunImageChange);
        }

        private void HandleGunImageChange(PlayerInventoryChange evt)
        {
            gunImage.sprite = evt.Weapon.WeaponData.WeaponImage;
        }
        private void HandleGunValueChange(GunValueChanged evt)
        {
            currentAmmoText.SetText(evt.CurrentAmmo.ToString());
            leftAmmoText.SetText(evt.LeftAmmo.ToString());
        }
        

    }
}
