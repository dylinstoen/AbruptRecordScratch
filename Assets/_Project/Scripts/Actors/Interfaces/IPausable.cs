namespace _Project.Scripts.Actors {
    public interface IPausable {
        /// <summary>
        /// Entity request to pause 
        /// </summary>
        /// <returns>Bool</returns>
        public bool PauseIntent();
    }
}