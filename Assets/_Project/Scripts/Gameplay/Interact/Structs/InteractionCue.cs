using UnityEngine;

namespace _Project.Scripts.Gameplay.Structs {
    public struct InteractionCue {
        public readonly string Prompt;
        public readonly Transform PromptAnchor;
        public readonly IHighlightable Highlight;
        public InteractionCue(string prompt, Transform promptAnchor, IHighlightable highlight) {
            Prompt = prompt;
            Highlight = highlight;
            PromptAnchor = promptAnchor;
        }

        public bool HasPrompt => !string.IsNullOrEmpty(Prompt);
        public bool HasHighlight => Highlight != null;
    }
}