using System;
using _Project.Scripts.Actors;
using UnityEngine;
using TMPro;
using IDisposable = _Project.Scripts.Actors.IDisposable;

namespace _Project.Scripts.UI {
    public class WeaponHud : MonoBehaviour, IDisposable {
        [SerializeField] private TMP_Text currentAmmoText;
        
        private IAmmoEvents _ammoEvents;

        public void BindAmmoEvents(IAmmoEvents ammoEvents) {
            _ammoEvents = ammoEvents;
            _ammoEvents.OnAmmoChanged += SetAmmo;
            ammoEvents.Refresh();
        }
        
        public void SetAmmo(int mag, int reserve, bool isInfinite) {
            if (isInfinite) {
                currentAmmoText.text = "∞";
            }
            else {
                currentAmmoText.text = mag.ToString() + " / " + reserve.ToString();
            }
            
        }

        private void OnDestroy() {
            Dispose();
        }

        public void Dispose() {
            _ammoEvents.OnAmmoChanged -= SetAmmo;
        }
    }
}