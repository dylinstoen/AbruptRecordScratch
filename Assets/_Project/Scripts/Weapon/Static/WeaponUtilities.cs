using System;
using _Project.Scripts.Actors.Structs;
using _Project.Scripts.Weapon.Stucts;
using Unity.Cinemachine;
using UnityEngine;

namespace _Project.Scripts.Weapon.Static {
    public static class WeaponUtilities {
        public static SceneParts SpawnScene(WeaponSO so, WeaponDeps deps) {
            var reticleGo = InstantiateAndMount(so.reticlePrefab, deps.ReticleMount, "Reticle");
            var viewGo = InstantiateAndMount(so.viewPrefab, deps.WeaponViewMount, "View");
            var view = GetRequiredComponent<WeaponView>(viewGo, "ViewPrefab");
            var motorGo = InstantiateAndMount(so.motorPrefab, deps.WeaponLogicMount, "Motor");
            var motor = GetRequiredComponent<WeaponMotor>(motorGo, "MotorPrefab");
            return new SceneParts(reticleGo, view, motor);
        }
        
        public static LogicParts BuildLogic(WeaponSO so, WeaponDeps deps) {
            IWeaponMagazine mag = new WeaponMagazine(so.magSize, so.costPerShot);
            IReloadPolicy reload = new ReloadPolicy(deps.AmmoInventory, mag, so.ammoType, so.reloadDuration, deps.AudioService, so.reloadSfx);
            var reloadBridge = new WeaponReloadBridge(reload);
            IEmitterMode emitter = so.emitterMode.Create(deps.ImpactService, so.damage, deps.Owner, so.sourceVisualImpactProfile, so.hitLayerMask);
            var fireMode = deps.ImpulseSource ? 
                so.fireMode.Create(mag, emitter, deps.AudioService, so.gunShotSfx, so.costPerShot, so.fireRate, so.spread, deps.ImpulseSource, so.recoilProfile) : 
                so.fireMode.Create(mag, emitter, deps.AudioService, so.gunShotSfx, so.costPerShot, so.fireRate, so.spread);
            var controller = new WeaponStateController(fireMode, reload);
            return new LogicParts(mag, reload, reloadBridge, controller, fireMode);
        }
        
        private static T GetRequiredComponent<T>(GameObject go, string label) where T : Component {
            if (go.TryGetComponent<T>(out var c)) return c;
            throw new MissingComponentException($"[{label}] Missing required component {typeof(T).Name} on '{go.name}'.");
        }
        private static GameObject InstantiateAndMount(GameObject prefab, Transform parent, string label) {
            if (!prefab) throw new ArgumentNullException(nameof(prefab), $"[{label}] Prefab is null.");
            var go = UnityEngine.Object.Instantiate(prefab, parent);
            go.transform.localPosition = Vector3.zero;
            go.transform.localRotation = Quaternion.identity;
            go.transform.localScale = Vector3.one;
            return go;
        }
        
    }
}