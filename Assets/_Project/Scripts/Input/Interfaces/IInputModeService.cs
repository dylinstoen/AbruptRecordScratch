namespace _Project.Scripts.Input {
    public interface IInputModeService {
        void SetGameplay();
        void SetDead();
        IDeathUIIInputEvent DeathUIIInputEvent { get; }
    }
}