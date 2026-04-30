using _Project.Scripts.Actors;
using _Project.Scripts.Core;
using _Project.Scripts.Gameplay;
using _Project.Scripts.Gameplay.Structs;
using UnityEngine;

public class EnemyColliderDamager : MonoBehaviour
{
    [SerializeField] private int amount;

    private IImpactService _impactService;
    [SerializeField] private SourceVisualImpactProfileSO _sourceVisualImpactProfileSO;
    [SerializeField] private SourceAudioImpactProfileSO _sourceAudioImpactProfileSO;
    [SerializeField] private LayerMask damageableMask;

    private void Awake() {
        _impactService = SceneServiceLocator.Current.Impact;
    }

    private void OnTriggerEnter(Collider other) {
        if(((1 << other.gameObject.layer) & damageableMask) == 0) return;
        IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
        if (damageable == null) return;
        if (_impactService == null) {
            _impactService = SceneServiceLocator.Current.Impact;
            if(_impactService == null) return;
        }
        Vector3 hitPoint = other.ClosestPoint(transform.position);
        Vector3 hitNormal = (other.transform.position - transform.position).normalized;
        var ctx = new HitContext(hitPoint, hitNormal, other, gameObject, amount, damageable);

        _impactService.ProcessHitVisual(in ctx, _sourceVisualImpactProfileSO);
        _impactService.ProcessHitAudio(in ctx, _sourceAudioImpactProfileSO);
        _impactService.ProcessHitLogic(in ctx);
    }
}
