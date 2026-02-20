using _Project.Scripts.Weapon.Enums;
using UnityEngine;

namespace _Project.Scripts.Actors.Structs {
    [System.Serializable]
    public struct AmmoProfileEntry {
        public AmmoType ammoType;
        [Min(0)] public int startingInventory;
        [Min(0)] public int maxInventory;
    }
}