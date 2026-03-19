namespace _Project.Scripts.Audio.Structs {
    public readonly  struct AudioHandle {
        private readonly OneShotInstance _instance;
        public AudioHandle(OneShotInstance instance) {
            _instance = instance;
        }
        public bool IsValid => _instance != null && _instance.InUse;
        public void Stop() {
            if (_instance && _instance.InUse)
                _instance.ForceRecycle();
        }
    }
}