using System;
using _Project.Scripts.Actors.Structs;
using _Project.Scripts.Weapon;
using _Project.Scripts.Weapon.Static;
using _Project.Scripts.Weapon.Stucts;
using Unity.Cinemachine;
using UnityEngine.Assertions;

namespace _Project.Scripts.Actors {
    using UnityEngine;
    using Weapon;
    public class WeaponFactory: IWeaponFactory {
        // Spawn the weapon visuals under the visual mount
        // Spawn the weapon logic under the logic mount
        public WeaponFacets Create(WeaponSO so, WeaponDeps deps) {
            if (!so) throw new ArgumentNullException(nameof(so));
            var scene = WeaponUtilities.SpawnScene(so, deps);
            var recoil = scene.Motor.GetComponent<CinemachineImpulseSource>();
            if(recoil) deps.ImpulseSource = recoil;
            var logic = WeaponUtilities.BuildLogic(so, deps);
            scene.Motor.Initialize(logic.Controller);
            scene.View.Initialize(logic.ReloadBridge, logic.FireMode, so.fireRate);
            var instance = new WeaponInstance(
                so.iD,
                so.ammoType,
                scene.Motor,
                scene.View,
                scene.Reticle,
                deps.AmmoInventory,
                logic.Magazine
            );
            return new WeaponFacets(instance);
        }
    }
}