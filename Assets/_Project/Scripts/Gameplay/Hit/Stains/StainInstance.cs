using System;
using _Project.Scripts.Gameplay.Enums;
using _Project.Scripts.Utilities;
using UnityEngine;

namespace _Project.Scripts.Gameplay {
    public class StainInstance : MonoBehaviour, IPoolKeyed<StainType> {
        private Action<Component, StainType> _return;
        private StainType _type;
        private Transform _follow;
        private Vector3 _localPos;
        private Quaternion _localRot;
        private bool _hasTtl;
        private float _dieAt;

        public bool InUse { get; private set; }
        public void MarkInUse() => InUse = true;
        public void MarkFree() => InUse = false;
        
        public void Bind(Action<Component, StainType> returnToPool, StainType key) {
            _return = returnToPool;
            _type = key;
        }

        private bool _isFollowing;

        public void Activate(Vector3 worldPos, Quaternion worldRot, float ttlSeconds, Transform follow = null) {
            ForceRecycle();

            transform.SetPositionAndRotation(worldPos, worldRot);

            _follow = follow;
            _isFollowing = follow != null;

            if (_isFollowing) {
                _localPos = _follow.InverseTransformPoint(worldPos);
                _localRot = Quaternion.Inverse(_follow.rotation) * worldRot;
            }

            _hasTtl = ttlSeconds > 0f;
            _dieAt = _hasTtl ? Time.time + ttlSeconds : 0f;

            InUse = true;
            gameObject.SetActive(true);
        }

        private void LateUpdate() {
            if (!InUse)
                return;

            if (_isFollowing) {
                if (_follow == null || !_follow.gameObject.activeInHierarchy) {
                    Release();
                    return;
                }

                transform.SetPositionAndRotation(
                    _follow.TransformPoint(_localPos),
                    _follow.rotation * _localRot
                );
            }

            if (_hasTtl && Time.time >= _dieAt)
                Release();
        }

        public void ForceRecycle() {
            _follow = null;
            _isFollowing = false;
            _hasTtl = false;
            _dieAt = 0f;
        }

        private void Release() {
            if (!InUse)
                return;

            InUse = false;
            ForceRecycle();
            _return?.Invoke(this, _type);
        }
    }
}