using UnityEngine;
using TMPro;

namespace _Project.Scripts.UI.Weapon {
    public class WeaponHud : MonoBehaviour, IWeaponHud {
        [SerializeField] private TMP_Text currentAmmoText;
        public void SetAmmo(int mag, int reserve) {
            currentAmmoText.text = mag.ToString() + " / " + reserve.ToString();
        }
    }
}