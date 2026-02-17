using _Project.Scripts.Weapon.Stucts;

namespace _Project.Scripts.Weapon {
    public interface IEmitterMode {
        void Fire(WeaponUseContext ctx);
    }
}