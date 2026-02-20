using _Project.Scripts.Actors;
using UnityEngine;
namespace _Project.Scripts.Weapon.Stucts {
    public readonly struct WeaponUseContext {
        public readonly Ray AimRay;
        public readonly float DeltaTime;
        public WeaponUseContext(Ray aimRay, float deltaTime) { AimRay = aimRay; DeltaTime = deltaTime; }
    }
}