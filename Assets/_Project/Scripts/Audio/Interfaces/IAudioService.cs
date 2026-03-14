using _Project.Scripts.Audio.ScriptableObjects;
using UnityEngine;

namespace _Project.Scripts.Audio.Interfaces {
    public interface IAudioService {
        // Where this audio should be played
        // What kind of audio it is
        
        void Play3D(Vector3 position, Quaternion rotation, AudioCue cue);
        void Play2D(Vector3 position, Quaternion rotation, AudioCue cue);
    }
}