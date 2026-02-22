using System;
using _Project.Scripts.Actors;
using _Project.Scripts.Weapon.Enums;
using _Project.Scripts.Weapon.Stucts;
using IDisposable = _Project.Scripts.Actors.IDisposable;
using Object = UnityEngine.Object;

namespace _Project.Scripts.Weapon {
    public sealed class WeaponInstance : IWeaponLogic, IWeaponAmmoView, IWeaponIdentity, IDisposable, IEquipable {
        // TODO: Make a central OnAmmoChange(mag,current) that the internals of this weapon subscribe too and broadcast to this script when something changes
        // TODO: Once that gets excepted broadcast an external version for hud
        public string ID { get; }
        public AmmoType AmmoType { get; }
        public int Mag => _weaponMagazine.CurrentAmmo;
        public int Reserve => _ammoInventory.GetCurrent(AmmoType);
        public event Action AmmoChanged;
        
        private readonly WeaponMotor  _motor;
        private readonly WeaponView _view;
        private readonly IAmmoInventory _ammoInventory;
        private readonly IWeaponMagazine _weaponMagazine; 
        public WeaponInstance(string iD, AmmoType ammoType, WeaponMotor motor, WeaponView view, IAmmoInventory ammoInventory, IWeaponMagazine weaponMagazine) {
            AmmoType = ammoType;
            _weaponMagazine = weaponMagazine;
            _ammoInventory = ammoInventory;
            _motor = motor;
            _view = view;
            ID = iD;
        }
        
        public void Tick(in WeaponUseContext ctx) => _motor.Tick(ctx);

        public void StartFire(in WeaponUseContext ctx) => _motor.StartFire(ctx);

        public void StopFire(in WeaponUseContext ctx) => _motor.StopFire(ctx);

        public void Unequip() {
            _motor.Unequip();
            _motor.SetActive(false);
            _view.SetActive(false);
        } 

        public void Equip() {
            _motor.SetActive(true);
            _view.SetActive(true);
            _motor.Equip();
            AmmoChanged?.Invoke();
        }

        public void FirstEquipped() {
            _weaponMagazine.OnMagazineChange += HandleMagazineChanged;
            _ammoInventory.OnCurrentAmmoChange += HandleReserveChanged;
            _motor.FirstEquipped();
            _motor.SetActive(true);
            _view.SetActive(true);
            AmmoChanged?.Invoke();
        }

        public void LateTick(in WeaponUseContext ctx) => _view.LateTick(ctx);


        public void Dispose()
        {
            _motor.Dispose();
            _view.Dispose();
            _weaponMagazine.OnMagazineChange -= HandleMagazineChanged;
            _ammoInventory.OnCurrentAmmoChange -= HandleReserveChanged;
            if (_view != null) Object.Destroy(_view.gameObject);
            if (_motor != null) Object.Destroy(_motor.gameObject);
        }

        private void HandleMagazineChanged() => AmmoChanged?.Invoke();
        
        private void HandleReserveChanged(AmmoType type, int current) {
            if (type != AmmoType) return;
            AmmoChanged?.Invoke();
        }


        
    }
}