using _Project.Scripts.Audio.Interfaces;
using _Project.Scripts.Core;
using _Project.Scripts.Items;
using _Project.Scripts.Audio.ScriptableObjects;
using UnityEngine;

[CreateAssetMenu(fileName = "CoinPickup", menuName = "Item/CoinPickup")]
public class CoinPickupDefinition : ItemDefinition
{
    [SerializeField] private int value = 1;
    [SerializeField] private AudioCue pickupSound;
    public override bool TryApply(GameObject target, IAudioService audioService) {
        if (SceneServiceLocator.Current == null) {
            Debug.LogError("SceneServiceLocator is not initialized.");
            return false;
        }
        if (target == null) {
            Debug.LogError("Target object is null.");
            return false;
        }
        if (audioService == null) {
            Debug.LogError("Audio service is null.");
            return false;
        }
        if (SceneServiceLocator.Current.Coin == null) {
            Debug.LogError("Coin service is not available.");
            return false;
        }
        // WARNING: Hidden dependency: This method assumes that the Coin service is available in the SceneServiceLocator.
        SceneServiceLocator.Current.Coin.AddCoins(value);
        audioService.Play3D(target.transform.position, target.transform.rotation, pickupSound);
        return true;
    }
}
