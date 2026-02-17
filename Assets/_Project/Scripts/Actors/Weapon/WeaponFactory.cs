using _Project.Scripts.Actors.Structs;
using _Project.Scripts.Weapon;
using UnityEngine.Assertions;

namespace _Project.Scripts.Actors {
    using UnityEngine;
    using Weapon;
    public class WeaponFactory: IWeaponFactory {
        public IWeapon Create(WeaponDefinition weaponDefinition, IAmmoInventory ammoInventory, WeaponDeps weaponDeps) {
            // Spawn the weapon visuals under the visual mount
            // Spawn the weapon logic under the logic mount
            Assert.IsNotNull(weaponDefinition.motorPrefab);
            Assert.IsNotNull(weaponDefinition.viewPrefab);
            Assert.IsNotNull(weaponDefinition.fireMode);
            Assert.IsNotNull(weaponDefinition.emitterMode);
            Assert.IsNotNull(weaponDeps.WeaponLogicMount);
            Assert.IsNotNull(weaponDeps.WeaponViewMount); ;
            IEmitterMode emitterMode = weaponDefinition.emitterMode.Create();
            IFireMode fireMode = weaponDefinition.fireMode.Create(new WeaponAmmo(ammoInventory, weaponDefinition.magSize, weaponDefinition.ammoType), emitterMode); // TODO: Make an upper layer ActorWeapons class that coordinates everything
            var motor = Object.Instantiate(weaponDefinition.motorPrefab, weaponDeps.WeaponLogicMount).GetComponent<WeaponMotor>();
            motor.transform.localPosition = Vector3.zero;
            motor.transform.localRotation = Quaternion.identity;
            motor.Initialize(fireMode);
            var view = Object.Instantiate(weaponDefinition.viewPrefab, weaponDeps.WeaponViewMount).GetComponent<WeaponView>();
            view.transform.localPosition = Vector3.zero;
            view.transform.localRotation = Quaternion.identity;
            view.Initialize(weaponDeps.AimRaySource);
            return new WeaponInstance(motor, view);
        }
    }
}