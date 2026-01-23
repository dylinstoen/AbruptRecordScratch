using UnityEngine;

namespace FPS.WeaponSystem {
    public interface IWeaponInputReader {
        /// <summary>
        /// Entity request to primary fire
        /// </summary>
        /// <returns>Bool</returns>
        public bool PrimaryFire();
        public bool SecondaryFire();
        public bool Reload();
        public float SwitchWeapon();
    }

    public interface IFireType {
        
    }
}

