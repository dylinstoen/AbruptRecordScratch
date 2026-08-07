using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
namespace _Project.Scripts.MainMenu {

    [System.Serializable]
    public sealed class SettingsData {
        // Gameplay
        public bool InvertLook = false;
        public float LookSensitivity = 1f;
        public float VerticalFOV = 60f;

        // Display
        public int ResolutionWidth;
        public int ResolutionHeight;
        public int RefreshRate;
        public bool WindowMode;

        // Audio
        public float Volume = 1f;

        // Graphics
        public bool VSync = true;
       

        public SettingsData() {
        }

        public SettingsData(SettingsData other) {
            InvertLook = other.InvertLook;
            LookSensitivity = other.LookSensitivity;
            VerticalFOV = other.VerticalFOV;

            ResolutionWidth = other.ResolutionWidth;
            ResolutionHeight = other.ResolutionHeight;
            RefreshRate = other.RefreshRate;
            WindowMode = other.WindowMode;

            Volume = other.Volume;
            VSync = other.VSync;

        }
    }

}

