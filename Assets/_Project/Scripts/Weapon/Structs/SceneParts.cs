using UnityEngine;

namespace _Project.Scripts.Weapon.Stucts {
    public readonly struct SceneParts {
        public readonly GameObject Reticle;
        public readonly WeaponView View;
        public readonly WeaponMotor Motor;

        public SceneParts(GameObject reticle, WeaponView view, WeaponMotor motor)
        {
            Reticle = reticle;
            View = view;
            Motor = motor;
        }
    }
}