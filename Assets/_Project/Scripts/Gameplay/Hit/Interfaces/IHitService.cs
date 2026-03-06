
using _Project.Scripts.Gameplay.Structs;

namespace _Project.Scripts.Gameplay {
    public interface IHitService {
        void ProcessHit(in HitContext ctx, SourceImpactProfileSO source);
    }
}