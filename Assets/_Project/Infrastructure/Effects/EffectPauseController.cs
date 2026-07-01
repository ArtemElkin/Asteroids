using System;
using System.Collections.Generic;
using _Project.Core.GameLifecycle;
using _Project.Core.Render.VFX;
using _Project.Core.Tools;

namespace _Project.Infrastructure.Effects
{
    public class EffectPauseController : IDisposable
    {
        private readonly IGameStateService _gameStateService;
        private readonly Storage<IEffect> _effectStorage;


        public EffectPauseController(Storage<IEffect> effectStorage, IGameStateService gameStateService)
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
                case GameState.Running:
                    foreach (var e in _effectStorage)
                    {
                        if (e.IsPlaying) e.Play();
                    }
                    break;
                case GameState.Restart:
                    var effects = new List<IEffect>(_effectStorage.GetAll());
                    foreach (var e in effects)
                    {
                        e.Stop();                        
                    }
                    break;
            }
        }
        
        public void Dispose()
        {
            _gameStateService.OnGameStateChanged -= OnGameStateChanged;
        }
    }
}