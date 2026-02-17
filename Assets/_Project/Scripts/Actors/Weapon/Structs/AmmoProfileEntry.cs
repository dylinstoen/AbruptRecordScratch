using _Project.Scripts.Weapon.Enums;

namespace _Project.Scripts.Actors.Structs {
    [System.Serializable]
    public struct AmmoProfileEntry {
        public AmmoType AmmoType;
        public int StartingReserve;
        public int MaxReserve;
    }
}