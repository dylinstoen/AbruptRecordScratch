using _Project.Scripts.UI.Weapon;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.Actors {
    public struct PlayerDeps {
        public CameraRig CameraRig;
        public PlayerConfigSO PlayerConfigSo;
        public Transform WeaponViewMount;
        public WeaponHud WeaponHud;
    }
}