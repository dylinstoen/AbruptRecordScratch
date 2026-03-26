using System.Collections.Generic;

namespace _Project.Scripts.Utilities.HSM {
    // Manages changing from one state to another
    // 
    public class StateMachine {
        public readonly State Root;
        public readonly TransitionSequencer Sequencer;
        private bool started;

        public StateMachine(State root) {
            Root = root;
            Sequencer = new TransitionSequencer(this);
        }

        public void Start() {
            if(started) return;
            started = true;
            Root.Enter();
        }

        // The seperation of Tick and Internal Tick makes it easier to do sequencing
        public void Tick(float deltaTime) {
            if (!started) Start();
            InternalTick(deltaTime);
        }

        internal void InternalTick(float deltaTime) => Root.Update(deltaTime);
        
        // Perform the actual switch from 'from' to 'to' by exiting up to the shared ancestor, then entering down to the target
        public void ChangeState(State from, State to) {
            if (from == to || from == null || to == null) return;
            State lca = TransitionSequencer.Lca(from, to);
            for(State s = from; s != lca; s = s.Parent) s.Exit();
            var stack = new Stack<State>();
            for(State s = to; s != lca; s = s.Parent) stack.Push(s);
            while(stack.Count > 0) stack.Pop().Enter();
        }
    }
}