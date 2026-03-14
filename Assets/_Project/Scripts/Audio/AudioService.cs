using _Project.Scripts.Audio.Interfaces;
using _Project.Scripts.Audio.ScriptableObjects;
using UnityEngine;

namespace _Project.Scripts.Audio {
    public class AudioService : MonoBehaviour, IAudioService {
        [SerializeField] private OneShotPool oneShot3DPool;
        [SerializeField] private OneShotPool oneShot2DPool;
        public void Play3D(Vector3 position, Quaternion rotation, AudioCue cue) {
            var inst = oneShot3DPool.Rent();
            inst.Play(position, rotation, cue);
        }

        public void Play2D(Vector3 position, Quaternion rotation, AudioCue cue) {
            var inst = oneShot2DPool.Rent();
            inst.Play(position, rotation, cue);
        }
    }
}

