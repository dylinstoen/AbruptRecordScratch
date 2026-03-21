using System;

namespace _Project.Scripts.Utilities {
    public abstract class Timer {
        protected float InitalTime;
        protected float Time { get; set; }
        public bool IsRunning { get; protected set; }
        public float Progress => Time / InitalTime;
        
        public Action OnTimerStart = delegate { };
        public Action OnTimerStop = delegate { };

        protected Timer(float value) {
            InitalTime = value;
            IsRunning = true;
        }

        public void Start() {
            Time = InitalTime;
            if (!IsRunning) {
                IsRunning = true;
                OnTimerStart.Invoke();
            }
        }

        public void Stop() {
            if (IsRunning) {
                IsRunning = false;
                OnTimerStop.Invoke();
            }
        }
        
        public void Resume() => IsRunning = true;
        public void Pause() => IsRunning = false;
        public abstract void Tick(float deltaTime);
    }
}