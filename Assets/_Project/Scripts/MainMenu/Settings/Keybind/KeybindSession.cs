using System;

namespace _Project.Scripts.MainMenu {
    public sealed class KeybindSession {
        private readonly InputBindingController _controller;

        private string _baselineJson;
        private bool _closed;

        public bool HasChanges {
            get {
                EnsureOpen();

                string currentJson =
                    _controller.CaptureCurrentOverrides();

                return currentJson != _baselineJson;
            }
        }

        public KeybindSession(InputBindingController controller) {
            _controller = controller;

            // Represents the bindings that were active when
            // the keybind page was opened.
            _baselineJson = controller.CaptureCurrentOverrides();
        }

        public void Apply() {
            EnsureOpen();

            _controller.SaveCurrentOverrides();

            // The newly saved bindings become the session baseline.
            _baselineJson =
                _controller.CaptureCurrentOverrides();
        }

        public void Discard() {
            EnsureOpen();

            _controller.RestoreOverrides(_baselineJson);
        }

        public void RestoreDefaults() {
            EnsureOpen();

            // This changes the working/live bindings, but does not
            // save them until Apply is pressed.
            _controller.RestoreDefaults();
        }

        public void CloseAndDiscard() {
            if (_closed)
                return;

            _controller.RestoreOverrides(_baselineJson);
            _closed = true;
        }

        public void CloseKeepingCurrentBindings() {
            if (_closed)
                return;

            _closed = true;
        }

        private void EnsureOpen() {
            if (_closed) {
                throw new InvalidOperationException(
                    "This keybind session has already been closed."
                );
            }
        }
    }
}