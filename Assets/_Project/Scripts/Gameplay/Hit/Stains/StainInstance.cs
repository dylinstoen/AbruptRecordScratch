using System;
using _Project.Scripts.Gameplay.Enums;
using _Project.Scripts.Utilities;
using UnityEngine;

namespace _Project.Scripts.Gameplay {
    public sealed class StainInstance : MonoBehaviour, IPoolKeyed<StainType> {
        private Action<Component, StainType> _return;
        private StainType _type;

        private Transform _follow;
        private bool _wasFollowing;

        private Vector3 _localPosition;
        private Quaternion _localRotation;

        private bool _hasTtl;
        private float _remainingLifetime;

        private bool _isPaused;

        public bool InUse { get; private set; }

        public void MarkInUse() {
            InUse = true;
        }

        public void MarkFree() {
            InUse = false;
        }

        public void Bind(
            Action<Component, StainType> returnToPool,
            StainType key
        ) {
            _return = returnToPool;
            _type = key;
        }

        public void Activate(
            Vector3 worldPosition,
            Quaternion worldRotation,
            float ttlSeconds,
            Transform follow = null
        ) {
            // Clears state from the previous use of this pooled instance.
            // It intentionally does not clear _isPaused.
            ForceRecycle();

            transform.SetPositionAndRotation(
                worldPosition,
                worldRotation
            );

            _follow = follow;
            _wasFollowing = follow != null;

            if (_wasFollowing) {
                _localPosition = _follow.InverseTransformPoint(worldPosition);

                _localRotation =
                    Quaternion.Inverse(_follow.rotation) *
                    worldRotation;
            }

            _hasTtl = ttlSeconds > 0f;
            _remainingLifetime = Mathf.Max(0f, ttlSeconds);

            InUse = true;
            gameObject.SetActive(true);
        }

        private void LateUpdate() {
            if (!InUse || _isPaused)
                return;

            UpdateFollowTarget();
            UpdateLifetime();
        }

        private void UpdateFollowTarget() {
            if (!_wasFollowing)
                return;

            // _follow == null also catches Unity objects that have been
            // destroyed because Unity overloads its null comparison.
            if (_follow == null ||
                !_follow.gameObject.activeInHierarchy) {
                Release();
                return;
            }

            transform.SetPositionAndRotation(
                _follow.TransformPoint(_localPosition),
                _follow.rotation * _localRotation
            );
        }

        private void UpdateLifetime() {
            // UpdateFollowTarget may have released this instance.
            if (!InUse || !_hasTtl)
                return;

            _remainingLifetime -= Time.deltaTime;

            if (_remainingLifetime <= 0f)
                Release();
        }

        public void SetPaused(bool paused) {
            _isPaused = paused;
        }

        public void ForceRecycle() {
            _follow = null;
            _wasFollowing = false;

            _localPosition = Vector3.zero;
            _localRotation = Quaternion.identity;

            _hasTtl = false;
            _remainingLifetime = 0f;

            // Do not reset _isPaused here. The pause state belongs to the
            // stain pool and should survive ring recycling.
        }

        private void Release() {
            if (!InUse)
                return;

            ForceRecycle();

            // ReturnToPool checks InUse before returning, so don't mark this
            // free here. The pool owns that transition.
            _return?.Invoke(this, _type);
        }
    }
}