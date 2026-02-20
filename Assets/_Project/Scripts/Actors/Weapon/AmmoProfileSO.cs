using System.Collections.Generic;
using _Project.Scripts.Actors.Structs;
using _Project.Scripts.Weapon.Enums;
using UnityEngine;

namespace _Project.Scripts.Actors.Weapon {
    [CreateAssetMenu(fileName = "AmmoProfile", menuName = "Weapon/AmmoProfile")]
    public class AmmoProfileSO : ScriptableObject {
        [SerializeField] private List<AmmoProfileEntry> entries;
        public IReadOnlyList<AmmoProfileEntry> Entries => entries;

#if UNITY_EDITOR
        private void OnValidate() {
            if(entries == null) return;
            var seen = new HashSet<AmmoType>();
            for (int i = 0; i < entries.Count; i++) {
                var entry = entries[i];
                if(entry.startingInventory > entry.maxInventory)
                    entry.startingInventory = entry.maxInventory;
                if (!seen.Add(entry.ammoType)) {
                    Debug.LogWarning($"Duplicate AmmoType found in {name}: {entry.ammoType}", this);
                }
                entries[i] = entry;
            }
        }
#endif
    }
}