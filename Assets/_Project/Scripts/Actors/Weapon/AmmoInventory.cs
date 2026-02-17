using System;
using System.Collections.Generic;
using _Project.Scripts.Actors.Structs;
using _Project.Scripts.Weapon;
using _Project.Scripts.Weapon.Enums;
using UnityEngine;

namespace _Project.Scripts.Actors {
    public class AmmoInventory : IAmmoInventory {
        private readonly AmmoPool[] _ammoPool = new AmmoPool[Enum.GetValues(typeof(AmmoType)).Length];

        public AmmoInventory() {
            foreach(AmmoType t in Enum.GetValues(typeof(AmmoType))) {
                _ammoPool[(int)t].Current = 0;
                _ammoPool[(int)t].Max = 0;
            }
        }

        public int GetCurrent(AmmoType type) => _ammoPool[(int)type].Current;
        public int GetMax(AmmoType type) => _ammoPool[(int)type].Max;
        public void Add(AmmoType type, int amount) => _ammoPool[(int)type].Add(amount);
        public bool TryConsume(AmmoType type, int maxAmount) => _ammoPool[(int)type].TryConsume(maxAmount);
        public void SetMax(AmmoType type, int max) {
            int i = (int)type;
            _ammoPool[i].Max = max;
            _ammoPool[i].Current = Mathf.Min(_ammoPool[i].Current, _ammoPool[i].Max);
        }
        
    }
}