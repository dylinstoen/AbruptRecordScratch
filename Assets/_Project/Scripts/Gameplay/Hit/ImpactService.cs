using _Project.Scripts.Actors;
using _Project.Scripts.Audio;
using _Project.Scripts.Audio.ScriptableObjects;
using _Project.Scripts.Audio.Structs;
using _Project.Scripts.Gameplay;
using _Project.Scripts.Gameplay.Structs;
using UnityEngine;

namespace _Project.Scripts.Gameplay {
    public sealed class ImpactService : MonoBehaviour, IImpactService {
        [SerializeField] private VisualImpactResolver visualImpactResolver;
        [SerializeField] private AudioImpactResolver audioImpactResolver;
        [SerializeField] private AudioService audioService;

        public void ProcessHitVisual(in HitContext ctx, SourceVisualImpactProfileSO sourceVisual) {
            visualImpactResolver.Impact(ctx, sourceVisual);
        }

        public void ProcessHitLogic(in HitContext ctx) {
            IDamageable damageable = ctx.Damageable;
            if (damageable != null)
                damageable.ApplyDamage(ctx);
        }

        public void ProcessHitAudio(in HitContext ctx, SourceAudioImpactProfileSO sourceAudio) {
            audioImpactResolver.Impact(ctx, sourceAudio);
        }
    }
}