using _Project.Scripts.Actors;
using _Project.Scripts.Gameplay;
using _Project.Scripts.Gameplay.Structs;
using UnityEngine;

namespace _Project.Scripts.Gameplay {
    public sealed class HitService : MonoBehaviour, IHitService {
        [SerializeField] private DamageService damageService;
        [SerializeField] private ImpactEffectService impactEffectService;

        public void ProcessHit(in HitContext ctx, SourceImpactProfileSO source) {
            IDamageable damageable = ctx.Damageable;
            if (damageable != null)
                damageService.ApplyDamage(ctx, damageable);
            impactEffectService.Impact(ctx, source);
        }
    }
}