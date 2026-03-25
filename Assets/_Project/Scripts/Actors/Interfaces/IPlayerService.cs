namespace _Project.Scripts.Actors {
    public interface IPlayerService {
        IPlayerFacade PlayerFacade { get; }
        void Initialize(IPlayerFacade playerService);
    }
}