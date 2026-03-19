using _Project.Scripts.Audio.ScriptableObjects;
using _Project.Scripts.Audio.Structs;
using UnityEngine;

namespace _Project.Scripts.Audio.Interfaces {
    public interface IAudioService {
        // Where this audio should be played
        // What kind of audio it is
        
        AudioHandle Play3D(Vector3 position, Quaternion rotation, AudioCue cue);
        void Play2D(Vector3 position, Quaternion rotation, AudioCue cue);
        public void Stop(AudioHandle handle);
    }
}