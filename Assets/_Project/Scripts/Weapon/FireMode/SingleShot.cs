using FPS.Weapon;
using UnityEngine;

public class SingleShot : FireMode {
    private float coolDownRemaining = 0f;
    private bool wasPressedLastFrame = false;
    public SingleShot(Weapon weapon, float coolDown) : base(weapon, coolDown) { }
    public override void Tick(bool inputState, float deltaTime) {
        if (coolDownRemaining > 0f) {
            coolDownRemaining -= deltaTime;
        }
        bool pressedThisFrame = inputState && !wasPressedLastFrame;
        wasPressedLastFrame = inputState;
        if (coolDownRemaining <= 0f && pressedThisFrame) {
            coolDownRemaining = coolDown;
            weapon.TryFire();
        }
    }
}
