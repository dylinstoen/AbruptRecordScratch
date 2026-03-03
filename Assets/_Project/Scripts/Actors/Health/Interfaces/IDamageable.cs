using _Project.Scripts.Gameplay.Combat.Struct;
using _Project.Scripts.Gameplay.Structs;

namespace _Project.Scripts.Actors {
    public interface IDamageable {
        void ApplyDamage(in HitContext ctx);
    }
}