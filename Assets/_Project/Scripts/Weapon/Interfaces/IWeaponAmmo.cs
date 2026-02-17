namespace _Project.Scripts.Weapon {
    public interface IWeaponAmmo {
        public bool TryConsumeAmmo(int amount);
        bool IsReloading { get; }
        bool IsEmpty { get; }
        public int TryStartReload();
    }
}