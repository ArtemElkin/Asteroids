using _Project.Core.EventBus;
using _Project.Core.GameLifecycle;
using _Project.Features.Common.EntitiesLifecycle.Events;

namespace _Project.Features.Spaceship
{
    public class SpaceshipDeathHandler
    {
        private readonly IGameStateService _gameStateService;
        private readonly IEventBus _eventBus;
        
        
        public SpaceshipDeathHandler(
            IGameStateService gameStateService,
            IEventBus eventBus)
        {
            _gameStateService = gameStateService;
            _eventBus = eventBus;
        }
        public void HandleDeath(SpaceshipFacade spaceshipFacade)
        {
            _eventBus.Publish(new DespawnRequestedEvent<SpaceshipFacade>(spaceshipFacade));
            _gameStateService.SetState(GameState.GameOver);
        }
    }
}