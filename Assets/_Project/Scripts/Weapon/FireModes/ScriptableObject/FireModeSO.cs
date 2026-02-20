using _Project.Scripts.Weapon.Stucts;
using UnityEngine;

namespace _Project.Scripts.Weapon {
    public abstract class FireModeSO : ScriptableObject {
        public abstract IFireMode Create(IWeaponMagazine weaponMagazine, IEmitterMode emitterMode, int costPerShot);
    }
}