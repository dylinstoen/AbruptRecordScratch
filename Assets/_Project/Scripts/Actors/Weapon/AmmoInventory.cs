using System;
using System.Collections.Generic;
using _Project.Scripts.Actors.Structs;
using _Project.Scripts.Actors.Weapon;
using _Project.Scripts.Weapon;
using _Project.Scripts.Weapon.Enums;
using UnityEngine;

namespace _Project.Scripts.Actors {
    public class AmmoInventory : MonoBehaviour, IAmmoInventory {
        public event Action<int> OnCurrentAmmoChange;
        private readonly AmmoPool[] _ammoPool = new AmmoPool[Enum.GetValues(typeof(AmmoType)).Length];

        public void BuildAmmo(AmmoProfileSO ammoProfile) {
            foreach(AmmoType t in Enum.GetValues(typeof(AmmoType))) {
                _ammoPool[(int)t].Current = 0;
                _ammoPool[(int)t].Max = 0;
            }
            foreach (var startingAmmo in ammoProfile.Entries) {
                SetMax(startingAmmo.ammoType, startingAmmo.maxInventory);
                StoreUpToMax(startingAmmo.ammoType, startingAmmo.startingInventory);
            }
        }
        

        
        public int GetCurrent(AmmoType type) => _ammoPool[(int)type].Current;
        public int GetMax(AmmoType type) => _ammoPool[(int)type].Max;

        public int StoreUpToMax(AmmoType type, int requested) {
            if (requested <= 0) return 0;
            int current = GetCurrent(type);
            int max = GetMax(type);
            int space = max - current;
            if (space <= 0) return 0;
            int accepted = requested > space ? space : requested; 
            SetCurrent(type, current + accepted);
            return accepted;
        }
        private void SetCurrent(AmmoType type, int requested) => _ammoPool[(int)type].Current = requested;


        public int ConsumeUpTo(AmmoType type, int requested) {
            if(requested <= 0) return 0;
            int available = GetCurrent(type);
            int take = available < requested ? available : requested;
            if(take > 0)
                SetCurrent(type, available - take);
            OnCurrentAmmoChange?.Invoke(GetCurrent(type));
            return take;
        }

        public void SetMax(AmmoType type, int max) {
            int i = (int)type;
            _ammoPool[i].Max = max;
            _ammoPool[i].Current = Mathf.Min(_ammoPool[i].Current, _ammoPool[i].Max);
        }
        
    }
}