using System;
using UnityEngine;

namespace _Project.Scripts.MainMenu {
    public struct RowKey : IEquatable<RowKey> {
        private readonly string _action;
        private readonly string _composite;
        private readonly string _part;

        public RowKey(string action, string composite, string part) {
            _action = action;
            _composite = composite;
            _part = part;
        }
        public bool Equals(RowKey other) => _action == other._action && _composite == other._composite && _part == other._part;
        public override bool Equals(object obj) => obj is RowKey other && Equals(other);
        public override int GetHashCode() => HashCode.Combine(_action, _composite, _part);

    }
}

