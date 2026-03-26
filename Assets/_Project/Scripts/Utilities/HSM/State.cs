using System.Collections.Generic;

namespace _Project.Scripts.Utilities.HSM {
    public abstract class State {
        public readonly StateMachine Machine;
        public readonly State Parent;
        public State ActiveChild;

        protected State(StateMachine stateMachine, State parent = null) {
            Machine = stateMachine;
            Parent = parent;
        }

        protected virtual State GetInitialState() => null; // Initial child to enter when this state starts (null = this is the leaf)
        protected virtual State GetTransition() => null; // Target state to switch to this frame (null = stay in current state)
        protected virtual void OnEnter() { }
        protected virtual void OnExit() { }
        protected virtual void OnUpdate(float deltaTime) { }

        internal void Enter() {
            if (Parent != null) Parent.ActiveChild = this;
            OnEnter();
            var init = GetInitialState();
            init?.Enter();
        }
        internal void Exit() {
            ActiveChild?.Exit();
            ActiveChild = null;
            OnExit();
        }
        // Template method to control execution order. Check this Transition -> Check child transition -> Update child -> Update this 
        internal void Update(float deltaTime) {
            var t = GetTransition();
            if (t != null) {
                Machine.Sequencer.RequestTransition(this, t);
                return;
            }
            ActiveChild?.Update(deltaTime);
            OnUpdate(deltaTime);
        }

        public State Leaf() {
            var s = this;
            while(s.ActiveChild != null) s = s.ActiveChild;
            return s;
        }

        public IEnumerable<State> PathToRoot() {
            for(var s = this; s != null; s = s.Parent) yield return s;
        }
    }
}