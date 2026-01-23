using System;
using FPS.WeaponSystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FPS.Player {
    public class WeaponInputReader : MonoBehaviour, IWeaponInputReader {
        private InputAction primaryFireAction;
        private InputAction secondaryFireAction;
        private InputAction reloadAction;
        private InputAction switchWeaponAction;

        private void Awake() {
            primaryFireAction = InputSystem.actions.FindAction("PrimaryFire");
            secondaryFireAction = InputSystem.actions.FindAction("SecondaryFire");
            reloadAction = InputSystem.actions.FindAction("Reload");
            switchWeaponAction = InputSystem.actions.FindAction("SwitchWeapon");
        }

        private void Update() {
            PrimaryFire();
            SecondaryFire();
            Reload();
            SwitchWeapon();
        }

        public bool PrimaryFire() => primaryFireAction.ReadValue<float>() > 0.5f;

        public bool SecondaryFire() => secondaryFireAction.ReadValue<float>() > 0.5f;

        public bool Reload() => reloadAction.ReadValue<float>() > 0.5f;

        public float SwitchWeapon() => switchWeaponAction.ReadValue<float>();
    }

}
