using _Project.Scripts.Actors.Structs;
using _Project.Scripts.Actors.Weapon;
using UnityEngine;

namespace _Project.Scripts.Actors {
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Player/PlayerConfig")]
    public sealed class PlayerConfigSO : ScriptableObject {
        [SerializeField] private int startingHealth;
        public WeaponLoadoutSO weaponLoadoutSo;
        public AmmoProfileSO ammoProfileSo;
    }
}