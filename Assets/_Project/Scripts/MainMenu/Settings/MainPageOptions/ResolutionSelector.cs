using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.MainMenu {
    public class ResolutionSelector : MonoBehaviour, ISettingsControl {
        [SerializeField] private TMP_Text resolutionText;
        private SettingsSession _session;
        private List<Resolution> _resolutions;
        private int _currentIndex;

        public void Initialize(SettingsSession session) {
            _session = session;
            CacheResolution();
            FindCurrentResolution();
            RefreshText();
        }

        public void Previous() {
            if (_resolutions.Count == 0)
                return;

            _currentIndex = (_currentIndex - 1 + _resolutions.Count) % _resolutions.Count;
            ApplyPendingResolution();
        }

        public void Next() {
            if (_resolutions.Count == 0)
                return;

            _currentIndex = (_currentIndex + 1) % _resolutions.Count;
            ApplyPendingResolution();
        }

        private void ApplyPendingResolution() {
            Resolution resolution = _resolutions[_currentIndex];
            _session.SetResolution(resolution.width, resolution.height);

            RefreshText();
        }

        private void RefreshText() {
            Resolution resolution = _resolutions[_currentIndex];
            resolutionText.text = $"{resolution.width} x {resolution.height}";
        }

        private void FindCurrentResolution() {
            int width = _session.WorkingCopy.ResolutionWidth;
            int height = _session.WorkingCopy.ResolutionHeight;

            for (int i = 0; i < _resolutions.Count; i++) {
                Resolution r = _resolutions[i];

                if (r.width == width &&
                    r.height == height) {
                    _currentIndex = i;
                    return;
                }
            }

            _currentIndex = 0;
        }

        public void CacheResolution() {
            _resolutions = new List<Resolution>();
            foreach(Resolution resolution in Screen.resolutions) {
                bool _alreadyExists = false;
                foreach(Resolution existing in _resolutions) {
                    if (existing.width == resolution.width && existing.height == resolution.height) {
                        _alreadyExists = true;
                        break;
                    }
                }
                if(!_alreadyExists) {
                    _resolutions.Add(resolution);
                }
            }
        }

        public void Refresh() {
            FindCurrentResolution();
            RefreshText();
        }
    }

}


