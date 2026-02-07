using FPS.Weapon;
using UnityEngine;

public class SingleShot : FireMode {
    public SingleShot(Weapon weapon) : base(weapon) { }
    public override void Tick(bool inputState, float deltaTime) {
        weapon.TryFire();
    }
}
