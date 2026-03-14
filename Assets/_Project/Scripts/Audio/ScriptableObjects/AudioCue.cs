using UnityEngine;

namespace _Project.Scripts.Audio.ScriptableObjects {
    [CreateAssetMenu(menuName = "Audio Cue", fileName = "AudioCue")]
    public class AudioCue : ScriptableObject {
        public AudioClip[] clips;
        [Range(0,1)]
        public float volume = 1f;

        public Vector2 pitchRange = new Vector2(0.95f, 1.05f);

        public AudioClip GetRandomClip() {
            return clips[Random.Range(0, clips.Length)];
        }

        public float GetRandomPitch() {
            return Random.Range(pitchRange.x, pitchRange.y);
        }
    }
}