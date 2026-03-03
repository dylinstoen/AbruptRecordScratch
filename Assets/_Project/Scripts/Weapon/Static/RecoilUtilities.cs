using UnityEngine;

namespace _Project.Scripts.Weapon.Static {
    public static class RecoilUtilities {
        private static uint XorShift32(uint x) {
            x ^= x << 13;
            x ^= x >> 17;
            x ^= x << 5;
            return x;
        }
        private static float Seeded01(uint seed) {
            seed = XorShift32(seed);
            return (seed & 0x00FFFFFF) / (float)0x01000000;
        }
        public static float SeededRange(uint seed, float min, float max) => Mathf.Lerp(min, max, Seeded01(seed));
    }
}