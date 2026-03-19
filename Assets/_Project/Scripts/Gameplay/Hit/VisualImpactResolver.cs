using _Project.Scripts.Audio.Interfaces;
using _Project.Scripts.Audio.ScriptableObjects;
using _Project.Scripts.Gameplay.Combat.Struct;
using _Project.Scripts.Gameplay.Enums;
using _Project.Scripts.Gameplay.Interract;
using _Project.Scripts.Gameplay.Structs;
using UnityEngine;

namespace _Project.Scripts.Gameplay {
    public class VisualImpactResolver : MonoBehaviour {
        [SerializeField] private StainPool stainPool;
        [SerializeField] private SplatterPool splatterPool;
        public void Impact(HitContext ctx, SourceVisualImpactProfileSO sourceVisual) {
            TargetVisualImpactProfileSO targetVisual = null;
            var targetEffect = ctx.HitCollider.GetComponentInParent<ITargetVisualEffect>();
            if (targetEffect != null)
                targetVisual = targetEffect.TargetVisualProfile;
            
            if (!targetVisual && !sourceVisual) return;

            if (targetVisual && sourceVisual) {
                
                ProcessSplatter(ctx, sourceVisual.SplatterType, targetVisual.SplatterType, targetVisual.SplatterBlendType);
                ProcessStain(ctx, sourceVisual.StainType, targetVisual.StainType, targetVisual.StainBlendType);
            }
            else if (sourceVisual) {
                ProcessSplatter(ctx, sourceVisual.SplatterType);
                ProcessStain(ctx, sourceVisual.StainType);
            }
            else if(targetVisual) {
                ProcessSplatter(ctx, targetVisual.SplatterType);
                ProcessStain(ctx, targetVisual.StainType);
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

        private void ProcessStain(HitContext ctx, StainType sourceType, StainType targetType, BlendType targetBlendType) {
            if (targetBlendType == BlendType.Override && targetType != StainType.None) {
                ProcessStain(ctx, targetType);
                return;
            }
            
            ProcessStain(ctx, sourceType);
            ProcessStain(ctx, targetType);
        }
        private void ProcessSplatter(HitContext ctx, SplatterType sourceType) {
            if (sourceType == SplatterType.None) return;
            var inst = splatterPool.Rent(sourceType);
            if (inst)
                inst.Play(ctx.Position, Quaternion.LookRotation(ctx.Normal));
        }
        private void ProcessStain(HitContext ctx, StainType sourceType) {
            if (sourceType == StainType.None) return;
            var inst = stainPool.Rent(sourceType);
            if (inst)
                inst.Activate(ctx.Position, Quaternion.LookRotation(ctx.Normal), 5f, ctx.HitCollider.transform);
            
        }
    }
}