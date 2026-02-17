using _Project.Scripts.Actors;
using UnityEngine;
namespace _Project.Scripts.Weapon.Stucts {
    public readonly struct WeaponUseContext {
        public readonly Ray AimRay;
        public readonly float Time;
        public WeaponUseContext(Ray aimRay, float time) { AimRay = aimRay; Time = time; }
    }
}