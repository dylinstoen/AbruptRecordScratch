using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.MainMenu {
    public sealed class RefreshRateSelector :
        MonoBehaviour,
        ISettingsControl {

        [SerializeField] private TMP_Text refreshRateText;

        private readonly List<int> _refreshRates = new();

        private SettingsSession _session;
        private int _currentIndex;

        public void Initialize(SettingsSession session) {
            _session = session;
            _session.ResolutionChanged += HandleResolutionChanged;

            RebuildRefreshRates();
        }

        private void OnDestroy() {
            if (_session != null) {
                _session.ResolutionChanged -= HandleResolutionChanged;
            }
        }

        public void Previous() {
            if (_refreshRates.Count == 0) {
                return;
            }

            _currentIndex =
                (_currentIndex - 1 + _refreshRates.Count) %
                _refreshRates.Count;

            ApplyPendingRefreshRate();
        }

        public void Next() {
            
            if (_refreshRates.Count == 0) {
                return;
            }

            _currentIndex = (_currentIndex + 1) % _refreshRates.Count;

            ApplyPendingRefreshRate();
        }

        public void Refresh() {
            RebuildRefreshRates();
        }

        private void HandleResolutionChanged() {
            RebuildRefreshRates();
        }

        private void RebuildRefreshRates() {
            _refreshRates.Clear();

            int selectedWidth = _session.WorkingCopy.ResolutionWidth;

            int selectedHeight = _session.WorkingCopy.ResolutionHeight;

            foreach (Resolution resolution in Screen.resolutions) {
                if (resolution.width != selectedWidth ||
                    resolution.height != selectedHeight) {
                    continue;
                }

                int refreshRate = Mathf.RoundToInt(
                    (float)resolution.refreshRateRatio.value
                );

                if (!_refreshRates.Contains(refreshRate)) {
                    _refreshRates.Add(refreshRate);
                }
            }

            _refreshRates.Sort();

            FindCurrentRefreshRate();
            EnsureValidRefreshRate();
            RefreshText();
        }

        private void FindCurrentRefreshRate() {
            int selectedRefreshRate =
                _session.WorkingCopy.RefreshRate;

            int index = _refreshRates.IndexOf(selectedRefreshRate);

            _currentIndex = index >= 0
                ? index
                : Mathf.Max(0, _refreshRates.Count - 1);
        }

        private void EnsureValidRefreshRate() {
            if (_refreshRates.Count == 0) {
                return;
            }

            int refreshRate = _refreshRates[_currentIndex];

            if (_session.WorkingCopy.RefreshRate != refreshRate) {
                _session.SetRefreshRate(refreshRate);
            }
        }

        private void ApplyPendingRefreshRate() {
            int refreshRate = _refreshRates[_currentIndex];
            _session.SetRefreshRate(refreshRate);

            RefreshText();
        }

        private void RefreshText() {
            if (_refreshRates.Count == 0) {
                refreshRateText.text = "Default";
                return;
            }

            refreshRateText.text =
                $"{_refreshRates[_currentIndex]} Hz";
        }
    }
}