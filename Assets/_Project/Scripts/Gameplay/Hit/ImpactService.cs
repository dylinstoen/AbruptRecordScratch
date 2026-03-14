using _Project.Scripts.Actors;
using _Project.Scripts.Gameplay;
using _Project.Scripts.Gameplay.Structs;
using UnityEngine;

namespace _Project.Scripts.Gameplay {
    public sealed class ImpactService : MonoBehaviour, IImpactService {
        [SerializeField] private VisualImpactResolver visualImpactResolver;

        public void ProcessHit(in HitContext ctx, SourceImpactProfileSO source) {
            IDamageable damageable = ctx.Damageable;
            if (damageable != null)
                damageable.ApplyDamage(ctx);
            visualImpactResolver.Impact(ctx, source);
        }
    }
}