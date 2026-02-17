using _Project.Scripts.Actors;
using _Project.Scripts.Weapon.Enums;

// Reach into the actors weapon inventory and try to consume a shot.
// Total ammo available for each ammo type is owned by the actor in the weapon inventory
// Max ammo in each clip for each gun until i have to reload is owned by the weapon definition
namespace _Project.Scripts.Weapon {
    public class WeaponAmmo : IWeaponAmmo {
        private AmmoType _type;
        private IAmmoInventory _ammoInventory;
        private readonly int _magSize;
        public WeaponAmmo(IAmmoInventory ammoInventory, int magSize, AmmoType type) {
            _ammoInventory = ammoInventory;
            _magSize = magSize;
            _type = type;
        }
        
        public bool TryConsumeAmmo(int amount) => _ammoInventory.TryConsume(_type, amount);

        public bool IsReloading { get; }
        
        public bool IsEmpty { get; }
        
        public int TryStartReload() {
            throw new System.NotImplementedException();
        }
    }
}