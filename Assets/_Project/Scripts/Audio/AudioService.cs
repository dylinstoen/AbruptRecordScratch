using _Project.Scripts.Audio.Interfaces;
using _Project.Scripts.Audio.ScriptableObjects;
using _Project.Scripts.Audio.Structs;
using UnityEngine;

namespace _Project.Scripts.Audio {
    public class AudioService : MonoBehaviour, IAudioService {
        [SerializeField] private OneShotPool oneShot3DPool;
        [SerializeField] private OneShotPool oneShot2DPool;
        public AudioHandle Play3D(Vector3 position, Quaternion rotation, AudioCue cue) {
            var inst = oneShot3DPool.Rent();
            inst.Play(position, rotation, cue);
            return new AudioHandle(inst);
        }

        public void Play2D(Vector3 position, Quaternion rotation, AudioCue cue) {
            var inst = oneShot2DPool.Rent();
            inst.Play(position, rotation, cue);
        }

        public void Stop(AudioHandle handle) {
            if(handle.IsValid)
                handle.Stop();
        }
    }
}

