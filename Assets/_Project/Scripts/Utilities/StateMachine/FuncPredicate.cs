using System;
using _Project.Scripts.Utilities.StateMachine.Interfaces;

namespace _Project.Scripts.Utilities.StateMachine {
    public class FuncPredicate : IPredicate {
        readonly Func<bool> _func;

        public FuncPredicate(Func<bool> func) {
            _func = func;
        }
        public bool Evaluate() => _func.Invoke();
    }
}