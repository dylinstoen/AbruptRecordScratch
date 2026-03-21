namespace _Project.Scripts.Actors {
    public interface IPlayerService {
        IPlayerFacade PlayerFacade { get; }
        bool IsDead { get; }
        void Initialize(IPlayerFacade playerService);
    }
}