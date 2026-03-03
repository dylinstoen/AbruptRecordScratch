using _Project.Scripts.Combat;
using _Project.Scripts.Gameplay;
using UnityEngine;

namespace _Project.Scripts.Weapon {
    public abstract class EmitterModeSO : ScriptableObject {
        public abstract IEmitterMode Create(IHitService hitService, int damage, GameObject owner, SourceImpactProfileSO sourceImpactProfile);
    }
}