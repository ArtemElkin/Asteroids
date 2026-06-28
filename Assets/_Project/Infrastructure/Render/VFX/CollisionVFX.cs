using _Project.Core.GameLifecycle;
using Zenject;

namespace _Project.Infrastructure.Render.VFX
{
    public class CollisionVFX : ParticleSystemEffect
    {
        private IGameStateService _gameStateService;
        
        
        private void OnEnable()
        {
            _gameStateService.OnGameStateChanged += OnGameStateChanged;
        }

        [Inject]
        private void Construct(IGameStateService gameStateService)
        {
            _gameStateService = gameStateService;
        }

        private void OnGameStateChanged(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.Paused:
                    _particleSystem.Pause();
                    break;
                case GameState.Running:
                    _particleSystem.Play();
                    break;
                case GameState.Restart:
                    Stop();
                    break;
            }
        }
        
        private void OnDisable()
        {
            _gameStateService.OnGameStateChanged -= OnGameStateChanged;
        }
    }
}