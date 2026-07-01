using _Project.Core.EventBus;
using _Project.Core.GameLifecycle;
using _Project.Core.Render;
using _Project.Core.Services;
using _Project.Features.Common.EntitiesLifecycle;
using _Project.Features.Common.EntitiesLifecycle.Events;

namespace _Project.Features.Spaceship.Weapon.LaserWeapon.LaserBeam
{
    public class LaserBeamFacade : IFacade
    {
        public IDrawable Drawable { get; }
        private readonly Timer _timer;
        private readonly IGameStateService _gameStateService;
        private readonly IEventBus _eventBus;


        public LaserBeamFacade(
            IDrawable drawable,
            float aliveTime,
            Timer timer,
            IGameStateService gameStateService,
            IEventBus eventBus)
        {
            Drawable = drawable;
            _timer = timer;
            _gameStateService = gameStateService;
            _eventBus = eventBus;

            _gameStateService.OnGameStateChanged += OnGameStateChanged;
            _timer.Elapsed += OnTimerElapsed;
            _timer.Start(aliveTime);
        }

        private void OnGameStateChanged(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.Paused:
                    _timer.Pause();
                    break;
                case GameState.Resume:
                    _timer.Resume();
                    break;
            }
        }

        private void OnTimerElapsed()
        {
            _eventBus.Publish(new DespawnRequestedEvent<LaserBeamFacade>(this));
        }

        public void Dispose()
        {
            _gameStateService.OnGameStateChanged -= OnGameStateChanged;
            _timer.Elapsed -= OnTimerElapsed;
            _timer.Dispose();
        }
    }
}