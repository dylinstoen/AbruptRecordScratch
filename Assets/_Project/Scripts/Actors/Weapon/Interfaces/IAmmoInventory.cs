using System;
using _Project.Scripts.Weapon;
using _Project.Scripts.Weapon.Enums;

namespace _Project.Scripts.Actors {
    public interface IAmmoInventory {
        public event Action<AmmoType, int> OnCurrentAmmoChange;
        int GetCurrent(AmmoType  type);
        int GetMax(AmmoType  type);
        /// <summary>
        /// Stores the amount requested into the ammo inventory adding to the total pool up to the max it can store.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="requested"></param>
        /// <returns>The amount that was stored in the pool</returns>
        int StoreUpToMax(AmmoType type, int requested);
        void SetMax(AmmoType type, int max);
        /// <summary>
        /// Pulls the amount requested from the ammo inventory if there is any to pull.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="requested"></param>
        /// <returns>The actual amount that was able to be pulled</returns>
        int ConsumeUpTo(AmmoType type, int requested);
    }
}