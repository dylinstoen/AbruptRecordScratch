using _Project.Scripts.Weapon.Stucts;
using UnityEngine;

namespace _Project.Scripts.Weapon {
    public class WeaponInstance : IWeapon {
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

        public void LateTick(float dt) => _view.LateTick(dt);

        public void SetActive(bool active) {
            _view.SetActive(active);
            _motor.SetActive(active);
        }
        public void Dispose()
        {
            if (_view != null) Object.Destroy(_view.gameObject);
            if (_motor != null) Object.Destroy(_motor.gameObject);
        }
    }
}