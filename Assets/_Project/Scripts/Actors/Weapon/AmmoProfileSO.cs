using System.Collections.Generic;
using _Project.Scripts.Actors.Structs;
using UnityEngine;

namespace _Project.Scripts.Actors.Weapon {
    [CreateAssetMenu(fileName = "AmmoProfile", menuName = "Weapon/AmmoProfile")]
    public class AmmoProfileSO : ScriptableObject {
        [SerializeField] private List<AmmoProfileEntry> entries;
        public IReadOnlyList<AmmoProfileEntry> Entries => entries;
    }
}