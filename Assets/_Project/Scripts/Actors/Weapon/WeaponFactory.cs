using System;
using _Project.Scripts.Actors.Structs;
using _Project.Scripts.Weapon;
using _Project.Scripts.Weapon.Static;
using _Project.Scripts.Weapon.Stucts;
using UnityEngine.Assertions;

namespace _Project.Scripts.Actors {
    using UnityEngine;
    using Weapon;
    public class WeaponFactory: IWeaponFactory {
        // Spawn the weapon visuals under the visual mount
        // Spawn the weapon logic under the logic mount
        public WeaponFacets Create(WeaponSO so, WeaponDeps deps) {
            if (!so) throw new ArgumentNullException(nameof(so));
            var scene = Utilities.SpawnScene(so, deps);
            var logic = Utilities.BuildLogic(so, deps);
            scene.Motor.Initialize(logic.Controller);
            scene.View.Initialize(logic.ReloadBridge, logic.FireMode, so.fireRate);
            var instance = new WeaponInstance(
                so.iD,
                so.ammoType,
                scene.Motor,
                scene.View,
                scene.Reticle,
                deps.AmmoInventory,
                logic.Magazine,
                deps.CameraRecoilService,
                so.recoil
            );
            return new WeaponFacets(instance);
        }
    }
}