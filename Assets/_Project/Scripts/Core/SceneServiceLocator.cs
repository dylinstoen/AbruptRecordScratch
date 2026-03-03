using System;
using UnityEngine;

namespace _Project.Scripts.Core {
    public static class SceneServiceLocator {
        private static SceneServices _current;
        public static SceneServices Current {
            get {
                if (!_current) {
                    throw new InvalidOperationException("SceneServiceLocator.Current is null." +
                                                        "Ensure a scene service exist in the scene and has awake executed");
                }
                return _current;
            }
        }
        internal static void Bind(SceneServices services) {
            if (!services) throw new ArgumentNullException(nameof(services));
            if (_current != null && _current != services)
                throw new InvalidOperationException($"SceneServiceLocator already bound to '{_current.name}', cannot bind '{services.name}'.");
            _current = services;
        }
        internal static void Unbind(SceneServices services) {
            if(_current == services) _current = null;
        }
    }
}