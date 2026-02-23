using System;
using UnityEngine;

namespace _Project.Scripts.UI.DeathScreen {
    public class DeathScreen : MonoBehaviour, IDeathScreen {
        [SerializeField] private GameObject deathScreen;
        public void Show() {
            deathScreen.SetActive(true);
        }

        public void Hide() {
            deathScreen.SetActive(false);
        }
    }
}