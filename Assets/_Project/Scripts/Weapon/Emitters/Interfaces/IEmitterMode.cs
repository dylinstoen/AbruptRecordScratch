using _Project.Scripts.Weapon.Stucts;
using UnityEngine;

namespace _Project.Scripts.Weapon {
    public interface IEmitterMode {
        void Fire(Ray ray);
    }
}