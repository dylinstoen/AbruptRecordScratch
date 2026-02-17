using _Project.Scripts.Weapon.Stucts;

namespace _Project.Scripts.Weapon {
    public interface IFireMode {
        void OnEquip();
        void StartFire(WeaponUseContext ctx);
        void StopFire(WeaponUseContext ctx);
        void Tick(WeaponUseContext ctx);
    }
}