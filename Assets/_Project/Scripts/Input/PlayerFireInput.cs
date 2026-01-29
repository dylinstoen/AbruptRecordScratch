using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FPS.Input {
    public class PlayerFireInput : MonoBehaviour, IFireInput {
        private InputAction primaryFireAction;
        private InputAction secondaryFireAction;
        private InputAction switchWeaponAction;

        private void Awake() {
            primaryFireAction = InputSystem.actions.FindAction("PrimaryFire");
            secondaryFireAction = InputSystem.actions.FindAction("SecondaryFire");
            switchWeaponAction = InputSystem.actions.FindAction("SwitchWeapon");
        }

        private void Update() {
            PrimaryFire();
            SecondaryFire();
            SwitchWeapon();
        }

        public bool PrimaryFire() => primaryFireAction.ReadValue<float>() > 0.5f;

        public bool SecondaryFire() => secondaryFireAction.ReadValue<float>() > 0.5f;

        public float SwitchWeapon() => switchWeaponAction.ReadValue<float>();
    }

}
