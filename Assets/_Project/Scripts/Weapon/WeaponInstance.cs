using System;
using _Project.Scripts.Actors;
using _Project.Scripts.Weapon.Enums;
using _Project.Scripts.Weapon.Stucts;
using Object = UnityEngine.Object;

namespace _Project.Scripts.Weapon {
    public sealed class WeaponInstance : IWeapon {
        // TODO: Make a central OnAmmoChange(mag,current) that the internals of this weapon subscribe too and broadcast to this script when something changes
        // TODO: Once that gets excepted broadcast an external version for hud
        
        public AmmoType AmmoType { get; }
        public int Mag { get; }
        public int Reserve { get; }
        public event Action AmmoChanged;
        
        private readonly WeaponMotor  _motor;
        private readonly WeaponView _view;
        public WeaponInstance(WeaponMotor motor, WeaponView view)
        {
            _motor = motor;
            _view = view;
        }
        
        public void Tick(in WeaponUseContext ctx) => _motor.Tick(ctx);

        public void StartFire(in WeaponUseContext ctx) => _motor.StartFire(ctx);

        public void StopFire(in WeaponUseContext ctx) => _motor.StopFire(ctx);

        public void OnUnequip() {
            _motor.Unequip();
        } 

        public void OnEquip() {
            _motor.Equip();
        }

        public void OnCreate() {
            _motor.Create();
            AmmoChanged?.Invoke();
        }

        public void LateTick(in WeaponUseContext ctx) => _view.LateTick(ctx);

        public void SetActive(bool active) {
            _view.SetActive(active);
            _motor.SetActive(active);
        }
        public void Dispose()
        {
            _motor.Dispose();
            _view.Dispose();
            if (_view != null) Object.Destroy(_view.gameObject);
            if (_motor != null) Object.Destroy(_motor.gameObject);
        }
        
        private void HandleReserveChanged(AmmoType type, int current)
        {
            if (type != AmmoType) return;
            AmmoChanged?.Invoke();
        }
        
        

    }
}