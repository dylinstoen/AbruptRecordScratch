using System;
using _Project.Scripts.Actors;
using _Project.Scripts.UI;
using TMPro;
using UnityEngine;
using IDisposable = _Project.Scripts.Actors.IDisposable;

namespace _Project.Scripts.UI {
    public class HealthHud : MonoBehaviour, IDisposable {
        [SerializeField] private TMP_Text currentHealthText;
        private IHealthEvents _playerHealthEvents;

        public void BindHealthEvents(IHealthEvents healthEvents) {
            _playerHealthEvents = healthEvents;
            _playerHealthEvents.HealthChanged += OnHealthChanged;
        }

        public void OnHealthChanged(int current, int max) {
            currentHealthText.text = $"{current}/{max}";
        }

        private void OnDestroy() {
            Dispose();
        }

        public void Dispose() {
            _playerHealthEvents.HealthChanged -= OnHealthChanged;
        }
    }
}