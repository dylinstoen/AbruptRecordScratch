using _Project.Scripts.Weapon.Stucts;
using UnityEngine;

namespace _Project.Scripts.Weapon {
    public abstract class EmitterModeSO : ScriptableObject {
        public abstract IEmitterMode Create();
    }
}