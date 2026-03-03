using _Project.Scripts.Weapon;

namespace _Project.Scripts.Actors {
    public interface ICameraRecoilService {
        void SetProfile(RecoilSO profile, uint baseSeed);
        void ResetRecoil(bool resetShotIndex = true);
        void OnShotFired();
        void OnTriggerReleased(bool resetShotIndex = true) ;
    }
}