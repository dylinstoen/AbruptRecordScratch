namespace _Project.Scripts.Utilities {
    public class CountdownTimer : Timer {
        public CountdownTimer(float value) : base(value) { }

        public override void Tick(float deltaTime) {
            if (IsRunning && Time > 0) {
                Time -= deltaTime;
            }
            if (IsRunning && Time <= 0) {
                Stop();
            }
        }

        public bool IsFinished => Time <= 0;
        public void Reset() => Time = InitalTime;

        public void Reset(float newTime) {
            InitalTime = newTime;
            Reset();
        }
    }
}