using _Project.Scripts.Actors.Weapon;
using UnityEngine;

namespace _Project.Scripts.Actors.Structs {
    public struct WeaponDeps {
        public Transform WeaponViewMount;
        public Transform WeaponLogicMount;
        public IAmmoInventory AmmoInventory;
    }
}