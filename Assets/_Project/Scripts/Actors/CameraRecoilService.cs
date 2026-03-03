using _Project.Scripts.Weapon;
using _Project.Scripts.Weapon.Static;
using UnityEngine;

namespace _Project.Scripts.Actors {
    public class CameraRecoilService : MonoBehaviour, ICameraRecoilService {
        [SerializeField] private CameraSourceRig rig;
        private RecoilSO _profile;
        private uint _baseSeed;
        private int _shotIndex;
        
        private Vector2 _kickOffset;
        private Vector2 _kickVel;
        private void LateUpdate() { 
            if (!_profile || !rig) return;
            
            _kickOffset = Vector2.SmoothDamp(
                current: _kickOffset,
                target: Vector2.zero,
                currentVelocity: ref _kickVel,
                smoothTime: _profile.aimReturnTime,
                maxSpeed: _profile.maxAimSpeed,
                deltaTime: Time.deltaTime
                );
            rig.SetRecoilOffset(_kickOffset);
        }
        
        public void SetProfile(RecoilSO profile, uint baseSeed) {
            _profile = profile;
            _baseSeed = baseSeed != 0 ? baseSeed : 1u;
            ResetRecoil(resetShotIndex: true);
        }
        
        public void ResetRecoil(bool resetShotIndex = true) {
            _kickOffset = Vector2.zero;
            _kickVel = Vector2.zero;
            if (resetShotIndex) _shotIndex = 0;
            if (rig) rig.SetRecoilOffset(Vector2.zero);
        }
        
        public void OnShotFired() {
            if (!_profile || !rig) return;
            rig.AddToBaseYawPitch(_kickOffset);
            _kickOffset = Vector2.zero;
            _kickVel = Vector2.zero;
            Vector2 kick = ComputeKick(_shotIndex++, _baseSeed);
            _kickOffset += kick;
        }
        
        public void OnTriggerReleased(bool resetShotIndex = true) {
            if (resetShotIndex) _shotIndex = 0;
        }
        
        private Vector2 ComputeKick(int shotIndex, uint baseSeed) {
            uint shotSeed = baseSeed ^ (uint)(shotIndex + 1);
            float yaw = RecoilUtilities.SeededRange(shotSeed, _profile.yawMin, _profile.yawMax);
            float pitch = _profile.pitchPerShot;
            return new Vector2(yaw, pitch);
            
        }
        
    }
}