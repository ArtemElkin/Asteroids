using System;
using _Project.Core.GameLifecycle;
using _Project.Core.Render.VFX;
using _Project.Core.Tools;

namespace _Project.Features.Common.Effects
{
    public class EffectsManager : IWorldResettable, IDisposable
    {
        private readonly IGameStateService _gameStateService;
        private readonly Storage<IEffect> _effectStorage;


        public EffectsManager(Storage<IEffect> effectStorage, IGameStateService gameStateService)
        {
            _effectStorage = effectStorage;
            _gameStateService = gameStateService;
            _gameStateService.OnGameStateChanged += OnGameStateChanged;
        }

        private void OnGameStateChanged(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.Paused:
                    foreach(var e in _effectStorage) e.Pause();
                    break;
                case GameState.Resume:
                    foreach (var e in _effectStorage) e.Resume();
                    break;
            }
        }

        public void Reset()
        {
            var effects = _effectStorage.GetAll();
            foreach (var e in effects)
            {
                e.Stop();
                _effectStorage.Remove(e);
            }
        }

        public void Dispose()
        {
            _gameStateService.OnGameStateChanged -= OnGameStateChanged;
        }
    }
}