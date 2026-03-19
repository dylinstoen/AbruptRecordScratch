namespace _Project.Scripts.Utilities.StateMachine.Interfaces {
    public interface IState {
        void OnEnter();
        void OnExit();
        void Update();
        void FixedUpdate();
    }
}