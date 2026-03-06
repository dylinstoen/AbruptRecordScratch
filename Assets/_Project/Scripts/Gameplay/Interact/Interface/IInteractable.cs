using _Project.Scripts.Gameplay.Structs;
using UnityEngine;

namespace _Project.Scripts.Gameplay {
    public interface IInteractable {
        void Interact();
        bool CanInteract();
        InteractionCue GetCue(); 
    }
}