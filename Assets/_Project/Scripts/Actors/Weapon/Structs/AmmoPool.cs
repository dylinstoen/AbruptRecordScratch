using UnityEngine;

namespace _Project.Scripts.Actors.Structs {
    public struct AmmoPool {
        public int Current;
        public int Max;
        public AmmoPool(int  current, int max) {
            Current = current;
            Max = max;
        }

        public void Add(int amount) => Current = Mathf.Min(Current + amount, Max);

        public bool TryConsume(int amount) {
            if (Current < amount) return false;
            Current -= amount;
            return true;
        }
    }
}