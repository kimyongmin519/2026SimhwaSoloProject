using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Weapons.Guns;

namespace UI.InGame
{
    public class GunDescriptionUI : MonoBehaviour
    {
        [field:SerializeField] public GunDataSO GunData { get; private set; }

        [SerializeField] private Image gunImage;
        [SerializeField] private TextMeshProUGUI gunNameText;
        [SerializeField] private TextMeshProUGUI gunDamageText;
        [SerializeField] private TextMeshProUGUI gunHeadShotText;
        [SerializeField] private TextMeshProUGUI gunFireRateText;

        private void OnValidate()
        {
            if (GunData != null)
            {
                gunImage.sprite = GunData.WeaponImage;
                gunNameText.SetText(GunData.WeaponPrefab.name);
                gunDamageText.SetText($"Damage:\n{GunData.Damage}");
                gunHeadShotText.SetText($"Headshot damage:\n{GunData.HeadShotDamage}");
                gunFireRateText.SetText($"Fire rate:\n{GunData.ShotAnimClip.length:F2}");
            }
        }
    }
}
