using UnityEngine;
using _Project.Scripts.Input;

namespace _Project.Scripts.Actors {
    [RequireComponent(typeof(Rigidbody))]
    public sealed class ActorMotor : MonoBehaviour {
        
        [Header("Movement")]
        [SerializeField] private float speed = 10f;
        private IAimRaySource _aimRaySource;
        private IIntentSource _intent;
        private Rigidbody _rb;
        private bool _boundAimSource = false;
        private void Awake() {
            _rb = GetComponent<Rigidbody>();
        }

        public void BindIntent(IIntentSource intent) {
            _intent = intent;
        }
        
        public void BindAimSource(IAimRaySource aimRaySource) {
            if(_boundAimSource) return;
            _aimRaySource = aimRaySource;
            _boundAimSource = true;
        }

        private void FixedUpdate() {
            if(!_boundAimSource) return;
            Vector3 forward = _aimRaySource.GetAimRay().direction;
            Vector2 direction = _intent.Current.Move;
            Vector3 right = Vector3.Cross(Vector3.up, forward);
            forward.y = 0f;
            right.y = 0f;
            forward.Normalize();
            right.Normalize();
            Vector3 moveDir = forward * direction.y + right * direction.x;
            _rb.linearVelocity = moveDir * speed + new Vector3(0, _rb.linearVelocity.y, 0);
        }
    }
}

