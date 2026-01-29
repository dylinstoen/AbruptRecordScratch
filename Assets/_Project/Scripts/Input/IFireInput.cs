using UnityEngine;

namespace FPS.Input {
    public interface IFireInput {
        /// <summary>
        /// Entity request to primary fire
        /// </summary>
        /// <returns>Bool</returns>
        public bool PrimaryFire();
        public bool SecondaryFire();
        public float SwitchWeapon();
    }
}

