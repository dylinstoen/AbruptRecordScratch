using _Project.Scripts.Actors.Weapon;
using _Project.Scripts.Combat;
using _Project.Scripts.Gameplay;
using _Project.Scripts.UI.Reticle;
using UnityEngine;

namespace _Project.Scripts.Actors.Structs {
    public struct WeaponDeps {
        public Transform WeaponViewMount;
        public Transform WeaponLogicMount;
        public Transform ReticleMount;
        public GameObject Owner;
        public AmmoInventory AmmoInventory;
        public ICameraRecoilService CameraRecoilService;
        public IHitService HitService;
    }
}