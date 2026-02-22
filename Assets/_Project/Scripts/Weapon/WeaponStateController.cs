using _Project.Scripts.Actors;
using _Project.Scripts.Weapon.Enums;
using _Project.Scripts.Weapon.Stucts;

namespace _Project.Scripts.Weapon {
    public sealed class WeaponStateController : IWeaponStateController {
        private readonly IFireMode _fireMode;
        private readonly IReloadPolicy _reloadPolicy;
        private enum State { Ready, Reloading }
        private State _state;
        
        public WeaponStateController(IFireMode fireMode, IReloadPolicy reloadPolicy) {
            _fireMode = fireMode;
            _reloadPolicy = reloadPolicy;
            BindEvents();
        }

        private void BindEvents() {
            _fireMode.FireAttempted += HandleFireAttempt;
            _reloadPolicy.ReloadAttempted += HandleReloadAttempt;
        }
        private void UnbindEvents() {
            _fireMode.FireAttempted -= HandleFireAttempt;
            _reloadPolicy.ReloadAttempted -= HandleReloadAttempt;
        }

        public void Dispose() => UnbindEvents();

        public void FirstEquipped() {
            _reloadPolicy.QuickFill();
            _state = State.Ready;
            _fireMode.Equip();
            _reloadPolicy.Equip();
        }
        
        public void Equip() {
            _state = State.Ready;
            _fireMode.Equip();
            _reloadPolicy.Equip();
        }
        
        public void Unequip() {
            if (_state != State.Reloading) return;
            _state = State.Ready;
            _reloadPolicy.Unequip();
        }

        public void Tick(in WeaponUseContext ctx) {
            
            switch (_state) {
                case State.Ready:
                    _fireMode.Tick(ctx);
                    break;
                case State.Reloading:
                    _reloadPolicy.Tick(ctx);
                    break;
            }
        }
        
        public void StartFire(in WeaponUseContext ctx) {
            if (_state == State.Reloading) return;
            _fireMode.StartFire(ctx);
        }

        public void StopFire(in WeaponUseContext ctx) {
            _fireMode.StopFire(ctx);
        }
        
        public void RequestReload() {
            if(_state == State.Reloading) return;
            if(_reloadPolicy.StartReloading())
                _state = State.Reloading;
        }

        public void HandleReloadAttempt(ReloadAttempt attempt) {
            _state = State.Ready;
        }
        
        public void HandleFireAttempt(FireAttempt attempt, float dt) {
            if(_state == State.Reloading) return;
            if (attempt == FireAttempt.Empty) {
                RequestReload();
            }
        }
    }
}