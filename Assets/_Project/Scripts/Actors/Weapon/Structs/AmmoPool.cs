using UnityEngine;

namespace _Project.Scripts.Actors.Structs {
    public struct AmmoPool {
        public int Current;
        public int Max;
        public AmmoPool(int  current, int max) {
            Current = current;
            Max = max;
        }
    }
}