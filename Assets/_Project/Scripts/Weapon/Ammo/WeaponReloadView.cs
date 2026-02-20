using UnityEngine;

namespace _Project.Scripts.Weapon {
    public class WeaponReloadView : MonoBehaviour {
        [SerializeField] private Animator animator;
        
        public void PlayAnimation(float duration) {
            animator.SetTrigger("IsReloading");
        }

        public void StopAnimation() {
            animator.Play("Idle");
        }
    }
}