using UnityEngine;

namespace FPS.Input {
    public interface IInput {
        /// <summary>
        /// Entity request a direction to move in
        /// </summary>
        /// <returns>Vector2</returns>
        public Vector2 Move();

        /// <summary>
        /// Entity request to look 
        /// </summary>
        /// <returns>Vector2</returns>
        public Vector2 Look();
        /// <summary>
        /// Entity request to pause 
        /// </summary>
        /// <returns>Bool</returns>
        public bool Pause();
    }
}