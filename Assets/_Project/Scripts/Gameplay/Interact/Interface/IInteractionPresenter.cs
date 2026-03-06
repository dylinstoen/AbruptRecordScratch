using _Project.Scripts.Gameplay.Structs;

namespace _Project.Scripts.Gameplay {
    public interface IInteractionPresenter {
        void Show(InteractionCue getCue);
        void Hide();
    }
}