using _Project.Scripts.Gameplay.Combat.Struct;

namespace _Project.Scripts.Actors {
    public interface IDamageable {
        void ApplyDamage(in DamageContext ctx);
    }
}