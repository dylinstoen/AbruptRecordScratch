using System;
using _Project.Scripts.Actors;
using _Project.Scripts.Weapon.Enums;
using _Project.Scripts.Weapon.Stucts;

namespace _Project.Scripts.Weapon {
    public interface IReloadPolicy {
        public event Action<ReloadAttempt> ReloadAttempted;
        public event Action<float> ReloadStarted;
        public event Action ReloadStopped;
        void Tick(in WeaponUseContext ctx);
        void OnEquip();
        void OnUnequip();
        bool StartReloading();
        void QuickFill();
    }
}