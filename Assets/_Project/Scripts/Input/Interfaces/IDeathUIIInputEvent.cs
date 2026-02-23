using System;

namespace _Project.Scripts.Input {
    public interface IDeathUIIInputEvent {
        event Action ContinueRequested;
    }
}