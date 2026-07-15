using _Project.Core.GameLifecycle;
using Zenject;

namespace _Project.Infrastructure.GameLifecycle
{
    public class GameplayStarter : IInitializable
    {
        private readonly IGameStateService _gameStateService;

        
        public GameplayStarter(IGameStateService gameStateService)
        {
            _gameStateService = gameStateService;
        }

        public void Initialize()
        {
            _gameStateService.SetState(GameState.Running);
        }
    }
}