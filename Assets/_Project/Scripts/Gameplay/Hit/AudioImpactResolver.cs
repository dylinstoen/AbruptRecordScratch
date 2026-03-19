using System;
using _Project.Scripts.Audio;
using _Project.Scripts.Audio.Structs;
using _Project.Scripts.Gameplay.Enums;
using _Project.Scripts.Gameplay.Structs;
using UnityEngine;

namespace _Project.Scripts.Gameplay {
    public class AudioImpactResolver : MonoBehaviour {
        [SerializeField] private AudioService audioService;
        public void Impact(HitContext ctx, SourceAudioImpactProfileSO sourceAudio) {
            TargetAudioImpactProfileSO targetAudio = null;
            var targetEffect = ctx.HitCollider.GetComponent<ITargetAudioEffect>();
            if (targetEffect != null)
                targetAudio = targetEffect.TargetAudioProfile;

            if (!targetAudio && !sourceAudio)
                return;

            if (!targetAudio) {
                audioService.Play3D(ctx.Position, Quaternion.LookRotation(ctx.Normal), sourceAudio.audioCue);
                return;
            }
            
            switch (targetAudio.blendType) {
                case BlendType.Override:
                    audioService.Play3D(ctx.Position, Quaternion.LookRotation(ctx.Normal), targetAudio.audioCue);
                    break;
                case BlendType.Additive:
                    audioService.Play3D(ctx.Position, Quaternion.LookRotation(ctx.Normal), targetAudio.audioCue);
                    audioService.Play3D(ctx.Position, Quaternion.LookRotation(ctx.Normal), targetAudio.audioCue);
                    break;
            }
        }
    }
}