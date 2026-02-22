using _Project.Scripts.Actors;

namespace _Project.Scripts.Weapon.Stucts {
    public readonly struct WeaponFacets {
        public IEquipable Equipable { get; }
        public IWeaponIdentity Identity { get; }
        public IWeaponLogic Logic { get; }
        public IWeaponAmmoView AmmoView { get; }
        public IDisposable Disposable { get; }
        
        public WeaponFacets(WeaponInstance instance) {
            Logic = instance;
            AmmoView = instance;
            Identity = instance;
            Disposable = instance;
            Equipable = instance;
        }
    }
}