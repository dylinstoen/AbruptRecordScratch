using _Project.Scripts.Gameplay.Combat.Struct;
using _Project.Scripts.Gameplay.Enums;
using _Project.Scripts.Gameplay.Interract;
using _Project.Scripts.Gameplay.Structs;
using UnityEngine;

namespace _Project.Scripts.Gameplay {
    public class ImpactEffectService : MonoBehaviour {
        [SerializeField] private StainPool stainPool;
        [SerializeField] private SplatterPool splatterPool;
        private HitContext _ctx;
        public void Impact(HitContext ctx, SourceImpactProfileSO source) {
            _ctx = ctx;
            TargetImpactProfileSO target = null;
            var targetEffect = ctx.HitCollider.GetComponentInParent<ITargetEffect>();
            if (targetEffect != null)
                target = targetEffect.TargetProfile;
            
            if (!target && !source) return;

            if (target && source) {
                Debug.Log("Spawned Source and Target");
                ProcessSplatter(source.SplatterType, target.SplatterType, target.SplatterBlendType);
                ProcessStain(source.StainType, target.StainType, target.StainBlendType);
            }
            else if (source) {
                Debug.Log("Spawned Source");
                ProcessSplatter(source.SplatterType);
                ProcessStain(source.StainType);
            }
            else if(target) {
                Debug.Log("Spawned Target");
                ProcessSplatter(target.SplatterType);
                ProcessStain(target.StainType);
            }
        }

        private void ProcessSplatter(SplatterType sourceType, SplatterType targetType, BlendType targetBlendType) {
            if (targetBlendType == BlendType.Override) {
                ProcessSplatter(targetType);
                return;
            }
            ProcessSplatter(sourceType);
            ProcessSplatter(targetType);
        }
        private void ProcessSplatter(SplatterType sourceType) {
            var inst = splatterPool.Rent(sourceType);
            if (inst)
                inst.Play(_ctx.Position, Quaternion.LookRotation(_ctx.Normal));
            
        }
        private void ProcessStain(StainType sourceType, StainType targetType, BlendType targetBlendType) {
            if (targetBlendType == BlendType.Override) {
                ProcessStain(targetType);
                return;
            }
            ProcessStain(sourceType);
            ProcessStain(targetType);
        }
        private void ProcessStain(StainType sourceType) {
            var inst = stainPool.Rent(sourceType);
            if (inst)
                inst.Activate(_ctx.Position, Quaternion.LookRotation(_ctx.Normal), 5f, _ctx.HitCollider.transform);
            
        }
    }
}