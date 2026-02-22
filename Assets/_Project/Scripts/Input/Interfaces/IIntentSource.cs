using _Project.Scripts.Actors;

namespace _Project.Scripts.Input {
    public interface IIntentSource {
        public ActorIntent Current { get;}
    }
}