namespace _Project.Scripts.Actors {
    /// <summary>
    /// The single entry point for all external systems like the level boostrap to wire dependencies and initalize the player
    /// </summary>
    public interface IPlayerService {
        IPlayerFacade PlayerFacade { get; }
        void Initialize(IPlayerFacade playerService);
    }
}