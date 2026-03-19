
using _Project.Scripts.Audio.Structs;
using _Project.Scripts.Gameplay.Structs;

namespace _Project.Scripts.Gameplay {
    public interface IImpactService {
        void ProcessHitVisual(in HitContext ctx, SourceVisualImpactProfileSO sourceVisual);
        void ProcessHitLogic(in HitContext ctx);
        void ProcessHitAudio(in HitContext ctx, SourceAudioImpactProfileSO sourceAudio);
    }
}