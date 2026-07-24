using System;
using _Project.Scripts.Gameplay.Enums;
using _Project.Scripts.Utilities;
using UnityEngine;

namespace _Project.Scripts.Gameplay {
    public class SplatterInstance : MonoBehaviour, IPoolKeyed<SplatterType> {
        [SerializeField] private ParticleSystem[] systems;

        public bool InUse { get; private set; }

        private Action<Component, SplatterType> _return;
        private SplatterType _type;

        private float _remainingLifetime;
        private bool _isPaused;
        private bool _returnScheduled;

        public void Bind(
            Action<Component, SplatterType> returnToPool,
            SplatterType key
        ) {
            _return = returnToPool;
            _type = key;

            if (systems == null || systems.Length == 0)
                systems = GetComponentsInChildren<ParticleSystem>(true);
        }

        private void Update() {
            if (!_returnScheduled || _isPaused)
                return;

            _remainingLifetime -= Time.deltaTime;

            if (_remainingLifetime <= 0f)
                Return();
        }

        public void Play(Vector3 pos, Quaternion rot) {
            transform.SetPositionAndRotation(pos, rot);

            gameObject.SetActive(true);

            ForceRecycle();
            MarkInUse();

            for (int i = 0; i < systems.Length; i++) {
                if (!systems[i])
                    continue;

                systems[i].Play(true);
            }

            _remainingLifetime = ComputeMaxLifetimeSeconds();
            _returnScheduled = true;
        }

        public void SetPaused(bool paused) {
            if (_isPaused == paused)
                return;

            _isPaused = paused;

            for (int i = 0; i < systems.Length; i++) {
                ParticleSystem system = systems[i];

                if (!system)
                    continue;

                if (paused) {
                    system.Pause(true);
                }
                else if (InUse) {
                    system.Play(true);
                }
            }
        }

        public void ForceRecycle() {
            _returnScheduled = false;
            _remainingLifetime = 0f;
            _isPaused = false;

            for (int i = 0; i < systems.Length; i++) {
                if (!systems[i])
                    continue;

                systems[i].Stop(
                    true,
                    ParticleSystemStopBehavior.StopEmittingAndClear
                );
            }
        }

        public void MarkInUse() => InUse = true;

        public void MarkFree() => InUse = false;

        private float ComputeMaxLifetimeSeconds() {
            float max = 0.05f;

            for (int i = 0; i < systems.Length; i++) {
                ParticleSystem ps = systems[i];

                if (!ps)
                    continue;

                ParticleSystem.MainModule main = ps.main;

                float lifeMax =
                    main.startLifetime.mode ==
                    ParticleSystemCurveMode.TwoConstants
                        ? main.startLifetime.constantMax
                        : main.startLifetime.constant;

                float estimate = main.duration + lifeMax;

                if (estimate > max)
                    max = estimate;
            }

            return max;
        }

        private void Return() {
            if (!_returnScheduled)
                return;

            _returnScheduled = false;
            _remainingLifetime = 0f;

            _return?.Invoke(this, _type);
        }
    }
}