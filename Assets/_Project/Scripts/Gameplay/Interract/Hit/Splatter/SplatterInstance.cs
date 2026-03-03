using System;
using _Project.Scripts.Gameplay.Enums;
using UnityEngine;

namespace _Project.Scripts.Gameplay {
    public class SplatterInstance : MonoBehaviour, IPoolKeyed<SplatterType> {
        [SerializeField] private ParticleSystem[] systems;

        public bool InUse { get; private set; }

        private Action<Component, SplatterType> _return;
        private SplatterType _type;

        public void Bind(Action<Component, SplatterType> returnToPool, SplatterType key)
        {
            _return = returnToPool;
            _type = key;

            if (systems == null || systems.Length == 0)
                systems = GetComponentsInChildren<ParticleSystem>(true);
        }

        public void MarkInUse() => InUse = true;
        public void MarkFree() => InUse = false;

        public void Play(Vector3 pos, Quaternion rot) {
            transform.SetParent(null, true);
            transform.SetPositionAndRotation(pos, rot);

            gameObject.SetActive(true);

            ForceRecycle();
            MarkInUse();

            for (int i = 0; i < systems.Length; i++)
                systems[i].Play(true);

            CancelInvoke(nameof(Return));
            Invoke(nameof(Return), ComputeMaxLifetimeSeconds());
        }

        public void ForceRecycle()
        {
            CancelInvoke(nameof(Return));

            for (int i = 0; i < systems.Length; i++)
            {
                if (!systems[i]) continue;
                systems[i].Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            }
        }

        private float ComputeMaxLifetimeSeconds() {
            float max = 0.05f;
            for (int i = 0; i < systems.Length; i++)
            {
                var ps = systems[i];
                if (!ps) continue;
                var main = ps.main;

                float lifeMax =
                    main.startLifetime.mode == ParticleSystemCurveMode.TwoConstants
                        ? main.startLifetime.constantMax
                        : main.startLifetime.constant;

                float estimate = main.duration + lifeMax;
                if (estimate > max) max = estimate;
            }
            return max;
        }

        private void Return() {
            _return?.Invoke(this, _type);
        }
    }
}