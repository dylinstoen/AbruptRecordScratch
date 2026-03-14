using _Project.Scripts.Gameplay.Combat.Struct;
using _Project.Scripts.Gameplay.Enums;
using _Project.Scripts.Gameplay.Interract;
using _Project.Scripts.Gameplay.Structs;
using UnityEngine;

namespace _Project.Scripts.Gameplay {
    public class VisualImpactResolver : MonoBehaviour {
        [SerializeField] private StainPool stainPool;
        [SerializeField] private SplatterPool splatterPool;
        public void Impact(HitContext ctx, SourceImpactProfileSO source) {
            TargetImpactProfileSO target = null;
            var targetEffect = ctx.HitCollider.GetComponentInParent<ITargetEffect>();
            if (targetEffect != null)
                target = targetEffect.TargetProfile;
            
            if (!target && !source) return;

            if (target && source) {
                ProcessSplatter(ctx, source.SplatterType, target.SplatterType, target.SplatterBlendType);
                ProcessStain(ctx, source.StainType, target.StainType, target.StainBlendType);
            }
            else if (source) {
                ProcessSplatter(ctx, source.SplatterType);
                ProcessStain(ctx, source.StainType);
            }
            else if(target) {
                ProcessSplatter(ctx, target.SplatterType);
                ProcessStain(ctx, target.StainType);
            }
        }

        private void ProcessSplatter(HitContext ctx, SplatterType sourceType, SplatterType targetType, BlendType targetBlendType) {
            if (targetBlendType == BlendType.Override) {
                ProcessSplatter(ctx, targetType);
                return;
            }
            ProcessSplatter(ctx, sourceType);
            ProcessSplatter(ctx, targetType);
        }
        private void ProcessSplatter(HitContext ctx, SplatterType sourceType) {
            var inst = splatterPool.Rent(sourceType);
            if (inst)
                inst.Play(ctx.Position, Quaternion.LookRotation(ctx.Normal));
            
        }
        private void ProcessStain(HitContext ctx, StainType sourceType, StainType targetType, BlendType targetBlendType) {
            if (targetBlendType == BlendType.Override) {
                ProcessStain(ctx, targetType);
                return;
            }
            ProcessStain(ctx, sourceType);
            ProcessStain(ctx, targetType);
        }
        private void ProcessStain(HitContext ctx, StainType sourceType) {
            var inst = stainPool.Rent(sourceType);
            if (inst)
                inst.Activate(ctx.Position, Quaternion.LookRotation(ctx.Normal), 5f, ctx.HitCollider.transform);
            
        }
    }
}