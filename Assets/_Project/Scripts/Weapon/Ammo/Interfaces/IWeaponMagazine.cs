using System;

namespace _Project.Scripts.Weapon {
    public interface IWeaponMagazine {
        public event Action<int> OnMagazineChange;
        int MagSize { get; }
        int CurrentAmmo { get; }
        int MissingAmmo { get; }
        bool TryConsumeAmmo(int amount);
        int LoadUpTo(int amount);
    }
}