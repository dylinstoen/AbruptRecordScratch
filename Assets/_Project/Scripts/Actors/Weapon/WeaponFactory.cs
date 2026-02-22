using _Project.Scripts.Actors.Structs;
using _Project.Scripts.Weapon;
using _Project.Scripts.Weapon.Stucts;
using UnityEngine.Assertions;

namespace _Project.Scripts.Actors {
    using UnityEngine;
    using Weapon;
    public class WeaponFactory: IWeaponFactory {
        // Spawn the weapon visuals under the visual mount
        // Spawn the weapon logic under the logic mount
        public WeaponFacets Create(WeaponSO weaponSo, WeaponDeps weaponDeps) {
            Assert.IsNotNull(weaponSo.motorPrefab);
            Assert.IsNotNull(weaponSo.viewPrefab);
            Assert.IsNotNull(weaponSo.fireMode);
            Assert.IsNotNull(weaponSo.emitterMode);
            Assert.IsNotNull(weaponDeps.WeaponLogicMount);
            Assert.IsNotNull(weaponDeps.WeaponViewMount); ;
            
            var view = Object.Instantiate(weaponSo.viewPrefab, weaponDeps.WeaponViewMount);
            var weaponView = view.GetComponent<WeaponView>();
            
            IWeaponMagazine weaponMagazine = new WeaponMagazine(weaponSo.magSize);
            IReloadPolicy reloadPolicy = new ReloadPolicy(weaponDeps.AmmoInventory, weaponMagazine, weaponSo.ammoType, weaponSo.reloadDuration);
            var reloadStateView = new WeaponReloadViewBridge(reloadPolicy);
            IEmitterMode emitterMode = weaponSo.emitterMode.Create();
            var fireMode = weaponSo.fireMode.Create(weaponMagazine, emitterMode, weaponSo.costPerShot);
            var controller = new WeaponStateController(fireMode, reloadPolicy);
            
            var motor = Object.Instantiate(weaponSo.motorPrefab, weaponDeps.WeaponLogicMount).GetComponent<WeaponMotor>();
            motor.transform.localPosition = Vector3.zero;
            motor.transform.localRotation = Quaternion.identity;
            motor.Initialize(controller);
            
            view.transform.localPosition = Vector3.zero;
            view.transform.localRotation = Quaternion.identity;
            weaponView.Initialize(reloadStateView);
            WeaponInstance weaponInstance = new WeaponInstance(weaponSo.iD, weaponSo.ammoType, motor, weaponView, weaponDeps.AmmoInventory, weaponMagazine);
            WeaponFacets weaponFacets = new WeaponFacets(weaponInstance); 
            return weaponFacets;
        }
    }
}