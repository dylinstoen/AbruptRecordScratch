using System.Collections.Generic;
using _Project.Scripts.Weapon;
using _Project.Scripts.Weapon.Enums;
using UnityEngine;

namespace _Project.Scripts.Actors.Structs {
    [CreateAssetMenu(fileName = "WeaponLoadout", menuName = "Player/WeaponLoadout")]
    public class WeaponLoadoutSO : ScriptableObject {
        [SerializeField] private List<WeaponDefinition> entries;
        public IReadOnlyList<WeaponDefinition> Entries => entries;
    }
}