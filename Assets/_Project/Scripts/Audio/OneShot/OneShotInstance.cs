using System;
using _Project.Scripts.Audio.ScriptableObjects;
using _Project.Scripts.Gameplay;
using UnityEngine;

namespace _Project.Scripts.Audio {
    public class OneShotInstance : MonoBehaviour, IPoolable {
        [SerializeField] private AudioSource audioSource;
        public bool InUse { get; private set; }

        public Action<Component> _returnToPool;
        public void Bind(Action<Component> returnToPool) {
            _returnToPool = returnToPool;
        }

        public void MarkInUse() => InUse = true;

        public void MarkFree() => InUse = false;

        public void ForceRecycle() {
            CancelInvoke(nameof(Return));
            audioSource.Stop();
        }


        public void Play(Vector3 pos, Quaternion rot, AudioCue cue) {
            transform.SetPositionAndRotation(pos, rot);
            gameObject.SetActive(true);
            ForceRecycle();
            MarkInUse();
            AudioClip clip = cue.GetRandomClip();
            float pitch = cue.GetRandomPitch();
            audioSource.pitch = pitch;
            audioSource.clip = clip;
            audioSource.volume = cue.volume;
            float timeOfClip = clip.length;
            audioSource.Play();
            CancelInvoke(nameof(Return));
            Invoke(nameof(Return), timeOfClip);
        }

        private void Return() {
            _returnToPool?.Invoke(this);
        }
    }
}