using System;

namespace _Project.Scripts.Weapon {
    public sealed class WeaponMagazine : IWeaponMagazine {
        public event Action OnMagazineChange;
        public int MagSize => _magSize;
        public int CurrentAmmo => _currentAmmo;
        public int MissingAmmo => _magSize - _currentAmmo;
        private readonly int _magSize;
        private int _currentAmmo;
        public WeaponMagazine(int magSize) => _magSize = magSize;
        public bool TryConsumeAmmo(int costPerShot) {
            if(costPerShot > _currentAmmo) return false;
            _currentAmmo -= costPerShot;
            // TODO: Update UI
            OnMagazineChange?.Invoke();
            return true;
        }

        public int LoadUpTo(int amount) {
            if (amount <= 0) return 0;
            int space = _magSize - _currentAmmo;
            if(space <= 0) return 0;
            int accepted = amount > space ? space : amount;
            _currentAmmo += accepted;
            OnMagazineChange?.Invoke();
            return accepted;
        }
    }
}