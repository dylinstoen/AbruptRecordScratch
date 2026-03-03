

namespace _Project.Scripts.Weapon.Stucts {
    public readonly struct LogicParts {
        public readonly IWeaponMagazine Magazine;
        public readonly IReloadPolicy ReloadPolicy;
        public readonly WeaponReloadBridge ReloadBridge;
        public readonly WeaponStateController Controller;
        public readonly IFireMode FireMode;

        public LogicParts(IWeaponMagazine magazine, IReloadPolicy reloadPolicy, WeaponReloadBridge reloadBridge, WeaponStateController controller, IFireMode fireMode) {
            Magazine = magazine;
            ReloadPolicy = reloadPolicy;
            ReloadBridge = reloadBridge;
            Controller = controller;
            FireMode = fireMode;
        }
    }
}