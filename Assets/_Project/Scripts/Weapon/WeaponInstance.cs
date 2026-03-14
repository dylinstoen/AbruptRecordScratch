using System;
using _Project.Scripts.Actors;
using _Project.Scripts.Weapon.Enums;
using _Project.Scripts.Weapon.Stucts;
using UnityEngine;
using IDisposable = _Project.Scripts.Actors.IDisposable;
using Object = UnityEngine.Object;

namespace _Project.Scripts.Weapon {
    public sealed class WeaponInstance : IWeaponLogic, IWeaponAmmoView, IWeaponIdentity, IDisposable, IEquipable {
        public string ID { get; }
        public AmmoType AmmoType { get; }
        public int Mag => _weaponMagazine.CurrentAmmo;
        public int Reserve => _ammoInventory.GetCurrent(AmmoType);
        public event Action AmmoChanged;
        
        private readonly WeaponMotor  _motor;
        private readonly WeaponView _view;
        private readonly GameObject _reticle;
        private readonly AmmoInventory _ammoInventory;
        private readonly IWeaponMagazine _weaponMagazine; 
        private uint _baseSeed;
        public WeaponInstance(string iD, AmmoType ammoType, WeaponMotor motor, WeaponView view, GameObject reticle, AmmoInventory ammoInventory, IWeaponMagazine weaponMagazine) {
            AmmoType = ammoType;
            _weaponMagazine = weaponMagazine;
            _ammoInventory = ammoInventory;
            _reticle = reticle;
            _motor = motor;
            _view = view;
            ID = iD;
            _baseSeed = (uint)UnityEngine.Random.Range(0, int.MaxValue);
        }
        
        public void Tick(in WeaponUseContext ctx) => _motor.Tick(ctx);

        public void StartFire(in WeaponUseContext ctx) => _motor.StartFire(ctx);

        public void StopFire(in WeaponUseContext ctx) => _motor.StopFire(ctx);

        public void Unequip() {
            _motor.Unequip();
            _motor.SetActive(false);
            _view.SetActive(false);
            _reticle.SetActive(false);
        } 

        public void Equip() {
            _motor.SetActive(true);
            _view.SetActive(true);
            _reticle.SetActive(true);
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
            if(_reticle != null) Object.Destroy(_reticle.gameObject);
        }

        private void HandleMagazineChanged() => AmmoChanged?.Invoke();
        
        private void HandleReserveChanged(AmmoType type, int current) {
            if (type != AmmoType) return;
            AmmoChanged?.Invoke();
        }
    }
}