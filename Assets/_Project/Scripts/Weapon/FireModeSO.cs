using _Project.Scripts.Weapon.Stucts;
using UnityEngine;

namespace _Project.Scripts.Weapon {
    public abstract class FireModeSO : ScriptableObject {
        public abstract IFireMode Create(IWeaponAmmo weaponAmmo, IEmitterMode emitterMode);
    }
}